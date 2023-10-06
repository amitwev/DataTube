using System.Text.Json;

namespace shared_library.Helpers;

public class JsonSerializer<T> : ISerialize<T>
{
    /// <summary>
    /// Receives an object and returns a Json String
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string Serialize(T obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Receives a string and returns the object that was specified at T
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public T Deserialize(string s)
    {
        return JsonSerializer.Deserialize<T>(s);
    }
}