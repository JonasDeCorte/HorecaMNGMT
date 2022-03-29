namespace Horeca.Shared.Data.Entities
{
    public class Floorplan : BaseEntity
    {
        public string Level { get; set; }
        public string Name { get; set; }

        public int TotalCapacity { get; set; }

        public List<Table> Tables { get; set; } = new();

        public void AddTable(Table table)
        {
            if (table == null)
            {
                throw new NullReferenceException();
            }

            if (table.AmountOfPeople > TotalCapacity)
            {
                throw new ArgumentOutOfRangeException("Total capacity exceeded");
            }

            TotalCapacity -= table.AmountOfPeople;

            Tables.Add(table);
        }
    }
}