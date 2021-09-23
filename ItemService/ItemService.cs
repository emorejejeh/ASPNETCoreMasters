using System.Collections.Generic;

namespace Service
{
    public class ItemService
    {
        public IEnumerable<string> GetAll(int userId)
        {
            return new List<string>
            {
                "item1",
                "item2",
                "item3"
            };
        }
    }
}
