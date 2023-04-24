using shared_library.Models;

namespace ms_db.Interfaces;

public delegate void OnConsume(byte[] byteArray);

public interface IConsumer
{
    void Start();

    void Stop();

    event OnConsume OnConsume;

}