using System.Net;
using api.Models;
using shared_library.Models;

namespace api.Services;

public class AddDataService
{
    private static Random random;
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private int randomStringLength;
    private const string BASE_IP = "10.0.0.0";
    private const string IP_KEY = "x-forwarded-for";


    public AddDataService()
    {
        random = new Random();
        randomStringLength = 8;
    }

    public CompletedText TextDtoToTextComplete(TextDTO textDto, IHeaderDictionary headerDictionary)
    {
        string? author = textDto.Author;
        
        if (author == null)
        {
            var key = IP_KEY;
            author = BASE_IP; // Base Author if no IP was found
            
            if (headerDictionary.ContainsKey(key))
            {
                IPAddress? ipAddress = null;
                var headerValues = headerDictionary[key];
                var ipn = headerValues.FirstOrDefault()?.Split(new char[] { ',' }).FirstOrDefault()?.Split(new char[] { ':' }).FirstOrDefault();
                if (IPAddress.TryParse(ipn, out ipAddress))
                {
                    author = ipAddress.ToString();
                }
            }
        }
        
        // Generate a random ID using Guid
        var id = Guid.NewGuid().ToString();
        // Create and return a new CompletedText Object
        return new CompletedText(id, textDto.Title, textDto.TextData, author);
    }
}