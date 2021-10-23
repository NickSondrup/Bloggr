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
  [Route("api/[controller]")]
  public class BlogsController : ControllerBase
  {
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;
    public BlogsController(BlogsService blogsService, CommentsService commentsService)
    {
      _blogsService = blogsService;
      _commentsService = commentsService;
    }

    [HttpGet]
    public ActionResult<List<Blog>> GetAll()
    {
      try
      {
        return Ok(_blogsService.GetAll());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    
    [HttpGet("{blogId}")]
    public ActionResult<Blog> GetById(int blogId)
    {
      try
      {
        return Ok(_blogsService.GetById(blogId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{blogId}/comments")]
    public ActionResult<List<Comment>> getCommentsByBlog(int blogId)
    {
      try
      {
        return Ok(_commentsService.GetCommentByBlog(blogId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Blog>> Post([FromBody] Blog blogData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        blogData.CreatorId = userInfo.Id;
        blogData.Creator = userInfo;
        Blog createdBlog = _blogsService.Post(blogData);
        return createdBlog;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{blogId}")]
    public async Task<ActionResult<Blog>> Update(int blogId, [FromBody] Blog blogData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        blogData.CreatorId = userInfo.Id;
        Blog blog = _blogsService.Update(blogId, blogData);
        return Ok(blog);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{blogId}")]
    public async Task<ActionResult<string>> DeleteBlog(int blogId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _blogsService.DeleteBlog(blogId, userInfo.Id);
        return Ok("Blog has been ground into oblivion");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}