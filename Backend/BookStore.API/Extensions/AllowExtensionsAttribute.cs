using BookStore.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Extensions
{
    public class AllowExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                //if (extension.ToLower() != ".jpg")
                //{
                //    return new ValidationResult("this photo extension is not allowed");
                //}
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult("this photo extension is not allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}
