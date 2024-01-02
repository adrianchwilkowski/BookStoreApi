using BookStore.Helper;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace BookStore.Services
{
    public interface IOrdersService
    {
        public Task CreateOrder(int deliveryTypeNr);
    }
    public class OrdersService : IOrdersService
    {
        private readonly IIdentityService _identityService;
        private readonly JwtInfo _jwtInfo;
        private readonly IOrdersRepository _ordersRepository;
        public OrdersService(IIdentityService identityService, JwtInfo jwtInfo, IOrdersRepository ordersRepository)
        {
            _identityService = identityService;
            _jwtInfo = jwtInfo;
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _ordersRepository.GetAllOrders();
        }

        public async Task CreateOrder(int deliveryTypeNr)
        {
            var login = _jwtInfo.GetName();
            var user = await _identityService.GetUserByLogin(login);
            if(user == null)
            {
                throw new UnauthorizedAccessException("User with given login doesn't exist.");
            }
            var order = Order.Create(deliveryTypeNr, Guid.Parse(user.Id));
            await _ordersRepository.CreateOrder(order);
        }
    }
}
