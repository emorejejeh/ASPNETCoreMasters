using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public interface IItemRepository
    {
        IEnumerable<ItemDto> All();
        void Delete(int id);
        void SaveItem(ItemDto item);
        void UpdateItem(ItemDto item);
    }
}