using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebGameStore.Model
{
    public class GamePlatformType
    {
        public int Id { get; set; }
        public string GameKey { get; set; }
        public string PlatformTypeType { get; set; }

        [JsonIgnore]
        public virtual PlatformType PlatformType { get; set; }
        [JsonIgnore]
        public virtual Game Game { get; set; }
    }
}
