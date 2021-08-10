using Microsoft.AspNetCore.Mvc;
using Stupify.Model;
using Stupify.Model.Artists;
using Stupify.Model.Songs;
using Stupify.Services;
using System.Collections.Generic;

namespace Stupify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly SongService songService;

        public SongsController(SongService songService)
        {
            this.songService = songService;
        }

        /// <summary>
        /// Список песен
        /// </summary>
        [HttpGet]
        public ActionResult<List<SongToRead>> Get()
        {
            List<SongToRead> songs = new List<SongToRead>();
            foreach (var songFromDB in songService.GetList())
            {
                SongToRead song = new SongToRead
                {
                    Id = songFromDB.Id,
                    Address = songFromDB.Address,
                    ArtistId = songFromDB.ArtistId,
                    Artist = new ArtistToSave
                    {
                        Id = songFromDB.Artist.Id,
                        Description = songFromDB.Artist.Description,
                        Name = songFromDB.Artist.Name
                    },
                    Name = songFromDB.Name
                };

                songs.Add(song);
            }
            return songs;
        }

        /// <summary>
        /// Выбранная песня
        /// </summary>
        /// <param name="id">Идентификатор песни</param>
        [HttpGet("{id}")]
        public ActionResult<SongToRead> Get(int id)
        {
            var songFromDB = songService.Get(id);
            SongToRead song = new SongToRead
            {
                Id = songFromDB.Id,
                Address = songFromDB.Address,
                ArtistId = songFromDB.ArtistId,
                Artist = new ArtistToSave
                {
                    Id = songFromDB.Artist.Id,
                    Description = songFromDB.Artist.Description,
                    Name = songFromDB.Artist.Name
                },
                Name = songFromDB.Name
            };
            return song;
        }

        /// <summary>
        /// Создание песни
        /// </summary>
        /// <param name="song">Параметры песни</param>
        [HttpPost]
        public ActionResult<SongToSave> Post(SongToSave song)
        {
            Song songFromDB = new Song
            {
                Id = 0,
                Address = song.Address,
                ArtistId = song.ArtistId,
                Name = song.Name
            };
            songService.Create(songFromDB);
            return Ok(song);
        }

        /// <summary>
        /// Обновление песни
        /// </summary>
        /// <param name="song">Параметры песни</param>
        [HttpPut]
        public ActionResult Put(SongToSave song)
        {
            Song songFromDB = new Song
            {
                Id = song.Id,
                Address = song.Address,
                ArtistId = song.ArtistId,
                Name = song.Name
            };

            songService.Update(songFromDB);
            return Ok(song);
        }

        /// <summary>
        /// Удаление песни
        /// </summary>
        /// <param name="id">Идентификатор песни</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var song = songService.Get(id);
            if (song == null)
            {
                return BadRequest();
            }

            songService.Delete(id);
            return Ok("Песня успешно удалена");
        }
    }
}