﻿// Itinero.Logistics - Route optimization for .NET
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

namespace Itinero.Logistics.Logging
{
    /// <summary>
    /// Represents different types of trace events.
    /// </summary>
    public enum TraceEventType
    {
        /// <summary>
        /// Critical.
        /// </summary>
        Critical,
        /// <summary>
        /// Error.
        /// </summary>
        Error,
        /// <summary>
        /// Warning.
        /// </summary>
        Warning,
        /// <summary>
        /// Verbose.
        /// </summary>
        Verbose,
        /// <summary>
        /// Information.
        /// </summary>
        Information
    }
}