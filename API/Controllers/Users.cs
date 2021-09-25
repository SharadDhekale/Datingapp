using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entites;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class Users : ApiBaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public Users(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() 
        {
          var users= await _userRepository.GetAllMembersAsync();
            return Ok(users);
        } 

        [HttpGet("id")]
        [Authorize]
        public async Task<ActionResult<MemberDto>> GetUser(int id) {
            var user=await _userRepository.GetUserByIdAsync(id);
            var returnedUser= _mapper.Map<MemberDto>(user);
            return Ok(returnedUser);
        } 

        [HttpGet("username")]
        //[Authorize]
        public async Task<ActionResult<MemberDto>> GetUserByName(string username) => await _userRepository.GetMemberByNameAsync(username);
    }

}