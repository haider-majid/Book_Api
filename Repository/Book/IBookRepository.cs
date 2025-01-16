using System;
using books.Model;

namespace books.Repository.book;

public interface IBookRepository
{
    Task<IEnumerable<BookModel>> GetAllBooksAsync(string? search = null, bool? orderBy = null, int pageSize = 1, int pageNumber = 1, Guid? categoryId = null);
    Task<BookModel> GetBookByIdAsync(Guid id);
    Task AddBookAsync(BookModel book);
    Task UpdateBookAsync(BookModel book);
    Task DeleteBookAsync(Guid id);
}

