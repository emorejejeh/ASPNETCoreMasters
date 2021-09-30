using Services.DTO;

namespace Services.Interfaces
{
    public interface IItemService
    {
        void Save(ItemDto dto);
    }
}