﻿using System;
using System.Collections.Generic;

namespace OrderProcessor.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Profiles = new HashSet<Profile>();
            UserRoles = new HashSet<UserRole>();
        }

        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
