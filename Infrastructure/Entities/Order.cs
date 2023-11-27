namespace Infrastructure.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public double FullCost { get; set; }
        public int DeliveryType { get; set; }
        public Guid UserId { get; set; }
    }
}
