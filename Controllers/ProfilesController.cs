using Bloggr.Models;
using Bloggr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ProfileService;

    public ProfilesController(ProfilesService profileService)
    {
      _ProfileService = profileService;
    }

    [HttpGet("{accountId}")]
    public ActionResult<Profile> GetProfiles(string accountId)
    {
      try
      {
        var profile = _ProfileService.Get(accountId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    } 
  }
}