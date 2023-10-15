using Codebridge.Codebridge.BLL.Repository;
using Codebridge.Data;
using Codebridge.DbContext;
using Codebridge.Models;
using Microsoft.EntityFrameworkCore;

public class DogRepository : IDogRepository
{
    private readonly DogDbContext _context;

    public DogRepository(DogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Dog>> GetDogsList()
    {
        return await _context.Dogs.ToListAsync();
    }

   

    public async Task<Dog> CreateDog(Dog newDog)
    {
        var createdDog = new Dog
        {
             Name= newDog.Name,
            Colour = newDog.Colour,
            Tail_Lenght = newDog.Tail_Lenght,
            Weight = newDog.Weight
        };

        var newItem = await _context.Dogs.AddAsync(createdDog);
        await _context.SaveChangesAsync();

        return newItem.Entity;
    }
}