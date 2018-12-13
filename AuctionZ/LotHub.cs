using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace AuctionZ
{
    public class HubErrors
    {
        public Dictionary<string, List<string>> Errors { get; }

        public HubErrors()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public void AddError(string field, string message)
        {
            if(!Errors.TryGetValue(field, out var list))
                list = Errors[field] = new List<string>();
            list.Add(message);
        }
        public bool Empty => Errors.Count == 0;
    }

    public class LotHub : Hub
    {

        private readonly ILotsService _lotsService;
        private readonly IBidsService _bidsService;
        private readonly IUserServices _userService;
        
        private readonly HubErrors _errors;
        
        public LotHub(ILotsService lotService, 
            IBidsService bidService,
            IUserServices userService)
        {
            _lotsService = lotService;
            _bidsService = bidService;
            _userService = userService;
            _errors = new HubErrors();
        }
        
               
        public async Task MakeBid(int userId, int lotId, decimal bidValue)
        {
            var lot = _lotsService.GetItem(lotId);
            if (lot == null)
                _errors.AddError(nameof(lot.LotId), "Lot with id doesnt exist");
            var user = _userService.GetItem(userId);
            if (lot.Price >= bidValue)
                _errors.AddError(nameof(bidValue), "Bid must be greater than value price");
            if (user.Money < bidValue)
                _errors.AddError(nameof(bidValue), "Not enough funds");
            if (!_errors.Empty)
            {
                var res = JsonConvert.SerializeObject(_errors.Errors, Formatting.Indented);
                await Clients.Caller.SendAsync(
                    "Error", res);
                return;
            }

            _userService.MakeBid(lot.LotId, userId, bidValue);
            var lastbid = _bidsService.GetLastBidForLot(lotId);
            var row_info = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr", lastbid.User.UserName,
                lastbid.Price, lastbid.DateOfBid.ToString("f"));
            await Clients.All.SendAsync("BidMade", lotId, row_info, lastbid.Price.ToString("C"));

        }
    }
}
