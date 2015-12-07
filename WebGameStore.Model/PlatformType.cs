using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGameStore.Model
{
    public class PlatformType
    {
        [Key]
        public string Type { get; set; }

        public virtual IEnumerable<GamePlatformType> GamePlatformTypes { get; set; }
    }
}
