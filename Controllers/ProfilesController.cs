using Bloggr.Models;
using Bloggr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  // REVIEW ProfileController may need to be switched to ProfilesController
  public class ProfileController : ControllerBase
  {
    private readonly ProfilesService _ProfileService;
    public ProfileController(ProfilesService profileService)
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