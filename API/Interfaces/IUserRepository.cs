using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllUsersAsync();
        Task<List<AppUser>> GetAllUsersAsync();
        Task<List<MemberDto>> GetAllMembersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByNameAsync(string username);
        Task<MemberDto> GetMemberByNameAsync(string username);
    }
}