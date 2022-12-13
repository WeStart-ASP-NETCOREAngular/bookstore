using BookStore.API.DTOs;
using BookStore.API.Models;
using Mapster;

namespace BookStore.API.Mapping
{
    public class PublisherMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreatePublisherRequest, Publisher>()
                .Map(dest => dest.Name, src => src.Name);
        }
    }
}
