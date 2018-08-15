using System.ComponentModel.DataAnnotations;

namespace br.com.sagradacruz.Models
{
    public class LoginViewModel
    {
        [Required]
        public string User { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
