using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using user.DTO;
using user.Exceptions;
using user.Models;
using user.RabbitMQ;

namespace user.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public UserRepository(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;

        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userDbContext.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userDbContext.Users.ToListAsync();
        }

        public async Task AddAsync(User entity)
        {
            await _userDbContext.Users.AddAsync(entity);
            await _userDbContext.SaveChangesAsync();
            #if !TESTING
                var userCreateService = new UserCreateService();

                userCreateService.SendMessage(entity, "user-receive-elasticsearch");
                userCreateService.SendMessage(entity, "user-receive-sms");
                // userCreateService.SendMessage(entity, "user-receive-domain");

            #endif
        }


        public async Task UpdateAsync(User entity)
        {
            _userDbContext.Users.Update(entity);
            await _userDbContext.SaveChangesAsync();
            #if !TESTING
                var userUpdateService = new UserUpdateService();
                // userUpdateService.SendMessage(entity);
                userUpdateService.SendMessage(entity, "user-update-elasticsearch");
                userUpdateService.SendMessage(entity, "user-update-sms");

            #endif
        }

        public async Task DeleteAsync(User entity)
        {
            _userDbContext.Users.Remove(entity);
            await _userDbContext.SaveChangesAsync();
            #if !TESTING
                var userDeleteService = new UserDeleteService();
                userDeleteService.SendMessage(entity);
            #endif
        }
    }
}