using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stupify.Model;

namespace Stupify.Services
{
    public class UserLikeService : IRepository<UserLike>
    {
        private readonly ApplicationContext context;

        public UserLikeService(ApplicationContext context)
        {
            this.context = context;
        }

        public List<UserLike> GetList()
        {
            return context.UserLikes.Include(u => u.Song).ToList();
        }

        public UserLike Get(int id)
        {
            return context.UserLikes
                .Where(u => u.Id == id)
                .Include(u => u.Song)
                .FirstOrDefault();
        }

        public void Create(UserLike newUserLike)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.UserLikes.Add(newUserLike);
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void Update(UserLike updatedUserLike)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Entry(updatedUserLike).State = EntityState.Modified;
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void Delete(int id)
        {
            var userLikeToDelete = context.UserLikes
                .Where(x => x.Id == id)
                .FirstOrDefault();

            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.UserLikes.Remove(userLikeToDelete);
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }
    }
}