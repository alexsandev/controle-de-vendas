using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Username { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Password { get; set; }

        [Display(Name = "Manter conectado")]
        public bool StayConnected { get; set; }

        public string? ReturnUrl { get; set; }
    }
}