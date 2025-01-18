using System;
using books.Model;
using Microsoft.EntityFrameworkCore;

namespace books.Enitity;

public class BookDbContext : DbContext
{

    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }
    public DbSet<BookModel> Books { get; set; }
    public DbSet<CategoryModel> categories { get; set; }

   

}
