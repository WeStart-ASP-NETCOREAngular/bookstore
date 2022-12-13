using BookStore.API.Exentsions;
using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
    public class CreatePublisherRequest
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [AdaptIgnore]
        public IFormFile Image { get; set; }

    }
}
