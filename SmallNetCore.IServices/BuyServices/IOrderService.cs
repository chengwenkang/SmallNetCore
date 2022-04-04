using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.BuyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.IServices.BuyServices
{
    public interface IOrderService
    {
        public BaseResponse<int> BuyProduct(BuyProductRequest request);
    }
}
