using System;
using System.Collections.Generic;
using Bloggr.Models;
using Bloggr.Repositories;

namespace Bloggr.Services
{
  public class CommentsService
  {
    private readonly CommentsRepository _commentsRepository;

    public CommentsService(CommentsRepository commentsRepository)
    {
      _commentsRepository = commentsRepository;
    }

    public Comment CreateComment(Comment commentData)
    {
      return _commentsRepository.CreateComment(commentData);
    }

    public Comment GetById(int commentId)
    {
      Comment foundComment = _commentsRepository.GetById(commentId);
      if(foundComment == null)
      {
        throw new Exception("Unable to find comment by that id moron");
      }
      return foundComment;
    }

    public Comment Update(int commentId, Comment commentData)
    {
      Comment foundComment = GetById(commentId);
      if(foundComment.CreatorId != commentData.CreatorId)
      {
        throw new Exception("this aint your comment fool");
      }
      commentData.Blog = foundComment.Blog;
      foundComment.Body = commentData.Body ?? foundComment.Body;
      foundComment.Title = commentData.Title ?? foundComment.Title;
      foundComment.Published = commentData.Published;

      return _commentsRepository.Update(commentId, commentData);
    }

    internal List<Comment> GetCommentByBlog(int blogId)
    {
      return _commentsRepository.GetCommentsByBlog(blogId);
    }

    internal void DeleteComment(int commentId, string userId)
    {
      Comment foundComment = GetById(commentId);
      if(foundComment.CreatorId != userId)
      {
        throw new Exception("The dark fire will not avail you!");
      }
      _commentsRepository.DeleteComment(commentId);
    }

    public List<Comment> GetCommentsByProfile(string accountId)
    {
      return _commentsRepository.GetCommentsByProfile(accountId);
    }

    public List<Comment> GetCommentsByAccount(string userId)
    {
      return _commentsRepository.GetCommentsByAccount(userId);
    }
  }
}