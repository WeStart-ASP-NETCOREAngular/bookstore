using BookStore.API.Models;

namespace BookStore.API.Interfaces
{
    public interface IPublisherRepository
    {
        Task<Publisher> Add(Publisher entity);
        Task<Publisher> Update(Publisher entity);
        Task<Publisher> GetById(int id);
        Task<List<Publisher>> GetAll();
    }

}
