using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebGameStore.Model
{
    public class Genre
    {
        [Key]
        public string Name { get; set; }

        public string ParentName { get; set; }

        [JsonIgnore]
        public virtual Genre Parent { get; set; }
        [JsonIgnore]
        [ForeignKey("ParentName")]
        public virtual IEnumerable<Genre> SubGenres { get; set; }
    }
}
