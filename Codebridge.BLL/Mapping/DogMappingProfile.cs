using Codebridge.Data;
using Codebridge.Models;
using AutoMapper;

namespace Codebridge.Mapping;

public class DogMappingProfile:Profile
{
    public DogMappingProfile()
    {
        CreateMap<Dog, DogDto>();
    }
}