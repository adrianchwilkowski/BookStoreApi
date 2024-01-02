using Infrastructure.Exceptions;

namespace Infrastructure.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public double FullCost { get; private set; }
        public int DeliveryType { get; private set; }
        public Guid UserId { get; private set; }
        public IEnumerable<OrderedItem> Items { get; private set; } = null!;
        public bool IsSent { get; private set; }
        public static Order Create(int deliveryType, Guid userId)
        {
            return new Order()
            {
                DeliveryType = deliveryType,
                UserId = userId,
                FullCost = 0,
                Items = new List<OrderedItem>(),
                IsSent = false
            };
        }
        public void AddItem(Guid bookId, int quantity, BookInfo book)
        {
            if(!IsSent)
            {
                var item = OrderedItem.Create(book, this);
                item.ChangeQuantity(quantity);
                Items.Append(item);
                FullCost += quantity*book.Price;
            }
            else
            {
                throw new OrderInProgressException("cannot modify order after sending");
            }
        }
        public void ChangeCost(double amountToAdd)
        {
            FullCost += amountToAdd;
        }
        public bool ChangeIsSentStatus(bool isSent)
        {
            if (isSent != IsSent) 
            {
                IsSent = isSent;
                return true;
            }
            return false;
        }
    }
}
