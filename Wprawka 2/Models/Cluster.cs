using System.ComponentModel.DataAnnotations;

namespace Wprawka_2.Models
{
    public class Cluster
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa klastra jest wymagana.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od 3 do 50 znaków.")]
        [Display(Name = "Nazwa Klastra")]
        public string Name { get; set; }

        // Relacja 1..N
        public virtual ICollection<Device>? Devices { get; set; }
    }
}