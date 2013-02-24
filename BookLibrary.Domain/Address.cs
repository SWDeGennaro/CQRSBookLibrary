using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain
{
    public class Address
    {
        public string AddressLineOne { get; private set; }
        public string AddressLineTwo { get; private set; }
        public string Town { get; private set; }
        public string County { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }

        public Address(string addressLineOne, string addressLineTwo, string town, 
            string county, string country, string postalCode)
        {
            AddressLineOne = addressLineOne;
            AddressLineTwo = addressLineTwo;
            Town = town;
            County = country;
            Country = country;
            PostalCode = postalCode;
        }
    }
}
