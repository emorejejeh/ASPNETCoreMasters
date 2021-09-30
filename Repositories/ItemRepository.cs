using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
            _context.Items = CreateDto(2);
        }
        public IEnumerable<ItemDto> All()
        {
            return _context.Items;
        }

        public void SaveItem(ItemDto item)
        {
            _context.Items.Add(item);
        }

        public void UpdateItem(ItemDto item)
        {
            _context.Items.First(x => x.Id.Equals(item.Id)).Item = item.Item;
        }

        public void Delete(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id.Equals(id));
            _context.Items.Remove(item);
        }

        public List<ItemDto> CreateDto(int count)
        {
            var response = new List<ItemDto>();
            for (int x = 1; x <= count; x++)
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
