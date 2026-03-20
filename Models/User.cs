using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Wprawka_1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public virtual ICollection<Home> Homes { get; set; } = null!;

        public virtual ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();
    }
}