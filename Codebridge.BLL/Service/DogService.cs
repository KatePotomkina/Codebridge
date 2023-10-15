using AspNetCoreRateLimit;
using AutoMapper;
using Codebridge.Codebridge.BLL.Repository;
using Codebridge.Data;
using Codebridge.DbContext;
using Codebridge.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace Codebridge.Service;

public class DogService : IDogService
{
    private readonly IMapper _mapper;
    private readonly IDogRepository _repository;

    public DogService(IDogRepository repository, IIpPolicyStore ipPolicyStore, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<Dog>> GetDogsList()
    {

        var dogs = await _repository.GetDogsList();

        return dogs;
    }

    public async Task<List<Dog>> SortedDogList(string attribute, string order, int pageNumber,
        int pageSize)
    {
        var sortedlist = await _repository.GetDogsList();

        if (attribute.Equals("weight"))
            sortedlist = order.Equals("desc")
                ? sortedlist.OrderByDescending(dog => dog.Weight).Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList()
                : sortedlist.OrderBy(dog => dog.Weight).Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

        if (attribute.Equals("tail_lenght"))
            sortedlist = order.Equals("desc")
                ? sortedlist.OrderByDescending(dog => dog.Weight).Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList()
                : sortedlist.OrderBy(dog => dog.Weight).Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

        return sortedlist;
    }

    public async Task<DogDto> CreateDogAsync(DogDto dto) 
    {
        if(dto == null) throw new ArgumentNullException();
    
        if(dto.Tail_Lenght < 0)
            throw new ArgumentOutOfRangeException();

        var dog = _mapper.Map<Dog>(dto);

        var existingDog = await _repository.GetDogsList();
        if (existingDog.Any(x => x.Name == dto.Name))
            throw new CustomExeption(dog.Name);

        await _repository.CreateDog(dog);
    
        return _mapper.Map<DogDto>(dog);
    }
}
