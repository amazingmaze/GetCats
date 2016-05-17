using System;
using GetCats.Models.Entities;

namespace GetCats.Models.ViewModels
{
    public class OptionBidViewModel
    {
        public Bid.BidStatus Status { get; set; }
        public int Bid { get; set; }
        public Guid BidId { get; set; }
    }
}