namespace CineCritique.Models
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Name { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        [Required]
        public bool Admin { get; set; } = false;
    }
}