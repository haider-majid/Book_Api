using System;
using System.ComponentModel.DataAnnotations;

namespace books.Dto;

public class AddBookDto
{
    public Guid id { get; set; }
    [MaxLength(20)]
    public string name { get; set; } = string.Empty;
    public string author { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public Guid categoryId { get; set; }
    public string? imageUrl { get; set; } 


}
