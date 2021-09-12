using System.ComponentModel.DataAnnotations.Schema;

namespace Stupify.Model
{
    public class UserLike
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Song")]
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}