using System;
using System.ComponentModel.DataAnnotations;
using books.Dto;

namespace books.Model;

public class BookModel
{
    public Guid id { get; set; }

    public string? name { get; set; }


    public string? author { get; set; }

    public string? description { get; set; }
    public Guid categoryId { get; set; }
    
    public string? ImagePath { get; set; }


}

