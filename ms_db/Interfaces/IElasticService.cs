using shared_library.Models;

namespace ms_db.Interfaces;

public interface IElasticService
{
    public CompletedText Create(CompletedText completedText);

    public CompletedText Read(string id);

    public CompletedText Update(CompletedText completedText);

    public void Delete(string id);

    public List<CompletedText> Serach(List<string> keywords);
}