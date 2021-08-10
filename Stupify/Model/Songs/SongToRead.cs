using Stupify.Model.Artists;

namespace Stupify.Model.Songs
{
    public class SongToRead
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ArtistId { get; set; }

        public ArtistToSave Artist { get; set; }

        public string Address { get; set; }
    }
}
