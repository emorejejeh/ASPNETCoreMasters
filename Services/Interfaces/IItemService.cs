using Repositories;
using Services.DTO;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IItemService
    {

        void Add(ItemDto dto);
        IEnumerable<ItemDto> GetAll();
        IEnumerable<ItemDto> GetAllByFilter(ItemFilterByDto filters);
        ItemDto GetById(int id);
        void Update(ItemDto dto);
        void Delete(int id);

    }
}