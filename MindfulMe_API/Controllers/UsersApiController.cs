using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Entities.ViewModels.User;

namespace MindfulMe.Controllers
{
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/user_list")]
        public ActionResult<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            var userViewModels = users.Select(user => new UserViewModel
            {
                Password = user.Password,
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                UserType = user.UserType
            }).ToList();

            return Ok(userViewModels);
        }

        [HttpGet]
        [Route("/get_user/{id}")]
        public ActionResult<UserViewModel> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                Password = user.Password,
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                UserType = user.UserType
            };

            return Ok(userViewModel);
        }

        [HttpPost]
        [Route("/create_user")]
        public ActionResult CreateUser([FromBody] CreateUserViewModel createUserViewModel)
        {
            if (createUserViewModel == null)
            {
                return BadRequest();
            }

            var newUser = new User
            {
                Email = createUserViewModel.Email,
                Password = createUserViewModel.Password,
                UserName = createUserViewModel.UserName,
                UserType = createUserViewModel.UserType
            };

            _userService.CreateUser(newUser);

            return Ok();
        }

        [HttpPut]
        [Route("/update_user/{id}")]
        public ActionResult UpdateUser(int id, [FromBody] UpdateUserViewModel updateUserViewModel)
        {
            if (updateUserViewModel == null)
            {
                return BadRequest();
            }

            var existingUser = _userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Email = updateUserViewModel.Email;
            existingUser.Password = updateUserViewModel.Password;
            existingUser.UserName = updateUserViewModel.UserName;
            existingUser.UserType = updateUserViewModel.UserType;

            _userService.UpdateUser(existingUser);

            return Ok();
        }

        [HttpDelete]
        [Route("/delete_user/{id}")]
        public ActionResult DeleteUser(int id)
        {
            var existingUser = _userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(id);

            return Ok();
        }
    }
}
