using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Department department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Department?> FindByIdAsync(int id)
        {
            return await _context.Department.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Department department)
        {
            if (!await _context.Department.AnyAsync(x => x.Id == department.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var department = await FindByIdAsync(id);
                if (department != null)
                {
                    _context.Department.Remove(department);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
