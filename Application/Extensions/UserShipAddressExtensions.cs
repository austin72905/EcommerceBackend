using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class UserShipAddressExtensions
    {
        public static UserShipAddressDTO ToUserShipAddressDTO(this UserShipAddress address)
        {
            return new UserShipAddressDTO
            {
                AddressId = address.Id,
                RecipientName = address.RecipientName,
                PhoneNumber = address.PhoneNumber,
                RecieveWay = address.RecieveWay,
                RecieveStore = address.RecieveStore,
                AddressLine = address.AddressLine,
                IsDefault = address.IsDefault
            };
        }
    }
}
