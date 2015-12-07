using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGameStore.Model;

namespace WebGameStore.DAL
{
    public class GameGenreRepository : GenericRepository<GameGenre>, IGameGenreRepository
    {
        public GameGenreRepository(DbContext context)
            : base(context)
        {

        }
    }
}
