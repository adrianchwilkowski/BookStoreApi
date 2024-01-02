using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Migrations;
using Infrastructure.Models.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IOrdersRepository
    {
        public Task CreateOrder(Order order);
        public Task ChangeCost(Order order, double amountToAdd);
        public Task<List<Order>> GetAllOrders();
        public Task AddItem(OrderedItem item);
        public Task<Order> GetOrderById(Guid Id);
    }
    public class OrdersRepository : IOrdersRepository
    {
        public ApplicationDbContext Context { get; set; }
        public OrdersRepository(ApplicationDbContext context) { Context = context; }

        public async Task CreateOrder(Order order)
        {
            Context.Orders
                .Add(order);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AlreadyExistsException("Order with given ID already exists.");
            }
        }
        public async Task ChangeCost(Order order, double amountToAdd)
        {
            try
            {
                order.ChangeCost(amountToAdd);
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotFoundException("Order with given id doesn't exist.");
            }
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await Context.Orders.ToListAsync();
        }
        public async Task<Order> GetOrderById(Guid Id)
        {
            return await Context.Orders.Where
                (x => x.Id == Id)
                .FirstAsync();
        }

        public async Task AddItem(OrderedItem item)
        {
            await Context.OrderedItems.AddAsync(item);
            await Context.SaveChangesAsync();
        }
    }
}
