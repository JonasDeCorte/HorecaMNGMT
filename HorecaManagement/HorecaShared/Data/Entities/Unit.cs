namespace Horeca.Shared.Data.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new();
    }
}