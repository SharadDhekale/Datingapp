using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        DataContext _dataContext;
        private readonly IMapper _mapper;
        public UserRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<MemberDto>> GetAllMembersAsync()
        {
            var users = await _dataContext.Users
                                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                   .ToListAsync();
            return users;
        }
    
        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            var test = _dataContext.Users.Include(x => x.Photos).ToListAsync();

            return await _dataContext.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<MemberDto> GetMemberByNameAsync(string username)
        {
            return await _dataContext.Users
                                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync();

        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> SaveAllUsersAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}