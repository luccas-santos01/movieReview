namespace CineCritique.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public string? Image { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}