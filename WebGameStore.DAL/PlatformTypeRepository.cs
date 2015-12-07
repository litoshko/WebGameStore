using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGameStore.Model;

namespace WebGameStore.DAL
{
    public class PlatformTypeRepository : GenericRepository<PlatformType>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(DbContext context)
            : base(context)
        {

        }
    }
}
