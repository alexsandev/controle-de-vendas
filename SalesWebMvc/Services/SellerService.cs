using Microsoft.EntityFrameworkCore;
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
            return _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == id);
        }

        public void Delete(int id) {
            var seller = FindById(id);
            if(seller != null){
                _context.Seller.Remove(seller);
                _context.SaveChanges();
            } 
        }
    }
}
