using SmallNetCore.IRepository.FirstTestDb;
using SmallNetCore.IRepository.SecondTestDb;
using SmallNetCore.IServices.BuyServices;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.DBModels.SecondTestDb;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.BuyServices;
using static SmallNetCore.Models.ViewModels.Base.CommonResponse;

namespace SmallNetCore.Services.BuyServices
{
    public class OrderServicel : IOrderService
    {
        IOrderRepository orderRepository;
        IUserRepository userRepository;

        public OrderServicel(IOrderRepository _orderRepository, IUserRepository _userRepository)
        {
            this.orderRepository = _orderRepository;
            this.userRepository= _userRepository;
        }

        public BaseResponse<int> BuyProduct(BuyProductRequest request)
        {
            var order = new Order
            {
                ProductId = request.ProductId,
                UserId = request.UserId
            };

            var user = new User
            {
                Id = 13,
                UserName = request.UserName
            };

            var resultTran = orderRepository.UseMutliTran(() =>
           {
               var t1 = orderRepository.Insert(order);
               var t2 = userRepository.Insert(user);
           });

            return GetOK(order.OrderId);
        }
    }
}
