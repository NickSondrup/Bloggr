using System;
using System.Collections.Generic;
using Bloggr.Repositories;

namespace Bloggr.Services
{
  public class BlogsService
  {
    private readonly BlogsRepository _blogsRepository;

    public BlogsService(BlogsRepository blogsRepository)
    {
      _blogsRepository = blogsRepository;
    }

    public List<Blog> GetAll()
    {
      return _blogsRepository.GetAll();
    }

    public Blog Post(Blog blogData)
    {
      return _blogsRepository.Post(blogData);
    }

    public Blog GetById(int blogId)
    {
      Blog foundBlog = _blogsRepository.GetById(blogId);
      if(foundBlog == null)
      {
        throw new Exception("Don't got that Id fool");
      }
      return foundBlog;
    }

    public Blog Update(int blogId, Blog blogData)
    {
      Blog foundBlog = GetById(blogId);
      if(foundBlog.CreatorId != blogData.CreatorId)
      {
        throw new Exception("this ain't your blog dummy!");
      }
      foundBlog.Title = blogData.Title ?? foundBlog.Title;
      foundBlog.Body = blogData.Body ?? foundBlog.Body;
      foundBlog.ImgUrl = blogData.ImgUrl ?? foundBlog.ImgUrl;
      foundBlog.Published = blogData.Published;

      return _blogsRepository.Update(blogId, blogData);
    }

    public List<Blog> GetBlogsByProfile(string accountId)
    {
      return _blogsRepository.GetBlogsByProfile(accountId);
    }

    public void DeleteBlog(int blogId, string userId)
    {
      Blog foundBlog = GetById(blogId);
      if(foundBlog.CreatorId != userId)
      {
        throw new Exception("this seems to not be your blog numbskull");
      }
      _blogsRepository.DeleteBlog(blogId);
    }

    public List<Blog> GetBlogsByAccount(string userId)
    {
      return _blogsRepository.GetBlogsByAccount(userId);
    }
  }
}