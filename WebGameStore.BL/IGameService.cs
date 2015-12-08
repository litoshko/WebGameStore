using System.Collections.Generic;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public interface IGameService : IEntityService<Game>
    {
        Game GetById(string id);
        IEnumerable<Game> GetByGenre(string name);
        IEnumerable<Game> GetByPlatform(string name);
    }
}
