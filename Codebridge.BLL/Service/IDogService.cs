using AspNetCoreRateLimit;
using Codebridge.Data;
using Codebridge.Models;

namespace Codebridge.Service;

public interface IDogService
{
    Task<List<Dog>> GetDogsList();
    Task<DogDto> CreateDogAsync(DogDto dto);
    Task<List<Dog>> SortedDogList(string attribute, string order, int pageNumber, int pageSize);
    /*
    Task InvokeAsync(HttpContext context, IIpPolicyStore store);
*/
}
