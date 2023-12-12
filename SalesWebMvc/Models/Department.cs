using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(2, ErrorMessage = "O nome do departamento precisa conter no minimo dois caracteres.")]
        public string Name { get; set; }
        public ICollection<Seller> sellers { get; private set; } = new List<Seller>();

        public Department() 
        { 
        }

        public Department(string name)
        {
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return sellers.Sum(s => s.TotalSales(initial, final));
        }
    }
}
