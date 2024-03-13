using AutoMapper;
using demoAPI.Data;
using demoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace demoAPI.Repositories.Implement
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<int> AddBookAsync(BookModel model)
        {
            var newBook = _mapper.Map<Book>(model);
            _context.Books!.Add(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task DeleteBookAsync(int Id)
        {
            var deteleBook = _context.Books!.SingleOrDefault(b => b.Id == Id);
            if (deteleBook != null)
            {
                _context.Books!.Remove(deteleBook);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<BookModel>> getAllBooksAsync()
        {
            var books = await _context.Books!.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> getBookAsync(int Id)
        {
            var book = await _context.Books!.FindAsync(Id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task UpdateBookAsysnc(int Id, BookModel model)
        {
            if (Id == model.Id)
            {
                var updataBook = _mapper.Map<Book>(model);
                _context.Books!.Update(updataBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
