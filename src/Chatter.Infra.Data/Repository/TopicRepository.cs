using Chatter.Domain.Topics;
using Chatter.Domain.Topics.Repository;
using Chatter.Infra.Data.Context;
using Chatter.Infra.Data.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Chatter.Domain.Categories;
using Chatter.Domain.Users;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace Chatter.Infra.Data.Repository
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(ChatterContext db) : base(db)
        {

        }

        public override IEnumerable<Topic> GetAll(bool activeOnly = false)
        {
            var query = @"SELECT T.Id
                                ,T.Created
                                ,T.Title
	                            ,U.Name
	                            ,C.Name
                            FROM Topics T
                            INNER JOIN Users U ON T.UserId = U.Id
                            INNER JOIN Categories C ON T.CategoryId = C.Id";
            if(activeOnly) query += " WHERE T.Active = 1";

            var topics = Db.Database.GetDbConnection().Query<Topic, User, Category, Topic>(query,
                (topic, user, category) =>
                {
                    topic.SetUSer(user);
                    topic.SetCategory(category);
                    return topic;
                }, splitOn: "Name, Name");
            return topics;
        }

        // Posts
        public Post AddPost(Post post)
        {
            return Db.Posts.Add(post).Entity;
        }

        public void UpdatePost(Post post)
        {
            Db.Posts.Update(post);
        }

        public Post GetPost(int id)
        {
            const string query = @"SELECT TOP 1 P.*
                                  FROM Posts P
                                  WHERE P.Id = @_id";
            var postObj = Db.Database.GetDbConnection().QueryFirstOrDefault<Post>(query, new { _id = id });
            return postObj;
        }

        public void RemovePost(int id)
        {
            var post = Db.Posts.Find(id);
            post.SetRemoved();
            Db.Posts.Update(post);
        }

        public List<Post> GetPostsFromTopic(int topicId)
        {
            const string query = @"SELECT P.Id
                                      ,P.Created
                                      ,P.Text
                                      ,U.Name
                                  FROM Posts P
                                  INNER JOIN Topics T ON P.TopicId = T.Id
                                  INNER JOIN Users U ON P.UserId = U.Id
                                  WHERE P.Removed = 0 AND 
                                  P.TopicId = @_topicId";
            var posts = Db.Database.GetDbConnection().Query<Post, User, Post>(query,
                (post, user) =>
                {
                    post.SetUser(user);
                    return post;
                }, new { _topicId = topicId }, null, true, "Name");

            return posts.ToList();
        }

        public override void Remove(int id)
        {
            var topic = Get(id);
            topic.SetRemoved();
            Update(topic);
        }
        
        
    }
}