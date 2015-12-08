using System.Collections.Generic;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public interface ICommentService : IEntityService<Comment>
    {
        IEnumerable<Comment> GetCommentsForGame(string id);
        Comment GetById(int id);
    }
}
