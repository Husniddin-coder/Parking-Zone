﻿using Parking_Zone.Data.DbContexts;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.Repositories;

public class AddressRepository : Repository<Address> , IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    { }
}
