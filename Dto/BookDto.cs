using System;
using books.Model;

namespace books.Dto;

public class BookDto
{
    public Guid id { get; set; }

    public string name { get; set; } = string.Empty;

    public string author { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;
    public Guid categoryId { get; set; }
    public string? ImagePath { get; set; }    


}
