namespace Horeca.Core.Exceptions
{
    public class EntityIsAlreadyPartOfThisCollectionException : Exception
    {
        public EntityIsAlreadyPartOfThisCollectionException() : base("entity is already in this collection")
        {
        }

        public static EntityIsAlreadyPartOfThisCollectionException Instance { get; } = new();
    }
}