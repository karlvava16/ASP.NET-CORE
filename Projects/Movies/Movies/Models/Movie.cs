namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Producer? Producer { get; set; }
        public Genre? Genre { get; set; }
        public int YearOfIssue { get; set; }
        public string? Poster { get; set; }
        public string? ShortDescription { get; set; }
    }

}
