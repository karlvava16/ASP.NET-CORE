namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Producer { get; set; }
        public string? Genre { get; set; }
        public DateOnly YearOfIssue { get; set; }
        public string? Poster { get; set; }
        public string? ShortDescription { get; set; }
    }

}
