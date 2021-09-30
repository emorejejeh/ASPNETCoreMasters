using DomainModels;
using Services.DTO;
using Services.Interfaces;

namespace Services
{
    public class ItemService : IItemService
    {
        public void Save(ItemDto dto)
        {
            ItemModel model = new ItemModel { Item = dto.Item };
        }
    }
}
