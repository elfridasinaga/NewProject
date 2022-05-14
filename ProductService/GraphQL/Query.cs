
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ProductService.Models;
using OrderProcessor.GraphQL;

namespace ProductService.GraphQL
{
    public class Query
    {
        [Authorize]
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

        


    }
}
