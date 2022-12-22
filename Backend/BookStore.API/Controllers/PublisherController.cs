using BookStore.API.DTOs;
using BookStore.API.Interfaces;
using BookStore.API.Models;
using BookStore.API.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.API.Controllers;

[Route("api/publisher")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly IPublisherRepository _repository;
    private readonly IImageUploaderService _uploaderService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public PublisherController(IPublisherRepository repository,
        IImageUploaderService uploaderService,
        IMapper mapper, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _uploaderService = uploaderService;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] PaginationDTO paginationDTO)
    {
        var userEmail = HttpContext.User.Claims
            .FirstOrDefault(x=>x.Type == ClaimTypes.Name).Value;
        var userId = await _userManager.FindByEmailAsync(userEmail);

        //userId.Id;

        //HttpContext.Headers -> 1
        return Ok(await _repository.GetAll(paginationDTO));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreatePublisherRequest request)
    {
        var publisher = _mapper.Map<Publisher>(request);

        publisher.Logo = await _uploaderService.UploadImageAsync(request.Image);

        return Ok(await _repository.Add(publisher));
    }
}
