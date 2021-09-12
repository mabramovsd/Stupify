using System.ComponentModel.DataAnnotations.Schema;

namespace Stupify.Model
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public string Address { get; set; }
    }
}