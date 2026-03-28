using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wprawka_2.Models
{
    public class Device
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa urządzenia nie może być pusta.")]
        [Display(Name = "Nazwa Urządzenia")]
        public string DeviceName { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Moc musi być w przedziale od 1W do 10000W.")]
        [Display(Name = "Moc (W)")]
        public int PowerWatt { get; set; }

        // Klucz obcy i relacja
        [Required(ErrorMessage = "Musisz przypisać urządzenie do klastra.")]
        [Display(Name = "Klaster")]
        public int ClusterId { get; set; }
        [ForeignKey("ClusterId")]
        public virtual Cluster? Cluster { get; set; }
    }
}