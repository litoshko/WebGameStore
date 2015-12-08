using WebGameStore.Model;

namespace WebGameStore.BL
{
    public interface IGameService : IEntityService<Game>
    {
        Game GetById(string id);
    }
}
