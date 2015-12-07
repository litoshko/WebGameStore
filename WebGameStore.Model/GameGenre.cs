using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGameStore.Model
{
    public class GameGenre
    {
        public int Id { get; set; }
        public string GameKey { get; set; }
        public string GenreName { get; set; }

        public virtual Game Game { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
