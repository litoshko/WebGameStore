using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebGameStore.Model;

namespace WebGameStore.DAL
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(DbContext context)
            : base(context)
        {

        }

        public Game GetById(string id)
        {
            return _dbset.FirstOrDefault(x => x.Key == id);
        }
    }
}
