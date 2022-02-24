using Ardalis.GuardClauses;
using Domain.Kitchen;
using Domain.Restaurants;
using HorecaDomain.Common;

namespace Domain.Orders
{
    public class Order : Entity
    {
        public int Id { get; set; }
        public Table Table { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        public Order()
        {
        }

        public Order(Table table)
        {
            Table = Guard.Against.Null(table, nameof(table));
        }
    }
}