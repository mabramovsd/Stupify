using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stupify.Model;

namespace Stupify.Services
{
    public class UserService : IRepository<User>
    {
        private readonly ApplicationContext context;

        public UserService(ApplicationContext context)
        {
            this.context = context;
        }

        public List<User> GetList()
        {
            return context.Users.Include(u => u.Likes).ToList();
        }

        public User Get(int id)
        {
            return context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Likes)
                .FirstOrDefault();
        }

        public void Create(User newUser)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void Update(User updatedUser)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Entry(updatedUser).State = EntityState.Modified;
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
            var userToDelete = context.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();

            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Users.Remove(userToDelete);
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