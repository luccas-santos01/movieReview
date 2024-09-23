namespace CineCritique.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public User? User { get; set; }
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        [Required]
        public Movie? Movie { get; set; }
    }
}