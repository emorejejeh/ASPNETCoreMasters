using System.Collections.Generic;

namespace Repositories
{
    public class DataContext
    {
        public DataContext()
        {
            
        }
        public List<ItemDto> Items { get; set; }
    }
}
