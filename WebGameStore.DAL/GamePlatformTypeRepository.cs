using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGameStore.Model;

namespace WebGameStore.DAL
{
    public class GamePlatformTypeRepository : GenericRepository<GamePlatformType>, IGamePlatformTypeRepository
    {
        public GamePlatformTypeRepository(DbContext context)
            : base(context)
        {

        }
    }
}
