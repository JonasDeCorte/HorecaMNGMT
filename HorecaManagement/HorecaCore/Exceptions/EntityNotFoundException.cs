namespace Horeca.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Entity not found.")
        {
        }

        public static EntityNotFoundException Instance { get; } = new();
    }
}