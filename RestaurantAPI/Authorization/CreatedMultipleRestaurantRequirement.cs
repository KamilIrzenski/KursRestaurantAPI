using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurantRequirement : IAuthorizationRequirement
    {

        public CreatedMultipleRestaurantRequirement(int minmumRestaurantCreated)
        {
            MinimumRestaurantCreated = minmumRestaurantCreated;
        }
        public int MinimumRestaurantCreated { get; }
    }
}
