using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Enums
{
    public enum DeliveryTypes
    {
        Courier = 0,
        Parcel_Locker = 1,
        Mail = 2
    }
    public class Delivery
    {
        public static string GetDeliveryName(int deliveryNr)
        {
            if (Enum.IsDefined(typeof(DeliveryTypes), deliveryNr))
            {
                return Enum.GetName(typeof(DeliveryTypes), deliveryNr);
            }
            else
            {
                throw new NotFoundException("Delivery type doesn't exist");
            }
        }
    }
}
