using System;

namespace books.Model;

public class UpdateBookModel
{


    public string? name { get; set; } 

    public string? author { get; set; } 

    public string? description { get; set; }
    public Guid? categoryId { get; set; }
    
    public IFormFile? Image { get; set; }


}
