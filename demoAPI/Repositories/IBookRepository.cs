using demoAPI.Data;
using demoAPI.Models;

namespace demoAPI.Repositories
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> getAllBooksAsync();
        public Task<BookModel> getBookAsync(int Id);
        public Task<int> AddBookAsync(BookModel model);
        public Task UpdateBookAsysnc(int Id , BookModel model);    
        public Task DeleteBookAsync(int Id );
    }
}
