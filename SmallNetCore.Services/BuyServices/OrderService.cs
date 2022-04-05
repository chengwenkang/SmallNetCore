using SmallNetCore.Common.Convets;
using SmallNetCore.IRepository.FirstTestDb;
using SmallNetCore.IRepository.SecondTestDb;
using SmallNetCore.IServices.Base;
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
        IClaimsAccessor claimsAccessor;

        public OrderServicel(IClaimsAccessor _claimsAccessor, IOrderRepository _orderRepository, IUserRepository _userRepository)
        {
            this.claimsAccessor = _claimsAccessor;
            this.orderRepository = _orderRepository;
            this.userRepository = _userRepository;
        }

        /// <summary>
        /// 完全是为了测试事务，逻辑忽略
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<int> BuyProduct(BuyProductRequest request)
        {
            var order = new Order
            {
                ProductId = request.ProductId,
                UserId = claimsAccessor.CurrentUserId
            };

            var user = new User
            {
                UserName = request.UserName,
                Sex = 1
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
