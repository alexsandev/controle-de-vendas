using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Username { get; set; }

        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Por favor, entre com um e-mail válido.")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(16, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo do campo {0} é de {1} caracteres.")]
        public string? Password { get; set; }

        [Display(Name = "Confirmação de senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(16, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo do campo {0} é de {1} caracteres.")]
        [Compare(nameof(Password), ErrorMessage = "A confirmação da senha não confere com a senha.")]
        public string? PasswordConfirmation { get; set; }
    }
}