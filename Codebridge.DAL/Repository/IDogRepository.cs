using Codebridge.Data;
using Codebridge.Models;

namespace Codebridge.Codebridge.BLL.Repository;

public interface IDogRepository
{
  Task<List<Dog>> GetDogsList();
    Task<Dog> CreateDog(Dog dog);
}