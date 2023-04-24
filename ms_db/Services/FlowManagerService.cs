using System.Text;
using shared_library.Helpers;
using shared_library.Models;

namespace ms_db.Services;

public class FlowManagerService
{
    private ISerialize<CompletedText> _serialize;
    
    public FlowManagerService(ISerialize<CompletedText> serialize)
    {
        _serialize = serialize;
    }

    public void RunFlow(byte[] bytes)
    {
        var completedText = _serialize.Deserialize(Encoding.UTF8.GetString(bytes));
    }
}