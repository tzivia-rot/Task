using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.model;
using Tasks.service;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
         IUserHttp Iuserhttp;

        public UserController(IUserHttp iuserhttp) {
            this.Iuserhttp=iuserhttp;
        }
 
        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
            User Exists=Iuserhttp.Login(User);

            if(Exists==null){
                return Unauthorized();
            }
            var claims = new List<Claim>();
            if(User.IsAdmin) {
                claims.Add(new Claim("type", "Admin"));
            }
            else {
                claims.Add(new Claim("type", "User"));
            }
            claims.Add(new Claim("id",User.UserId.ToString()));
            claims.Add(new Claim("name",User.UserName.ToString()));
            var token = UserTokenService.GetToken(claims);
            
            return new OkObjectResult(UserTokenService.WriteToken(token));
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]

        public IEnumerable<User> Get()
        {
            return Iuserhttp.GetAll();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Delete (int id)
        {
            if (! Iuserhttp.Delete(id))
                return NotFound();
            return NoContent();            
        }
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult Post( User user)
        {
            Iuserhttp.Add(user);
            return CreatedAtAction(nameof(Post), new { id = user.UserId }, user);
        }
           
    }}
 
    //     [HttpPost]
    //     [Route("[action]")]
    //     [Authorize(Policy = "Admin")]
    //     public IActionResult GenerateBadge([FromBody] Agent Agent)
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new Claim("type", "Agent"),
    //             new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString()),
    //         };

    //         var token = FbiTokenService.GetToken(claims);

    //         return new OkObjectResult(FbiTokenService.WriteToken(token));
    //     }
    // }




