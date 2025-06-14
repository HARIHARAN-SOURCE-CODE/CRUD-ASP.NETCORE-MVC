using System.ComponentModel.DataAnnotations;

namespace TATABRAND.Models
{
    public class TataPk
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name  { get; set; }
        [Display(Name="DOB")]
        public String EstablishYear { get; set; }
        [Display (Name =" UPLOAD IMAGE")]
        public String Logo { get; set; }

    }
}
