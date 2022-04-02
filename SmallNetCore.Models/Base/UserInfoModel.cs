using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.Base
{
    public class UserInfoModel
    {
        public int Id { set; get; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
