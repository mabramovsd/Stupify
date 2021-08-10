using System.ComponentModel.DataAnnotations.Schema;

namespace Stupify.Model.Songs
{
    public class SongToSave
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        public string Address { get; set; }
    }
}
