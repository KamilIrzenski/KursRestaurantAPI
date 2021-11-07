using System.Collections.Generic;
using System.Linq;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())

                if (!_dbContext.Roles.Any())
                {
                    var role = GetRoles();
                    _dbContext.Roles.AddRange(role);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>
            {
                new()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC American fast food",
                    Email = "contact@email.com,",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new()
                        {
                            Name = "Hot chicken",
                            Price = 10.30M
                        },
                        new()
                        {
                            Name = "Nuggets",
                            Price = 5.30M
                        }
                    }, 
                    Address = new Address
                    {
                        City = "Krakow",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new()
                {
                    Name = "MC Donald",
                    Category = "Fast Food",
                    Description = "MC Donald American fast food",
                    Email = "MCDonald@email.com,",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new()
                        {
                            Name = "Royal Burger",
                            Price = 11.30M
                        },
                        new()
                        {
                            Name = "Maestro Burger",
                            Price = 15.30M
                        }
                    },
                    Address = new Address
                    {
                        City = "Rzeszów",
                        Street = "Krótka 5",
                        PostalCode = "36-001"
                    }
                }
            };
            return restaurants;
        }
    }
}