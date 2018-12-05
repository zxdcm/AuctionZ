using System;
using System.Collections.Generic;
using System.Numerics;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(AuctionContext context) // replace with DI later
        {
//            context.Database.EnsureDeleted();
//            context.Database.EnsureCreated();
//            var (u, c, l, b) = GetPreconfiguredData();
//            context.Users.AddRange(u);
//            context.Categories.AddRange(c);
//            context.Lots.AddRange(l);
//            context.Bids.AddRange(b);
//            context.SaveChanges();
        }



        private static (IEnumerable<User>, IEnumerable<Category>, IEnumerable<Lot>, IEnumerable<Bid>) GetPreconfiguredData()
        {
            var users = GetPreconfiguredUsers();
            var categories = GetPreconfiguredCategories();
            var lots =  new List<Lot>()
            {
                new Lot(){
                Category = categories[0],
                Description = "Test",
                ExpirationTime = DateTime.Today.AddDays(10),
                ImageUrl = null,
                Name = "Test",
                Price = (Decimal)100000.00001101,
                User = users[0]},
            };
            var bids = new List<Bid>()
            {
                new Bid
                {
                    DateOfBid = DateTime.Now, Lot = lots[0],
                    Price = lots[0].Price + 10m,
                    User = users[2],
                },
                new Bid
                {
                    DateOfBid = DateTime.Now, Lot = lots[0],
                    Price = lots[0].Price + 20m,
                    User = users[3],
                },
            };

            return (users, categories, lots, bids);
        }

        private static List<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User { FirstName  = "User 1" },
                new User { FirstName= "User 2" },
                new User { FirstName = "User 3" },
                new User { FirstName = "User 4" },
                new User { FirstName= "User 5" },
            };
        }

        private static List<Category> GetPreconfiguredCategories()
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