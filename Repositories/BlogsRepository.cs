using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bloggr.Models;
using Dapper;

namespace Bloggr.Repositories
{
  public class BlogsRepository
  {
    private readonly IDbConnection _db;

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Blog> GetAll()
    {
      string sql = @"
      SELECT * FROM blogs WHERE published = 1;
      ";
      return _db.Query<Blog>(sql).ToList();
    }

    internal Blog Post(Blog blogData)
    {
      string sql = @"
      INSERT INTO blogs(title, body, imgUrl, published, creatorId)
      VALUES(@Title, @Body, @ImgUrl, @Published, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, blogData);
      blogData.Id = id;
      return blogData;
    }

    internal Blog GetById(int blogId)
    {
      string sql = @"
      SELECT
      b.*,
      a.*
      FROM blogs b
      JOIN accounts a on a.id = b.creatorId
      WHERE b.id = @blogId;
      ";
      return _db.Query<Blog, Profile, Blog>(sql, (b, a) =>
      {
          b.Creator = a;
          return b;
      }, new{blogId}).FirstOrDefault();
    }

    internal Blog Update(int blogId, Blog blogData)
    {
      blogData.Id = blogId;
      string sql = @"
      UPDATE blogs
      SET
      title = @Title,
      body = @Body,
      imgUrl = @ImgUrl,
      published = @Published
      WHERE id = @Id
      ";

      var rowsAffected = _db.Execute(sql, blogData);
      if(rowsAffected == 0)
      {
        throw new System.Exception("The update did work idiot");
      }
      if(rowsAffected > 1)
      {
        throw new System.Exception("You done did mess up a-a-ron");
      }
      return blogData;
    }

    public List<Blog> GetBlogsByAccount(string userId)
    {
      var sql = @"
      SELECT
      * FROM blogs
      WHERE creatorId = @userId AND published = 1;
      ";
      return _db.Query<Blog>(sql, new{userId}).ToList();
    }

    internal List<Blog> GetBlogsByProfile(string accountId)
    {
      var sql = @"
      SELECT * FROM blogs 
      WHERE creatorId = @accountId AND published = 1;
      ";
      return _db.Query<Blog>(sql, new{accountId}).ToList();
    }

    internal void DeleteBlog(int blogId)
    {
      string sql = "DELETE FROM blogs WHERE id = @blogId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new{blogId});
      if(affectedRows == 0)
      {
        throw new Exception("Could delete that blog because someone was an dumb.");
      }
    }
  }
}