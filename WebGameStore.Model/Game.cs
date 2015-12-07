using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebGameStore.Model
{
    public class Game
    {
        [Key]
        public string Key { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Comment> Comments { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<GameGenre> GameGenres { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<GamePlatformType> GamePlatformTypes { get; set; }
    }
}
