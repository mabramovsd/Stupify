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
        /// Invoke-RestMethod http://localhost:7564/songs/ -Method GET
        /// Invoke-RestMethod http://localhost:7564/songs -Method POST -Body (@{Name = "My Plague"; Artist="Slipknot"; Address = "http://slipknotation.narod.ru/mp3/iowa/Slipknot_-_My_plague.mp3"} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
        /// Invoke-RestMethod http://localhost:7564/songs/5 -Method DELETE

        public static List<Song> Songs;
        private readonly SongService songService;

        public SongsController(SongService songService)
        {
            this.songService = songService;
        }

        [HttpGet]
        public ActionResult<List<Song>> Get()
        {
            return songService.GetList();
        }

        [HttpGet("{id}")]
        public ActionResult<Song> Get(int id)
        {
            return songService.Get(id);
        }

        [HttpPost]
        public ActionResult<Song> Post(Song song)
        {
            song.Id = 0;
            songService.Create(song);
            return Ok(song);
        }

        [HttpPut]
        public ActionResult Put(Song song)
        {
            songService.Update(song);
            return Ok(song);
        }

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