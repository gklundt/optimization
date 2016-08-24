﻿// Itinero - OpenStreetMap (OSM) SDK
// Copyright (C) 2016 Abelshausen Ben
// 
// This file is part of Itinero.
// 
// Itinero is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// Itinero is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Itinero. If not, see <http://www.gnu.org/licenses/>.

using Itinero.Attributes;
using Itinero.Profiles;
using System;
using System.Collections.Generic;

namespace Itinero.Logistics.Tests.Mocks
{
    /// <summary>
    /// A profile mock.
    /// </summary>
    public class MockProfile : Profile
    {
        /// <summary>
        /// Creates a new routing profile.
        /// </summary>
        private MockProfile(string name, Func<IAttributeCollection, Speed> getSpeed,
            List<string> vehicleTypes)
            : base(name, getSpeed, () => new Speed() { Value = 5, Direction = 0 }, x => true, (e1, e2) => true, vehicleTypes,
                  ProfileMetric.TimeInSeconds)
        {

        }

        /// <summary>
        /// Creates a mock car profile.
        /// </summary>
        /// <returns></returns>
        public static MockProfile CarMock()
        {
            return MockProfile.Mock("CarMock", x => new Speed()
            {
                Value = 50f / 3.6f,
                Direction = 0
            }, VehicleTypes.MotorVehicle, VehicleTypes.Vehicle);
        }

        /// <summary>
        /// Creates a mock car profile.
        /// </summary>
        /// <returns></returns>
        public static MockProfile CarMock(Func<IAttributeCollection, Speed> getSpeed)
        {
            return MockProfile.Mock("CarMock", getSpeed, VehicleTypes.MotorVehicle, VehicleTypes.Vehicle);
        }

        /// <summary>
        /// Creates a mock car profile.
        /// </summary>
        /// <returns></returns>
        public static MockProfile Mock(string name, Func<IAttributeCollection, Speed> getSpeed,
            params string[] vehicleTypes)
        {
            return new MockProfile(name, getSpeed, new List<string>(vehicleTypes));
        }
    }
}