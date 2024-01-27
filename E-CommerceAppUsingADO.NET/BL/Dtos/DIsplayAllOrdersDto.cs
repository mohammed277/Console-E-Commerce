using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.BL.Dtos
{
    class DIsplayAllOrdersDto: DisplayOrdersOFUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
