using BookStore.API.DTOs;
using BookStore.API.Interfaces;
using BookStore.API.Models;
using BookStore.API.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[Route("api/publisher")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly IPublisherRepository _repository;
    private readonly IImageUploaderService _uploaderService;
    private readonly IMapper _mapper;

    public PublisherController(IPublisherRepository repository,
        IImageUploaderService uploaderService,
        IMapper mapper)
    {
        _repository = repository;
        _uploaderService = uploaderService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreatePublisherRequest request)
    {
        var publisher = _mapper.Map<Publisher>(request);

        publisher.Logo = await _uploaderService.UploadImageAsync(request.Image);

        return Ok(await _repository.Add(publisher));
    }
}
