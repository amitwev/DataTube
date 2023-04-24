namespace shared_library.Helpers;

public interface ISerialize<T>
{
    public string Serizlie(T obj);

    public T Deserialize(string s);
}