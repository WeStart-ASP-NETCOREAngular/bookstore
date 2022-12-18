using BookStore.API.DTOs;
using BookStore.API.Models;
using Mapster;

namespace BookStore.API.Mapping
{
    public class RegistrationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, AppUser>()
                .Map(dest => dest.UserName, src => src.Email);
        }
    }
}
