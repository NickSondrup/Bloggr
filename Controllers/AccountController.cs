using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bloggr.Models;
using Bloggr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly BlogsService _blogsService;
        private readonly CommentsService _commentsService;

        public AccountController(AccountService accountService, BlogsService blogsService, CommentsService commentsService)
        {
            _accountService = accountService;
            _blogsService = blogsService;
            _commentsService = commentsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("blogs")]
        public async Task<ActionResult<List<Blog>>> GetBlogs()
        {
            try
            {
                 Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                 return Ok(_blogsService.GetBlogsByAccount(userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpGet("comments")]
        public async Task<ActionResult<List<Comment>>> GetComments()
        {
            try
            {
                 Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                 return Ok(_commentsService.GetCommentsByAccount(userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Account>> UpdateAccount([FromBody] Account editData)
        {
            try
            {
                 Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                 return _accountService.Edit(editData, userInfo.Email);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}