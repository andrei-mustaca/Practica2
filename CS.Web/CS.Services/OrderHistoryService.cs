using CS.Data;
using CS.Data.Models;
using CS.RequestResponse.OrderHistory;
using CS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repo;

namespace CS.Services;

public class OrderHistoryService: IOrderHistoryService
{
    private readonly IRepository<OrderHistory> _repository;
    private readonly DataContext _context;

    public OrderHistoryService(IRepository<OrderHistory> repository, DataContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<GetOrderHistoryResponse> GetOrderHistory(GetOrderHistoryRequest request)
    {
        var histories =await _context.OrderHistories.Where(x=>x.OrderId==request.OrderId).ToListAsync();
        if (histories.Count == 0)
        {
            return new GetOrderHistoryResponse
            {
                Success = false,
                Message = "Заказ не существует"
            };
        }
        var list = new List<OrderHistories>();
        foreach (var history in histories)
        {
            list.Add(new OrderHistories{OrderId = history.OrderId,Status = history.Status,Date=history.OrderDate });
        }
        var orderHistoryResponse = new GetOrderHistoryResponse
        {
            Success = true,
            Message = "История успешно выведена",
            OrderId = request.OrderId,
            Histories=list
        };
        return orderHistoryResponse;
    }
}