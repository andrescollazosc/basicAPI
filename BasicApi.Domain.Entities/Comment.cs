namespace BasicApi.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int BookId { get; set; }
        public bool Active { get; set; }
        public Book Book { get; set; }
    }
}
