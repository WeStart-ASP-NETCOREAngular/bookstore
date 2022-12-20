using BookStore.API.Data;
using BookStore.API.DTOs;
using BookStore.API.Extensions;
using BookStore.API.Interfaces;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PublisherRepository(BookStoreDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Publisher> Add(Publisher entity)
        {
            await _context.Publishers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<List<Publisher>> GetAll(PaginationDTO paginationDTO)
        {
            //HttpContext
            //_httpContextAccessor.HttpContext

            var queryable = _context.Publishers.AsQueryable();
            // 1 select count(*) from publihsers => total count of rows;
            await _httpContextAccessor.HttpContext.InsertParametersPaginationHeaders(queryable);
            //return await _context.Publishers.ToListAsync();
            return await queryable.OrderBy(x=>x.Name).Paginate(paginationDTO).ToListAsync();
        }

        public async Task<Publisher> GetById(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<Publisher> Update(Publisher entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
