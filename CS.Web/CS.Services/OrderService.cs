using System.Text;
using CS.Data;
using CS.Data.Enums;
using CS.Data.Models;
using CS.RequestResponse.Order;
using CS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Repo;

namespace CS.Services;

public class OrderService:IOrderService
{
    private readonly DataContext _context;
    private readonly IRepository<Order> _repository;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "5b3ce3597851110001cf6248850c2afff3ed41649b58a1d6c3396d56";


    public OrderService(DataContext context,IRepository<Order> repository)
    {
        _context = context;
        _repository = repository;
        _httpClient = new HttpClient();
    }
    public async Task<CreateOrderResponse> Create(CreateOrderRequest request)
    {
        var departureId = FinalId(request.DepartureName);
        var destinationId = FinalId(request.DestinationName);
        if (departureId==null||destinationId==null)
        {
            return new CreateOrderResponse
            {
                Success = false,
                Message = "Адрес конечной или начальной точки указаны неверно или их не существует"
            };
        }
        var order = new Order
        {
            Id=Guid.NewGuid(),
            ClientId = request.ClientId,
            DestinationId = destinationId,
            DepartureId = departureId,
        };
        await _repository.AddAsync(order);
        var history = new OrderHistory
        {
            OrderId = order.Id,
            Status = HistoryEnum.Created,
            OrderDate = DateTime.UtcNow,
        };
        await _context.AddAsync(history);
        await _repository.SaveChangesAsync();
        return new CreateOrderResponse(true,"Заказ успешно создан",order.Id,order.ClientId,order.DepartureId,order.DestinationId);
    }

    public async Task<CreateOrderAcceptanceResponse> CreateAcceptance(CreateOrderAcceptanceRequest request)
    {
        var orders=await _context.OrderHistories.Where(x=>x.OrderId==request.OrderId).ToListAsync();
        if (orders.Count==0)
            return new CreateOrderAcceptanceResponse(false,"Такого заказа не существует");
        foreach (var elem in orders)
        {
            if (elem.Status == HistoryEnum.Accepted)
                return new CreateOrderAcceptanceResponse(false,"Заказ уже принят");
        }
        var couriers=_context.Couriers.Find(request.CourierId);
        if (couriers == null)
            return new CreateOrderAcceptanceResponse(false,"Такого курьера не существует");
        var acceptance = new OrderAcceptance
        {
            OrderId = request.OrderId,
            CourierId = request.CourierId,
            AcceptanceDate = DateTime.UtcNow
        };
        var history = new OrderHistory
        {
            OrderId = request.OrderId,
            Status = HistoryEnum.Accepted,
            OrderDate = DateTime.UtcNow,
        };
        await _context.AddAsync(history);
        await _context.AddAsync(acceptance);
        await _repository.SaveChangesAsync();
        return new CreateOrderAcceptanceResponse(true, "Заказ успешно принят",acceptance.OrderId,acceptance.CourierId,acceptance.AcceptanceDate);
    }

    public async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request)
    {
        var orders =await _context.OrderHistories.Where(x=>x.OrderId==request.OrderId).ToListAsync();
        if (orders.Count == 0)
            return new CreatePaymentResponse(false, "Такого заказа не существует");
        foreach (var elem in orders)
        {
            if (elem.Status == HistoryEnum.Denied)
                return new CreatePaymentResponse(false,"Заказ уже оплачен");
        }
        var order =await _context.Orders.FirstOrDefaultAsync(x=>x.Id==request.OrderId);
        var departure = await BuildFullAddressAsync(order.DepartureId);
        var destination = await BuildFullAddressAsync(order.DestinationId);
        var distance = await CalculateDistanceAsync(departure, destination);
        var cost = (decimal)(distance * 10); // 10 руб/км
        var payment = new Payment
        {
            OrderId = request.OrderId,
            Amount = cost,
            Status = request.Status,
            PaymentDate = DateTime.UtcNow
        };
        await _context.AddAsync(payment);
        await _repository.SaveChangesAsync();
        return new CreatePaymentResponse(true,"Оплата успешно произведена",payment.OrderId,payment.Amount,payment.Status,payment.PaymentDate);
    }

    public async Task<GetClientOrdersResponse> GetClientOrders(GetClientOrdersRequest request)
    {
        IEnumerable<Order> list =await _repository.GetAllAsync();
        var orders = list.Where(x=>x.ClientId==request.ClientId).ToList();
        if (orders.Count == 0)
            return new GetClientOrdersResponse(true,"Заказы отсутствуют");
        var orderSummary = new List<OrderSummary>();
        foreach (var elem in orders)
        {
            string departureName = await BuildFullAddressAsync(elem.DepartureId);
            string destinationName = await BuildFullAddressAsync(elem.DestinationId);
            double distance = await CalculateDistanceAsync(departureName, destinationName);
            orderSummary.Add(new OrderSummary
            {
                Id=elem.Id,
                DepartureName = departureName,
                DestinationName = destinationName,
                Distance = distance
            });
        }

        return new GetClientOrdersResponse(true,"Заказы успешно выведены",request.ClientId,orderSummary);
    }

    public async Task<GetCourierOrdersResponse> GetCourierOrders(GetCourierOrdersRequest request)
    {
        var acceptances=await _context.OrderAcceptances.Where(x=>x.CourierId==request.CourierId).ToListAsync();
        if (acceptances.Count == 0)
            return new GetCourierOrdersResponse(false,"Id курьера не существует");
        var history = await _context.OrderHistories.ToListAsync();
        var orders = new List<OrderAcceptanceDto>();
        foreach (var elem in acceptances)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x=>x.Id==elem.OrderId);
            var departure = await BuildFullAddressAsync(order.DepartureId);
            var destination = await BuildFullAddressAsync(order.DestinationId);
            var dictance =await CalculateDistanceAsync(departure,destination);
            var status = history.Where(x => x.OrderId == elem.OrderId).MaxBy(x=>x.OrderDate);
            orders.Add(new OrderAcceptanceDto
            {
                OrderId = elem.OrderId,
                CourierId = elem.CourierId,
                DepartureName = departure,
                DestinationName = destination,
                Distance = dictance,
                Status = status.Status,
                AcceptanceDate = elem.AcceptanceDate
            });
        }

        return new GetCourierOrdersResponse(true,"История успешно выведена",request.CourierId,orders);
    }

    public async Task<GetOrdersResponse> GetOrders(GetOrdersRequest request)
    {
        var histories =await _context.OrderHistories.ToListAsync();
        var orders = await _repository.GetAllAsync();
        var getOrders = new List<OrderDto>();
        foreach (var elem in orders)
        {
            var history = histories.Where(x => x.OrderId == elem.Id).MaxBy(x=>x.OrderDate);
            if (history.Status == HistoryEnum.Created)
            {
                var departure = await BuildFullAddressAsync(elem.DepartureId);
                var destination = await BuildFullAddressAsync(elem.DestinationId);
                var dictance =await CalculateDistanceAsync(departure,destination);
                getOrders.Add(new OrderDto
                {
                    OrderId = elem.Id,
                    DepartureName = departure,
                    DestinationName = destination,
                    Distance = dictance
                });
            }
        }

        if (getOrders.Count == 0)
            return new GetOrdersResponse(false,"Ошибка в обработке свободных заказов");
        return new GetOrdersResponse(true,"Свободные заказы успешно выведены",request.CourierId,getOrders);
    }

    public Guid FinalId(string address)
    {
        var addressMass = address.Split(',');
        var routes = _context.Routes;
        var city = routes.FirstOrDefault(x=>x.Name==addressMass[0]);
        var street = routes.FirstOrDefault(x=>city != null && x.ParentId==city.Id&&x.Name==addressMass[1]);
        var home = routes.FirstOrDefault(x=>street!=null&&x.ParentId==street.Id&&x.Name==addressMass[2]);
        return home.Id;
    }

    private async Task<string> BuildFullAddressAsync(Guid addressId)
    {
        var address = await _context.Routes.FindAsync(addressId);
        if (address == null) return "Неизвестно";

        var route = await _context.Routes.FindAsync(address.Id);

        var segments = new List<string>();
        while (route != null)
        {
            segments.Insert(0, route.Name);
            route = route.ParentId.HasValue ? await _context.Routes.FindAsync(route.ParentId) : null;
        }

        return string.Join(", ", segments);
    }
    
    public async Task<double> CalculateDistanceAsync(string from, string to)
    {
        var fromCoords = await GeocodeAsync(from);
        var toCoords = await GeocodeAsync(to);
        
        if (fromCoords == null || toCoords == null)
            return 0;
        var body = new
        {
            coordinates = new[] { fromCoords, toCoords }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openrouteservice.org/v2/directions/driving-car/json")
        {
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", _apiKey);

        var response = await _httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(result);

        var distanceInMeters = json["routes"]?[0]?["summary"]?["distance"]?.ToObject<double>();

        return distanceInMeters.HasValue ? distanceInMeters.Value / 1000 : 0; // в км
    }

    private async Task<double[]> GeocodeAsync(string address)
    {
        var uri = $"https://api.openrouteservice.org/geocode/search?api_key={_apiKey}&text={Uri.EscapeDataString(address+", Moldova")}&lang=ru&boundary.country=MDA";

        var result = await _httpClient.GetStringAsync(uri);
        var json = JObject.Parse(result);
        var coords = json["features"]?[0]?["geometry"]?["coordinates"]?.ToObject<double[]>();
        return coords;
    } 
}