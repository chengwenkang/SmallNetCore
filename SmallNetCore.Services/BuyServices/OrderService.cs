using SmallNetCore.IServices.BuyServices;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.DBModels.SecondTestDb;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.BuyServices;
using SmallNetCore.Repository.Base;
using SmallNetCore.Services.Base;
using static SmallNetCore.Models.ViewModels.Base.CommonResponse;

namespace SmallNetCore.Services.BuyServices
{
    public class OrderServicel : BaseService<Order>, IOrderService
    {
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
                UserName = "skyskyskyskyskyskysky"
            };

            var resultTran = itenant.UseTran(() =>
           {
               var t1 = Context.Insertable(order).ExecuteCommand();

               var orderDal = base.ChangeRepository<BaseRepository<User>>();//切换仓储
               var t2 = orderDal.Context.Updateable(user).ExecuteCommand();
           });

            return GetOK(order.OrderId);
        }
    }
}
