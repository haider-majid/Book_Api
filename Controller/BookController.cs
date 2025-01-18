using AutoMapper;
using books.Dto;
using books.Model;
using books.Repository.book;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
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
        public async Task<IActionResult> GetBooks([FromQuery] string? search = null, [FromQuery] bool? orderBy = null, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] Guid? categoryId = null)
        {
            var books = await _bookRepository.GetAllBooksAsync(search, orderBy, pageSize, pageNumber, categoryId);

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

            var response = mapper.Map<BookDto>(book);
            return Ok(response);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromForm] AddBookModel book)
        {
            string? imagePath = null;

            if (book.Image != null)
            {
                var uploadsFolder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}_{book.Image.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await book.Image.CopyToAsync(fileStream);
                }

                imagePath = $"/images/{uniqueFileName}";
            }

            var newBook = new BookModel
            {
                name = book.name,
                author = book.author,
                description = book.description,
                categoryId = book.categoryId,
                ImagePath = imagePath
            };

            await _bookRepository.AddBookAsync(newBook);

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.id }, newBook);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto book)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Map updated fields
            mapper.Map(book, existingBook);

            await _bookRepository.UpdateBookAsync(existingBook);

            var response = mapper.Map<BookDto>(existingBook);
            return Ok(response);
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
