using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebGameStore.Model
{
    public class PlatformType
    {
        [Key]
        public string Type { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<GamePlatformType> GamePlatformTypes { get; set; }
    }
}
