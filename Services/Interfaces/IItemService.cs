using Services.DTO;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IItemService
    {

        void Save(ItemDto dto);
        IEnumerable<ItemDto> GetAll();
        ItemDto GetById(int id);
        void UpdateItem(int id);
        void DeleteItem(int id);

    }
}