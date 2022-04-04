using Microsoft.AspNetCore.Mvc;
using SmallNetCore.IServices.BuyServices;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.BuyServices;

namespace SmallNetCore.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        IOrderService orderService;
        public BuyController(IOrderService _orderService)
        {
            this.orderService = _orderService;
        }

        /// <summary>
        /// 购买
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
