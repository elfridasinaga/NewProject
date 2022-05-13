using System;
using System.Collections.Generic;

namespace UserService.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string OrderCode { get; set; } = null!;
        public int OrderContent { get; set; }
        public DateTime Created { get; set; }

        public virtual User OrderContentNavigation { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
