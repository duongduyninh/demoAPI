using demoAPI.Models;
using demoAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public ProductsController(IBookRepository repo)
        {
            _bookRepo = repo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                return Ok(await _bookRepo.getAllBooksAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBookById(int Id)
        {
            var book = await _bookRepo.getBookAsync(Id);
            return book == null ? NotFound() : Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            try
            {
                var newBookId = await _bookRepo.AddBookAsync(model);
                return Ok("Book added successfully!");
               /* var book = await _bookRepo.getBookAsync(newBookId);
                return book == null ? NotFound() : Ok(book);
               */
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{Id}")]

        public async Task<IActionResult> UpdateBook(int Id , [FromBody] BookModel model)
        {
            if(Id != model.Id)
            {
                return NotFound();
            }
            try
            {
                await _bookRepo.UpdateBookAsysnc(Id, model);
                return Ok("Book update successfully!");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]


        public async Task<IActionResult> DeleteBook([FromBody] int Id)
        {
            try
            {
                await _bookRepo.DeleteBookAsync(Id);
                return Ok("Book delete successfully!");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
