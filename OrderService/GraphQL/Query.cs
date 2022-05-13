using HotChocolate;
using HotChocolate.Types;

using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using OrderService.Models;

namespace OrderService.GraphQL
{
    public class Query
    {
        public IQueryable<Order> GetOrders([Service] StudyCaseContext context) =>
            context.Orders.AsQueryable();
    }
}
