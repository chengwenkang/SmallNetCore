using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.ViewModels.Response.Remotes
{
    public class PlayResponse
    {
        public List<PlayItem> list { get; set; }
    }

    public class PlayItem
    {
        public string name { get; set; }

        public int age { get; set; }
    }
}
