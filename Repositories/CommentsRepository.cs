using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bloggr.Models;
using Dapper;

namespace Bloggr.Repositories
{
  public class CommentsRepository
  {
    private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Comment CreateComment(Comment commentData)
    {
      var sql = @"
      INSERT INTO comments(
        creatorId,
        body,
        blog
      )
      VALUES (
        @CreatorId,
        @Body,
        @Blog
      );
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, commentData);
      commentData.Id = id;
      return commentData;
    }

    internal Comment GetById(int commentId)
    {
      string sql = @"
      SELECT * FROM comments WHERE id = @commentId;
      ";
      return _db.QueryFirstOrDefault<Comment>(sql, new {commentId});
    }

    internal Comment Update(int commentId, Comment commentData)
    {
      commentData.Id = commentId;
      string sql = @"
      UPDATE comments
      SET
      body = @Body
      WHERE id = @Id
      ";

      var rowsAffected = _db.Execute(sql, commentData);
      if(rowsAffected == 0)
      {
        throw new System.Exception("The update did work idiot");
      }
      if(rowsAffected > 1)
      {
        throw new System.Exception("You done did mess up a-a-ron");
      }
      return commentData;
    }

    internal List<Comment> GetCommentsByBlog(int blogId)
    {
      string sql = @"
      SELECT * FROM comments c WHERE c.blog = @blogId
      ";
      return _db.Query<Comment>(sql, new{blogId}).ToList();
    }
  }
}