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

        [HttpPost()]
        public void Post([FromBody] ItemCreateApiModel model)
        {
            if(ModelState.IsValid)
                _itemService.Save(new ItemDto { Item = model.Item });


        }
    }
}
