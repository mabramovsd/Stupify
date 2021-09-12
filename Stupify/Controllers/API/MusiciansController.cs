using Microsoft.AspNetCore.Mvc;
using Stupify.Model;
using Stupify.Model.Artists;
using Stupify.Services;
using System.Collections.Generic;

namespace Stupify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusiciansController : ControllerBase
    {
        private readonly IRepository<Artist> musicianService;

        public MusiciansController(IRepository<Artist> musicianService)
        {
            this.musicianService = musicianService;
        }

        /// <summary>
        /// Список исполнителей
        /// </summary>
        [HttpGet]
        public ActionResult<List<ArtistToRead>> Get()
        {
            List<ArtistToRead> musicians = new List<ArtistToRead>();

            foreach (var musicianFromDB in musicianService.GetList())
            {
                ArtistToRead musician = new ArtistToRead
                {
                    Id = musicianFromDB.Id,
                    Description = musicianFromDB.Description,
                    Name = musicianFromDB.Name
                };

                musicians.Add(musician);
            }
            return musicians;
        }

        /// <summary>
        /// Выбранный исполнитель
        /// </summary>
        /// <param name="id">Идентификатор исполнителя</param>
        [HttpGet("{id}")]
        public ActionResult<ArtistToRead> Get(int id)
        {
            var musicianFromDB = musicianService.Get(id);
            if (musicianFromDB == null)
            {
                return new ArtistToRead { Id = 0 };
            }

            ArtistToRead musician = new ArtistToRead
            {
                Id = musicianFromDB.Id,
                Description = musicianFromDB.Description,
                Name = musicianFromDB.Name
            };
            return musician;
        }

        /// <summary>
        /// Создание исполнителя
        /// </summary>
        /// <param name="musician">Параметры исполнителя</param>
        [HttpPost]
        public ActionResult<ArtistToSave> Post(ArtistToSave musician)
        {
            Artist musicianFromDB = new Artist
            {
                Id = 0,
                Description = musician.Description,
                Name = musician.Name
            };
            musicianService.Create(musicianFromDB);
            musician.Id = musicianFromDB.Id;

            return Ok(musician);
        }

        /// <summary>
        /// Обновление исполнителя
        /// </summary>
        /// <param name="musician">Параметры исполнителя</param>
        [HttpPut]
        public ActionResult Put(ArtistToSave musician)
        {
            Artist musicianFromDB = new Artist
            {
                Id =musician.Id,
                Description = musician.Description,
                Name = musician.Name
            };

            musicianService.Update(musicianFromDB);
            return Ok(musician);
        }

        /// <summary>
        /// Удаление исполнителя
        /// </summary>
        /// <param name="id">Идентификатор исполнителя</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var musician = musicianService.Get(id);
            if (musician == null)
            {
                return BadRequest();
            }

            musicianService.Delete(id);
            return Ok("Исполнитель успешно удален");
        }
    }
}