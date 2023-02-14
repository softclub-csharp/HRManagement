using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _context;

    public AccountService(IConfiguration configuration, UserManager<IdentityUser> userManager, DataContext context)
    {
       
        _configuration = configuration;
        _userManager = userManager;
        _context = context;
    }
    
    public async Task<Response<IdentityResult>> Register(RegisterDto registerDto)
    {
        
        var user = new IdentityUser()
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
        };
        var result = await _userManager.CreateAsync(user,registerDto.Password);
        return new Response<IdentityResult>(result);
    }

    public async Task<Response<IdentityResult>> Login(LoginDto model)
    {
        var existing = await _userManager.FindByEmailAsync(model.Email);
        if (existing == null)
            return new Response<IdentityResult>(HttpStatusCode.BadRequest, new List<string>(){"Incorrect passsword or login"});

        var check = await _userManager.CheckPasswordAsync(existing, model.Password);
    if(check == true)
        return new Response<IdentityResult>(HttpStatusCode.OK,new List<string>());
    else
        return new Response<IdentityResult>(HttpStatusCode.BadRequest,new List<string>());
    }



}