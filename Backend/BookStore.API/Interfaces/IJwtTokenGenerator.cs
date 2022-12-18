using BookStore.API.DTOs;
using BookStore.API.Models;

namespace BookStore.API.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<AuthenticationResponse> Generate(AppUser user);

    }
}
