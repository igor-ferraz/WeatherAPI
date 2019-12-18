using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.DataContracts
{
    public class RequestCity
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} length cannot be greater than {1}.")]
        [Display(Name = "City name")]
        public string Name { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "{0} length cannot be greater than {1}.")]
        public string State { get; set; }
    }
}