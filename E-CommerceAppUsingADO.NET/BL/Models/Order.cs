using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.BL.Models
{
    class Order
    {
        public int Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        //public List<Product> Products { get; set; }
    }
}
