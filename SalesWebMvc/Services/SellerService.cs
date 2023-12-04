using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {

        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task<Seller?> FindByIdAsync(int id) 
        {
            return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == id);
        }

        public async Task DeleteAsync(int id) 
        {   
            try
            {
                var seller = await FindByIdAsync(id);
                if(seller != null){
                    _context.Seller.Remove(seller);
                    await _context.SaveChangesAsync();
                } 
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }  
        }

        public async Task UpdateAsync(Seller seller)
        {
            if(!await _context.Seller.AnyAsync(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
