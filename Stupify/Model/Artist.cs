using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stupify.Model
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Song> Songs { get; set; }
        public Artist()
        {
            Songs = new List<Song>();
        }
    }
}
