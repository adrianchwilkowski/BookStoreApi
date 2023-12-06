using Infrastructure.Entities;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit
{
    public class OrderEntityTests
    {
        [Test]
        public void Create_ItemListIsEmpty()
        {
            var order = Order.Create(
                3,
                Guid.NewGuid());
            Assert.True(!order.Items.Any());
        }

        [Test]
        public void Create_IsSentStatusIsFalse()
        {
            var order = Order.Create(
                3,
                Guid.NewGuid());
            Assert.True(!order.IsSent);
        }

        [Test]
        public void ChangeIsSentStatus_IfStatusIsTheSame_ResultIsFalse()
        {
            var order = Order.Create(
                3,
                Guid.NewGuid());

            var result = order.ChangeIsSentStatus(false);

            Assert.True(!result);
        }

        [Test]
        public void AddItem_CannotMoodifyAfterSending()
        {
            var order = Order.Create(
                3,
                Guid.NewGuid());
            var book = Book.Create(
                "title",
                "author",
                string.Empty,
                123);
            var bookInfo = BookInfo.Create(
                12.99,
                5,
                book);

            order.ChangeIsSentStatus(true);
            Assert.Throws<OrderInProgressException>(() => order.AddItem(
                Guid.NewGuid(),
                5,
                bookInfo
                ));
        }
    }
}
