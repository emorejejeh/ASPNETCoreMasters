using ASPNetCoreMastersTodoList.Api.BindingModels;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;

namespace ASPNetCoreMastersTodoList.Controllers
{
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }
        public int Get(int id)
        {
            return id;
        }

        public void Post(ItemCreateApiModel model)
        {
            _itemService.Save(new ItemDto { Item = model.Item });
        }
    }
}
