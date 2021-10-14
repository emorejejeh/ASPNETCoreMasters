using Repositories;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public void Add(ItemDto dto)
        {
            _itemRepository.SaveItem(dto);
        }

        public IEnumerable<ItemDto> GetAll()
        {
            return _itemRepository.All();
        }

        public IEnumerable<ItemDto> GetAllByFilter(ItemFilterByDto filters)
        {
            IEnumerable<ItemDto> response;

            var itemFilterById = filters.Id.HasValue && filters.Id != 0 ? _itemRepository.All().Where(i => i.Id.Equals(filters.Id)) : _itemRepository.All();

            if (filters.Id.HasValue)
            {
                var itemFilterByItem = !string.IsNullOrEmpty(filters.Item) ? itemFilterById.Where(i => i.Item.ToLower().Equals(filters.Item.ToLower())) : itemFilterById;
                response = itemFilterByItem;
            }
            else
                response = !string.IsNullOrEmpty(filters.Item) ? _itemRepository.All().Where(i => i.Item.ToLower().Equals(filters.Item.ToLower())) : _itemRepository.All();

            return response;
        }

        public ItemDto GetById(int id)
        {
            return _itemRepository.All().FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Update(ItemDto dto)
        {
            _itemRepository.UpdateItem(dto);
        }

        public void Delete(int id)
        {
            _itemRepository.Delete(id);
        }


        public IEnumerable<ItemDto> CreateDto(int count)
        {
            var response = new List<ItemDto>();
            for(int x = 1; x <= count; x++)
            {
                response.Add(new ItemDto
                {
                    Id = x,
                    Item = "Item" + x.ToString(),
                    DateCreated = DateTime.UtcNow,
                    CreatedBy = "test" + x.ToString() + "@example.com"
                });
            }

            return response;
        }


    }
}
