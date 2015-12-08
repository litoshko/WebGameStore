using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGameStore.Model;

namespace WebGameStore.DAL
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context)
            : base(context)
        {

        }

        public IEnumerable<Comment> GetCommentsForGame(string id)
        {
            return _dbset.Where(c => c.GameKey == id);
        }

        public Comment GetById(int id)
        {
            return _dbset.FirstOrDefault(c => c.Id == id);
        }
    }
}
