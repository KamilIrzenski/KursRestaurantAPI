using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{


    public interface IDishService
    {
        int Create(int restauranId, CreatDishDto dto);
        DishDto GetById(int restaurantId, int dishId);
        public List<DishDto> GetAll(int restaurantId);

        void RemoveAll(int restaurantId);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext _context, IMapper mapper)
        {
            this._context = _context;
            _mapper = mapper;
        }
        public int Create(int restauranId, CreatDishDto dto)
        {
            var restaurant = GetRestaurantId(restauranId);
            var dishEntity = _mapper.Map<Dish>(dto);

            dishEntity.RestaurantId = restauranId;

            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();

            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantId(restaurantId);

            var dish = _context.Dishes.FirstOrDefault(x => x.Id == dishId);
            if (dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            var dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;

        }

        public List<DishDto> GetAll(int restaurantId)
        {

            var restaurant = GetRestaurantId(restaurantId);
            var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);

            return dishDtos;
        }

        public void RemoveAll(int restaurantId)
        {
            var restaurant = GetRestaurantId(restaurantId);

            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }

        private Restaurant GetRestaurantId(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            return restaurant;

        }
    }
}
