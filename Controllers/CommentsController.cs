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
  public class CommentsController : ControllerBase
  {
    private readonly CommentsService _CommentsService;

    public CommentsController(CommentsService commentsService)
    {
      _CommentsService = commentsService;
    }

    [Authorize]
    [HttpPost]
    public async  Task<ActionResult<Comment>> CreateComment([FromBody] Comment commentData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        commentData.CreatorId = userInfo.Id;
        commentData.Creator = userInfo;
        var comment = _CommentsService.CreateComment(commentData);
        return Ok(comment);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{commentId}")]
    public async Task<ActionResult<Comment>> Update(int commentId, [FromBody] Comment commentData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        commentData.CreatorId = userInfo.Id;
        Comment comment = _CommentsService.Update(commentId, commentData);
        return Ok(comment);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}