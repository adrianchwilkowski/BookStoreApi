using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Tests
{
    public class OrdersRepositoryTests : IntegrationTestBase
    {
        [Test]
        public async Task CanCreate()
        {
            var order = Order.Create(3, Guid.NewGuid());
            var count = await ordersRepository.GetAllOrders();
            await ordersRepository.CreateOrder(order);
            var countAfter = await ordersRepository.GetAllOrders();
            Assert.True(countAfter.Count == count.Count + 1);
        }

    }
}
