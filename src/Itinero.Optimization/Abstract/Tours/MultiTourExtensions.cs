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

using System.Collections.Generic;

namespace Itinero.Optimization.Abstract.Tours
{
    /// <summary>
    /// Contains extensions related to multi tours.
    /// </summary>
    public static class MultiTourExtensions
    {
        /// <summary>
        /// Verifies the given tour.
        /// </summary>
        public static void Verify(this IMultiTour tour, int count)
        {
#if DEBUG
            for (var t = 0; t < tour.Count; t++)
            {
                tour.Tour(t).Verify(count);
            }
#endif
        }
    }
}