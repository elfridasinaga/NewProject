
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using OrderProcessor.Models;

namespace ProductQL.GraphQL
{
    public class Query
    {
        public IQueryable<Product> GetProducts([Service] StudyCaseContext context) =>
            context.Products;

        [Authorize] // dapat diakses kalau sudah login
        public IQueryable<UserData> GetUsers([Service] StudyCaseContext context) =>
            context.Users.Select(p => new UserData()
            {
                Id = p.Id,
                FullName = p.FullName,
                Email = p.Email,
                Username = p.Username
            });

        /*[Authorize] // dapat diakses kalau sudah login
        public IQueryable<UserData> GetUsers([Service] StudyCaseContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;

            // check admin role ?
            var adminRole = claimsPrincipal.Claims.Where(o => o.Type == ClaimTypes.Role && o.Value == "ADMIN").FirstOrDefault();
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            if (user != null)
            {
                if (adminRole != null)
                {
                    throw new Exception("Role Invalid");
                }
                //var users = context.Users.Where(o => o.Id == user.Id);
                //return user.AsQueryable();
            }


            return new List<UserData>().AsQueryable();
        }*/

        [Authorize] 
        public IQueryable<Profile> GetProfiles([Service] StudyCaseContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;

            // check admin role ?
            var adminRole = claimsPrincipal.Claims.Where(o => o.Type == ClaimTypes.Role && o.Value == "ADMIN").FirstOrDefault();
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            if (user != null)
            {
                if (adminRole!=null)                    
                {                    
                    return context.Profiles;
                }
                var profiles = context.Profiles.Where(o => o.UserId == user.Id);                
                return profiles.AsQueryable();
            }


            return new List<Profile>().AsQueryable();
        }
        [Authorize]
        public IQueryable<Order> GetOrders([Service] StudyCaseContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;

            // check manager role ?
            var managerRole = claimsPrincipal.Claims.Where(o => o.Type == ClaimTypes.Role && o.Value == "MANAGER").FirstOrDefault();
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            if (user != null)
            {
                if (managerRole != null)
                    return context.Orders.Include(o => o.OrderDetails);

                var orders = context.Orders.Where(o => o.UserId == user.Id);
                return orders.AsQueryable();
            }


            return new List<Order>().AsQueryable();
        }

    }
}
