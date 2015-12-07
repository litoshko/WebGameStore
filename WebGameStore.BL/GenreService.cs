using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public class GenreService : EntityService<Genre>, IGenreService
    {
        IUnitOfWork _unitOfWork;
        IGenreRepository _genreRepository;

        public GenreService(IUnitOfWork unitOfWork, IGenreRepository genreRepository)
            : base(unitOfWork, genreRepository)
        {
            _unitOfWork = unitOfWork;
            _genreRepository = genreRepository;
        }
    }
}
