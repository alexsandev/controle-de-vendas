using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {

        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void Insert(Seller seller)
        {
            seller.Department = _context.Department.Where(d => d.Id == seller.Department.Id).First();
            _context.Add(seller);
            _context.SaveChanges();
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public Seller? FindById(int id) {
            return _context.Seller.FirstOrDefault(s => s.Id == id);
        }

        public void Delete(int id) {
            var obj = FindById(id);
            if(obj != null){
                _context.Seller.Remove(obj);
                _context.SaveChanges();
            } 
        }
    }
}
