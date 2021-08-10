using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stupify.Model;

namespace Stupify.Services
{
    public class ArtistService
    {
        /// <summary>
        /// Снимок базы
        /// </summary>
        private readonly ApplicationContext context;

        public ArtistService(ApplicationContext context)
        {
            this.context = context;
        }

        public List<Artist> GetList()
        {
            return context.Artists.Include(s => s.Songs).ToList();
        }

        public Artist Get(int id)
        {
            return context.Artists
                .Where(r => r.Id == id)
                .Include(s => s.Songs)
                .FirstOrDefault();
        }

        public void Create(Artist newArtist)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Artists.Add(newArtist);
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void Update(Artist updatedArtist)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Entry(updatedArtist).State = EntityState.Modified;
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
            var artistToDelete = context.Artists
                .Where(x => x.Id == id)
                .FirstOrDefault();

            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Artists.Remove(artistToDelete);
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