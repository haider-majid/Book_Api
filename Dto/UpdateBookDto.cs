using System;

namespace books.Dto;

public class UpdateBookDto
{

    public string? name { get; set; }
    public string? author { get; set; }
    public string? description { get; set; }
    public Guid? categoryId { get; set; }
    public IFormFile? Image { get; set; }


}
