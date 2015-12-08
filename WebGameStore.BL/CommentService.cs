using System.Collections.Generic;
using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public class CommentService : EntityService<Comment>, ICommentService
    {
        IUnitOfWork _unitOfWork;
        ICommentRepository _commentRepository;

        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository)
            : base(unitOfWork, commentRepository)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
        }


        public IEnumerable<Comment> GetCommentsForGame(string id)
        {
            return _commentRepository.GetCommentsForGame(id);
        }

        public Comment GetById(int id)
        {
            return _commentRepository.GetById(id);
        }
    }
}
