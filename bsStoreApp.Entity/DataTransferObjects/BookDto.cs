namespace bsStoreApp.Entity.DataTransferObjects
{
    [Serializable]
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
