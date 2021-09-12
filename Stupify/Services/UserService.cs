using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stupify.Model;

namespace Stupify.Services
{
    public class UserService
    {
        private readonly ApplicationContext context;

        public UserService(ApplicationContext context)
        {
            this.context = context;
        }

        public List<User> GetList()
        {
            return context.Users.ToList();
        }

        public User Get(int id)
        {
            return context.Users
                .Where(r => r.Id == id)
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