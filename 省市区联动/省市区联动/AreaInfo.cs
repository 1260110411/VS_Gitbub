using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 省市区联动
{
    public partial class AreaInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
