/*
 *  Licensed to SharpSoftware under one or more contributor
 *  license agreements. See the NOTICE file distributed with this work for 
 *  additional information regarding copyright ownership.
 * 
 *  SharpSoftware licenses this file to you under the Apache License, 
 *  Version 2.0 (the "License"); you may not use this file except in 
 *  compliance with the License. You may obtain a copy of the License at
 * 
 *       http://www.apache.org/licenses/LICENSE-2.0
 * 
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using Itinero.LocalGeo;
using Itinero.Optimization.Models.Costs;
using Itinero.Optimization.Models.TimeWindows;
using Itinero.Optimization.Models.Vehicles;

namespace Itinero.Optimization.Models
{
    /// <summary>
    /// Represents a real-world model for a vehicle routing problem.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Gets or sets the visits (including any depots).
        /// </summary>
        /// <returns></returns>
        public Coordinate[] Visits { get; set; }

        /// <summary>
        /// Gets or sets the time windows.
        /// </summary>
        /// <returns></returns>
        public TimeWindow[] TimeWindows { get; set; }
    
        /// <summary>
        /// Gets or sets the visit costs.
        /// </summary>
        /// <returns></returns>
        public VisitCosts[] VisitCosts { get; set; }

        /// <summary>
        /// Gets or sets the vehicle pool.
        /// </summary>
        /// <returns></returns>
        public VehiclePool VehiclePool { get; set; }
    }
}