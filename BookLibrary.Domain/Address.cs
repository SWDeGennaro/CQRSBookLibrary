﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain
{
    public class Address
    {
        public readonly string AddressLineOne;
        public readonly string AddressLineTwo;
        public readonly string Town;
        public readonly string County;
        public readonly string Country;
        public readonly string PostalCode;

        public Address(string addressLineOne, string addressLineTwo, string town, 
            string county, string country, string postalCode)
        {
            AddressLineOne = addressLineOne;
            AddressLineTwo = addressLineTwo;
            Town = town;
            County = county;
            Country = country;
            PostalCode = postalCode;
        }
    }
}
