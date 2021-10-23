using System;
using Bloggr.Models;
using Bloggr.Repositories;

namespace Bloggr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _ProfilesRepository;

    public ProfilesService(ProfilesRepository profilesRepository)
    {
      _ProfilesRepository = profilesRepository;
    }

    public Profile Get(string accountId)
    {
      Profile foundProfile = _ProfilesRepository.Get(accountId);
      if(foundProfile == null)
      {
        throw new Exception("Couldn't find profile by that Id fool");
      }
      return foundProfile;
    }
  }
}