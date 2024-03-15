namespace bsStoreApp.Entity.Exceptions
{
    public sealed class BookNotFound : NotFoundExceptions
    {
        public BookNotFound(int id) : base($"The book with id : {id} could not found.")
        {

        }
    }
}
