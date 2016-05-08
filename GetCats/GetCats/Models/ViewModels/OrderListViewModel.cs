using System.Collections.Generic;
using GetCats.Models.DTO;

namespace GetCats.Models.ViewModels
{
    public class OrderListViewModel
    {
        public List<OrderListItem> Orders { get; set; }
    }
}