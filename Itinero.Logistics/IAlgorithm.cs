﻿// Itinero - OpenStreetMap (OSM) SDK
// Copyright (C) 2015 Abelshausen Ben
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

namespace Itinero.Logistics
{
    /// <summary>
    /// Abstract representation of an algorithm.
    /// </summary>
    public interface IAlgorithm
    {
        /// <summary>
        /// Returns an error message when the algorithm was not successful.
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Returns true if this instance has run already.
        /// </summary>
        bool HasRun { get; }

        /// <summary>
        /// Returns true if this instance has run and it was succesfull.
        /// </summary>
        bool HasSucceeded { get; }

        /// <summary>
        /// Runs the algorithm.
        /// </summary>
        void Run();
    }
}