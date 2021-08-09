using Microsoft.AspNetCore.Mvc;
using Stupify.Model;
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
        public ActionResult<List<Song>> Get()
        {
            return songService.GetList();
        }

        /// <summary>
        /// Выбранная песня
        /// </summary>
        /// <param name="id">Идентификатор песни</param>
        [HttpGet("{id}")]
        public ActionResult<Song> Get(int id)
        {
            return songService.Get(id);
        }

        /// <summary>
        /// Создание песни
        /// </summary>
        /// <param name="song">Параметры песни</param>
        [HttpPost]
        public ActionResult<Song> Post(Song song)
        {
            song.Id = 0;
            songService.Create(song);
            return Ok(song);
        }

        /// <summary>
        /// Обновление песни
        /// </summary>
        /// <param name="song">Параметры песни</param>
        [HttpPut]
        public ActionResult Put(Song song)
        {
            songService.Update(song);
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