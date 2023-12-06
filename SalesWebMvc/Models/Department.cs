namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> sellers { get; private set; } = new List<Seller>();

        public Department() 
        { 
        }

        public Department(int id, string name)
        {
            Id = id;
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
