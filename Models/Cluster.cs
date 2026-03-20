using System.ComponentModel.DataAnnotations;

namespace Wprawka_1.Models
{
    public class Cluster
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ClusterName { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
