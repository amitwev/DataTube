namespace shared_library.Helpers;

public interface ISerialize<T>
{
    public string Serialize(T obj);

    public T Deserialize(string s);
}