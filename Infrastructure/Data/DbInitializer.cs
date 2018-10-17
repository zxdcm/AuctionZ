using System.Collections.Generic;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(AuctionContext context) // replace with DI later
        {
            
            context.AddRange(GetPreconfiguredCategories());
            context.AddRange(GetPreconfiguredUsers());
            context.SaveChanges();
   
        }

        private static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User { Name  = "Justin Noon" },
                new User { Name = "Willie Par" },
                new User { Name  = "Martin Noon" },
                new User { Name  = "Noon Justin" },
                new User { Name  = "WHil Par" },
            };
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category { Name = "Devices"},
                new Category { Name = "T-Shirts" },
                new Category { Name = "Coins" },
                new Category { Name = "Cars" },
            };
        }
    }
}