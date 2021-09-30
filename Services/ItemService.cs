using DomainModels;
using Services.DTO;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly IEnumerable<ItemDto> _items;

         
        public ItemService()
        {
            _items = CreateDto(2);
        }
        public void Save(ItemDto dto)
        {
            ItemModel model = new ItemModel { Item = dto.Item };
        }

        public IEnumerable<ItemDto> GetAll()
        {
            return _items;
        }

        public ItemDto GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void UpdateItem(int id)
        {

        }

        public void DeleteItem(int id)
        {

        }


        public IEnumerable<ItemDto> CreateDto(int count)
        {
            var response = new List<ItemDto>();
            for(int x = 1; x <= count; x++)
            {
                response.Add(new ItemDto
                {
                    Id = x,
                    Item = "Item" + x.ToString()
                });
            }

            return response;
        }


    }
}
