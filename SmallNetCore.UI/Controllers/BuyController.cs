using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallNetCore.IServices.BuyServices;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.BuyServices;

namespace SmallNetCore.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BuyController : ControllerBase
    {
        IOrderService orderService;
        public BuyController(IOrderService _orderService)
        {
            this.orderService = _orderService;
        }

        /// <summary>
        /// 下单，完全是为了测试跨库事务，逻辑忽略
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> BuyProduct(BuyProductRequest request)
        {
            return orderService.BuyProduct(request);
        }
    }
}
