using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Repositories
{
    public static class ShirtRepository
    {

        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt {ShirtId = 1, Brand = "MyBrand", Color = "Green", Gender = "Male", Price = 30, Size = 30},
            new Shirt {ShirtId = 2, Brand = "MyBrand2", Color = "Red", Gender = "Woman", Price = 15, Size = 15},
            new Shirt {ShirtId = 3, Brand = "MyBrand3", Color = "Ble", Gender = "Male", Price = 10, Size = 10},
            new Shirt {ShirtId = 4, Brand = "MyBrand4", Color = "Green", Gender = "Woman", Price = 20, Size = 20}
        };

        public static void AddShirt(Shirt shirt)
        {
            int maxId = shirts.Max(shirt => shirt.ShirtId);
            shirt.ShirtId = maxId + 1;
            shirts.Add(shirt);
        }
        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = shirts.First(x => x.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;
        }
        public static List<Shirt> GetShirts()
        {
            return shirts;
        }
        public static bool ShirtExist(int id)
        {
            return shirts.Any(shirt => shirt.ShirtId == id);
        }
        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(shirt => shirt.ShirtId == id);
        }
        public static Shirt? GetShirtByProperties(string? brand, string? gender, string? color, int? size)
        {
            return shirts.FirstOrDefault(shirt =>
                !string.IsNullOrWhiteSpace(brand) &&
                !string.IsNullOrWhiteSpace(shirt.Brand) &&
                shirt.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(gender) &&
                !string.IsNullOrWhiteSpace(shirt.Gender) &&
                shirt.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(color) &&
                !string.IsNullOrWhiteSpace(shirt.Color) &&
                shirt.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
                size.HasValue &&
                shirt.Size.HasValue &&
                size.Value == shirt.Size.Value);
        }

        public static void DeleteShirt(int shirtId)
        {
            var shirt = GetShirtById(shirtId);
            if (shirt != null)
            {
                shirts.Remove(shirt);
            }
        }
    }
}