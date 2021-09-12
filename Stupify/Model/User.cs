using System.Collections.Generic;

namespace Stupify.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<UserLike> Likes { get; set; }
    }
}
