


namespace books.Model;

public class AddBookModel
{

    public string name { get; set; } = string.Empty;

    public string author { get; set; } = string.Empty;
 
    public string description { get; set; } = string.Empty;
    public Guid categoryId { get; set; }
    public IFormFile? Image { get; set; }

}
