using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebGameStore.Model
{
    public class GameGenre
    {
        public int Id { get; set; }
        public string GameKey { get; set; }
        public string GenreName { get; set; }

        [JsonIgnore]
        public virtual Game Game { get; set; }
        [JsonIgnore]
        public virtual Genre Genre { get; set; }
    }
}
