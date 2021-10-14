using ASPNetCoreMastersTodoList.Api.BindingModels;
using ASPNetCoreMastersTodoList.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IAuthorizationService _authService;
        private readonly ILogger<ItemsController> _logger;
        public ItemsController(IItemService itemService, 
                               IAuthorizationService authService,
                               ILogger<ItemsController> logger)
        {
            _itemService = itemService;
            _authService = authService;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Getting all items - {RequestTime}", DateTime.Now);
            var items = _itemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{itemId}")]
        [ItemValidationFilterAttribute]
        public IActionResult Get(int itemId)
        {
            _logger.LogInformation("Getting item({itemId}) - {RequestTime}",itemId, DateTime.Now);
            if (Response.StatusCode.Equals((int)HttpStatusCode.NotFound))
            {
                return NotFound();
            }
            return Ok(_itemService.GetById(itemId));
        }

        [HttpGet("filterBy")]
        public IActionResult GetByFilters([FromQuery] Dictionary<string, string> filters)
        {
            _logger.LogInformation("Getting item by filter - {RequestTime}", DateTime.Now);
            filters.TryGetValue("id", out string id);
            filters.TryGetValue("item", out string item);

            return Ok(_itemService.GetAllByFilter(new ItemFilterByDto { Id = Convert.ToInt32(id), Item = item }));
        }

        [HttpPost()]
        public IActionResult Save([FromBody] ItemCreateApiModel model)
        {
            _logger.LogInformation("Saving item - {RequestTime}", DateTime.Now);
            _itemService.Add(new ItemDto { Item = model.Item, Id = model.Id, CreatedBy = User.Identity.Name, DateCreated = DateTime.UtcNow });
            return Ok();
        }

        [HttpPut()]
        [ItemValidationFilterAttribute]
        public async Task<IActionResult> Edit(ItemCreateApiModel item)
        {
            _logger.LogInformation("Editing item - {RequestTime}", DateTime.Now);
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
        [ItemValidationFilterAttribute]
        public IActionResult Delete(int itemId)
        {
            _logger.LogInformation("Deleting item({itemId}) - {RequestTime}", itemId, DateTime.Now);
            if (Response.StatusCode.Equals((int)HttpStatusCode.NotFound))
            {
                return NotFound();
            }
            _itemService.Delete(itemId);
            return Ok();
        }
    }
}
