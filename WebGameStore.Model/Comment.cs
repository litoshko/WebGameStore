using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGameStore.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public string GameKey { get; set; }
        public int? ParentCommentId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Comment ParentComment { get; set; }
        [ForeignKey("ParentCommentId")]
        public virtual IEnumerable<Comment> ChildComments { get; set; }
    }
}
