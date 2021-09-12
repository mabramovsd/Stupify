using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stupify.Model;

namespace Stupify.Services
{
    public class SongService : IRepository<Song>
    {
        /// <summary>
        /// Снимок базы
        /// </summary>
        private readonly ApplicationContext context;

        public SongService(ApplicationContext context)
        {
            this.context = context;
        }

        public List<Song> GetList()
        {
            return context.Songs.Include(s=>s.Artist).ToList();
        }

        public Song Get(int id)
        {
            return context.Songs
                .Where(r => r.Id == id)
                .Include(s => s.Artist)
                .FirstOrDefault();
        }

        public void Create(Song newSong)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Songs.Add(newSong);
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void Update(Song updatedSong)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Entry(updatedSong).State = EntityState.Modified;
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
            var songToDelete = context.Songs
                .Where(x => x.Id == id)
                .FirstOrDefault();
            
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Songs.Remove(songToDelete);
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