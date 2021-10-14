using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreMastersTodoList.Api.BindingModels
{
    public class ItemCreateApiModel
    {
        public int Id { get; set; }
        [StringLength(128, MinimumLength = 1)]
        public string Item { get; set; }
    }
}
