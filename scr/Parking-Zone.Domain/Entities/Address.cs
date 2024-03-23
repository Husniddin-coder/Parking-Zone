﻿namespace Parking_Zone.Domain.Entities
{
    public class Address : Auditable
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
