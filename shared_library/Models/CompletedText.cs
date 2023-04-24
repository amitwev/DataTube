namespace shared_library.Models;

public record CompletedText
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string TextData { get; init; }
    public string Author { get; init; }
    public DateTime Date { get; } 

    public CompletedText(string id, string title, string textData, string author)
    {
        Id = id;
        Title = title;
        TextData = textData;
        Author = author;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return 
            $"Id = {Id}{Environment.NewLine}"
            +$"Title = {Title}{Environment.NewLine}" 
            +$"TextData = {TextData}{Environment.NewLine}" 
            +$"Date = {Date}{Environment.NewLine}" 
            +$"Author = {Author}";
    }
}