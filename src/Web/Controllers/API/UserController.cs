using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Entities;
using Dashboard.Web.Models;
using Dashboard.Web.Models.User;
using Dashboard.Web.Interfaces.Services;
using Dashboard.Web.Services;

namespace Dashboard.Web.Controllers.API;

[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    private readonly IUserService _userService;
    
    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommonResponseDto<UserDto>>> Get(string id)
    {
        var user = await _userService.Get(Guid.Parse(id));
        
        if (user == null)
        {
            return new CommonResponseDto<UserDto>() {
                statusCode = "404",
                Message = "User not found"
            };
        }

        return new CommonResponseDto<UserDto>() {
            statusCode = "0000",
            Message = "Success",
            data = new UserDto() {
                id = user.id,
                firstName = user.firstName,
                lastName = user.lastName,
                gender = user.gender,
                birthday = DateOnly.Parse(user.birthday.ToString("yyyy-MM-dd")),
                status = user.status,
                email = user.email,
                phone = user.phone,
                createdAt = user.createdAt,
                updatedAt = user.updatedAt
            }
        };
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResponseDto>> Create([FromBody] CreateUserRequestDto userDto)
    {
        if(ModelState.IsValid == false)
        {
            _logger.LogInformation(ModelState.Keys.ToString());
            return BadRequest(ModelState);
        }

        await _userService.Create(userDto);

        var response = new CreateUserResponseDto() {
            statusCode = "000",
            statusMessage = "User created successfully"
        };

        return response;
    }
}