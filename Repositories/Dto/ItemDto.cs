using System;

namespace Repositories
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
