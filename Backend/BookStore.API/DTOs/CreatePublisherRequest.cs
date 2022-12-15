using BookStore.API.Extensions;
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
        [AllowExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; }


    }
}

