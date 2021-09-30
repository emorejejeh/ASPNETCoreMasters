using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreMastersTodoList.Api.BindingModels
{
    public class ItemCreateApiModel
    {
        [StringLength(128, MinimumLength = 1)]
        public string Item { get; set; }
    }
}
