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
    }
}
