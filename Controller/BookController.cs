using AutoMapper;
using books.Dto;
using books.Model;
using books.Repository.book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    //[Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string? search = null, [FromQuery] bool? orderBy = null, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1 , [FromQuery] Guid? categoryId = null)


        {
            var books = await _bookRepository.GetAllBooksAsync(search, orderBy, pageSize, pageNumber , categoryId);

            var response = mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookModel book)
        {

            var newBook = new BookModel
            {
                name = book.name,
                author = book.author,
                description = book.description,
                categoryId = book.categoryId
            };

            await _bookRepository.AddBookAsync(newBook);

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.id }, newBook);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto book)
        {
            // Fetch the existing book by ID
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Map other properties but do not modify the ID
            mapper.Map(book, existingBook);

            // Save the updated entity
            await _bookRepository.UpdateBookAsync(existingBook);
            return Ok(existingBook);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
