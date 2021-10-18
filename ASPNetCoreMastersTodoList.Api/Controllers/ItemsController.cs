using ASPNetCoreMastersTodoList.Api.BindingModels;
using ASPNetCoreMastersTodoList.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ASPNetCoreMastersTodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ItemValidationFilterAttribute]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IAuthorizationService _authService;
        public ItemsController(IItemService itemService, IAuthorizationService authService)
        {
            _itemService = itemService;
            _authService = authService;
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
            if (Response.StatusCode.Equals((int)HttpStatusCode.NotFound))
            {
                return NotFound();
            }
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
        public IActionResult Save([FromBody] ItemCreateApiModel model)
        {
            _itemService.Add(new ItemDto { Item = model.Item, Id = model.Id, CreatedBy = User.Identity.Name, DateCreated = DateTime.UtcNow });
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Put(ItemDto item)
        {
            if (Response.StatusCode.Equals((int)HttpStatusCode.NotFound))
            {
                return NotFound();
            }

            var itemVM = _itemService.GetById(item.Id);
            var authResult = await _authService.AuthorizeAsync(User, new ItemDto() { CreatedBy = itemVM.CreatedBy }, "CanEditItems");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }
            _itemService.Update(new ItemDto { Id = item.Id, Item = item.Item });
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public IActionResult Delete(int itemId)
        {
            if (Response.StatusCode.Equals((int)HttpStatusCode.NotFound))
            {
                return NotFound();
            }
            _itemService.Delete(itemId);
            return Ok();
        }
    }
}
