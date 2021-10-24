using System.Collections.Generic;
using Bloggr.Models;
using Bloggr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  // REVIEW ProfileController may need to be switched to ProfilesController
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ProfileService;
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;
    public ProfilesController(ProfilesService profileService, BlogsService blogsService, CommentsService commentsService)
    {
      _ProfileService = profileService;
      _blogsService = blogsService;
      _commentsService = commentsService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfiles(string profileId)
    {
      try
      {
        var profile = _ProfileService.Get(profileId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    } 

    [HttpGet("{accountId}/blogs")]
    public ActionResult<List<Blog>> GetBlogsByProfile(string accountId)
    {
      try
      {
        var blogs = _blogsService.GetBlogsByProfile(accountId);
        return Ok(blogs);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{accountId}/comments")]
    public ActionResult<List<Comment>> GetCommentsProfile(string accountId)
    {
      try
      {
        var comments = _commentsService.GetCommentsByProfile(accountId);
        return Ok(comments);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}