using ASPNetCoreMastersTodoList.Api.BindingModels;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ASPNetCoreMastersTodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var items = _itemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{itemId}")]
        public IActionResult Get(int itemId)
        {
            return Ok(_itemService.GetById(itemId));
        }

        [HttpGet("filterBy")]
        public IActionResult GetByFilters([FromQuery] Dictionary<string, string> filters)
        {

            filters.TryGetValue("id", out string id);
            filters.TryGetValue("item", out string item);

            return Ok(_itemService.GetAllByFilter(new ItemFilterByDto { Id = Convert.ToInt32(id), Item = item }));
        }

        [HttpPost()]
        public IActionResult Post([FromBody] ItemCreateApiModel model)
        {
            _itemService.Add(new ItemDto { Item = model.Item, Id = model.Id });
            return Ok();
        }

        [HttpPut()]
        public IActionResult Put(ItemDto item)
        {
            _itemService.Update(item);
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public IActionResult Delete(int itemId)
        {
            _itemService.Delete(itemId);
            return Ok();
        }
    }
}
