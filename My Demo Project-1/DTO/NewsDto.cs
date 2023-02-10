namespace GameNews.DTO
{
    public class NewsDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
        public int AuthorID { get; set; }
        public int? Views { get; set; }
        public int CategoryID { get; set; }

    }
}
