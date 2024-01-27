using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.BL.Dtos
{
    class DisplayOrdersOFUserDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
