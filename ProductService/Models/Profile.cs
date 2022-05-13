using System;
using System.Collections.Generic;

namespace ProductService.Models
{
    public partial class Profile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
