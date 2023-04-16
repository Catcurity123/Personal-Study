using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Item
    {
        public int? ItemID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class Food : Item
    {
        public Food(int? FoodId, string FoodName, decimal FoodPrice)
        {
            ItemID = FoodId;
            Name = FoodName;   
            Price = FoodPrice;
        }

        public void GetFullDescription()
        {
            string Description = $"This Food has an ID of {ItemID}, the name of this food is {Name}, its price is {Price}";
            Console.WriteLine(Description);
        }
    }

    public class Beverage : Item
    {
        public Beverage(int? BeverageId, string BeverageName, decimal BeveragePrice)
        {
            ItemID = BeverageId;
            Name = BeverageName;
            Price = BeveragePrice;
        }

        public void GetFullDescription()
        {
            string Description = $"This drink has an ID of {ItemID}, the name of this drink is {Name}, its price is {Price}";
            Console.WriteLine(Description);
        }

    }

    public interface IItemDatabase
    {
        int SaveItemToDatabase(Item item);
        int GetItemFromDatabase(int foodId);
    }

    public class SQLFoodDatabase : IItemDatabase
    {
        public int SaveItemToDatabase(Item item)
        {
            //Implementation
            return 0;
        }

        public int GetItemFromDatabase(int itemId)
        {
            //Implementation
            return 0;
        }
    }

    public class FoodRepository 
    {
        private SQLFoodDatabase _foodDatabase;
        public FoodRepository(SQLFoodDatabase database)
        {
            _foodDatabase = database;
        }

        public int SaveFood(Food food)
        {
            int result = _foodDatabase.SaveItemToDatabase(food);
            return result;
        }

        public int GetFood(int foodId)
        {
            int result = _foodDatabase.GetItemFromDatabase(foodId);
            return result;
        }
    }

    public class BeverageRepository
    {
        private SQLFoodDatabase _beverageDatabase;
        public BeverageRepository(SQLFoodDatabase database)
        {
            _beverageDatabase = database;
        }

        public int SaveFood(Beverage beverage)
        {
            int result = _beverageDatabase.SaveItemToDatabase(beverage);
            return result;
        }

        public int GetFood(int beverageID)
        {
            int result = _beverageDatabase.GetItemFromDatabase(beverageID);
            return result;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Food BanhMi = new Food(11, "Banh Mi", 5000);
            Beverage CocaCola = new Beverage(12, "Coca Cola", 2000);

            BanhMi.GetFullDescription();
            CocaCola.GetFullDescription();
            Console.ReadLine();
        }
    }

   


}