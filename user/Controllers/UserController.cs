using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using user.Data;
using user.DTO;
using user.Models;
using AutoMapper;
using user.Exceptions;
using Bogus;

namespace user.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("hoi")]
        public async Task<IActionResult> AddUserFake()
        {
            var faker = new Faker();

            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Email = faker.Internet.Email(),
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                    UserName = faker.Internet.UserName()
                };
                var entity = _mapper.Map<User>(user);
                entity.Guid = Guid.NewGuid();

                await _userRepository.AddAsync(entity);
            }

            return Ok("Users added successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userList = await _userRepository.GetAllAsync();
            return Ok(userList);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] InsertUser insertEntity)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entity = _mapper.Map<User>(insertEntity);
                entity.Guid = Guid.NewGuid();

                await _userRepository.AddAsync(entity);
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {

                // Log the exception or handle it accordingly
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUser updateEntity)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _mapper.Map(updateEntity, user); // Map properties from updateEntity to user

                await _userRepository.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var entity = await _userRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }

                await _userRepository.DeleteAsync(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}