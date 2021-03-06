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

using System;
using System.Collections.Generic;
using System.IO;
using Itinero.Optimization.Models.Costs;
using Itinero.Optimization.Models.Vehicles.Constraints;

namespace Itinero.Optimization.Test.Functional.VRP.Depot.Capacitated
{
    public static class DepotCVRPTests
    {
        private static string Name = "depot-CVRP-";

        public static void Run()
        {
            Console.WriteLine("Running Depot Capacitated VRP");
            Run1Wechelderzande();
            Run1WechelderzandeCapacitated();
            Run2SpijkenisseCapacitated();
            Run2SpijkenisseVisitCosts();
            Run2Spijkenisse();
            Run3DeHague();
            Run4Rotterdam();
            Run5Rotterdam();
        }

        public static void Run1Wechelderzande()
        {
            Console.WriteLine("Trying WechelderZande NC");
            // WECHELDERZANDE - LILLE
            // build routerdb and save the result.
            var lille = Staging.RouterDbBuilder.Build("query3");
            var vehicle = lille.GetSupportedVehicle("car");
            var router = new Router(lille);

            // build problem1.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem1.geojson"));

            // 
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP
            (vehicle.Fastest(), locations, 0, max: 100));
            var routes = func.TestPerf("Depot Capacitated VRP (Wechelderzande)");

            WriteStats(routes);
            routes.WriteGeoJson(DepotCVRPTests.Name + "lille-{0}.geojson");
        }

        public static void Run1WechelderzandeCapacitated()
        {
            // WECHELDERZANDE - LILLE
            // build routerdb and save the result.
            var lille = Staging.RouterDbBuilder.Build("query3");
            var vehicle = lille.GetSupportedVehicle("car");
            var router = new Router(lille);

            // build problem1.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem1.geojson"));

            // build capacity constraints
            var capacityConstraint = new CapacityConstraint[]
            {
                new CapacityConstraint()
                {
                    Name = Itinero.Optimization.Models.Metrics.Time,
                    Capacity = 10 // seconds
                },
                new CapacityConstraint()
                {
                    Name = Itinero.Optimization.Models.Metrics.Weight,
                    Capacity = 500
                }
            };

            // build visit costs.
            var visitCostWeight = new VisitCosts()
            {
                Name = Itinero.Optimization.Models.Metrics.Weight,
                Costs = new float[locations.Length]
            };
            for (var l = 0; l < locations.Length; l++)
            {
                visitCostWeight.Costs[l] = 100;
            }
            var visitCosts = new VisitCosts[]
            {
                visitCostWeight
            };

            // 
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP(
                vehicle.Fastest(), locations, 0, capacityConstraint,
                visitCosts));
            var routes = func.TestPerf("Depot Capacitated VRP (Wechelderzande - Capacitated)");


            // write info about result.
            WriteStats(routes);
            routes.WriteGeoJson(DepotCVRPTests.Name + "lille-capacitated-{0}.geojson");
        }

        public static void Run2Spijkenisse()
        {
            // SPIJKENISSE
            // build routerdb and save the result.
            var spijkenisse = Staging.RouterDbBuilder.Build("query4");
            var vehicle = spijkenisse.GetSupportedVehicle("car");
            var router = new Router(spijkenisse);

            // build problem.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem2-spijkenisse.geojson"));

            // 
            var max = 5400;
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP(vehicle.Fastest(), locations, 3, max));
            var routes = func.TestPerf("Depot Capacitated VRP (Spijkenisse)");


            // write info about result.
            WriteStats(routes);
            var filename = DepotCVRPTests.Name + "spijkenisse-{0}";
            routes.WriteGeoJson(filename + ".geojson");
            routes.WriteJson(filename + ".json");
        }

        public static void Run2SpijkenisseCapacitated()
        {
            // SPIJKENISSE
            // build routerdb and save the result.
            var spijkenisse = Staging.RouterDbBuilder.Build("query4");
            var vehicle = spijkenisse.GetSupportedVehicle("car");
            var router = new Router(spijkenisse);

            // build problem.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem2-spijkenisse.geojson"));

            // build capacity constraints
            var capacityConstraint = new CapacityConstraint[]
            {
                new CapacityConstraint()
                {
                    Name = Itinero.Optimization.Models.Metrics.Time,
                    Capacity = 15000
                },
                new CapacityConstraint()
                {
                    Name = Itinero.Optimization.Models.Metrics.Weight,
                    Capacity = 9000
                }
            };

            // build visit costs.
            var visitCostWeight = new VisitCosts()
            {
                Name = Itinero.Optimization.Models.Metrics.Weight,
                Costs = new float[locations.Length]
            };
            for (var l = 0; l < locations.Length; l++)
            {
                visitCostWeight.Costs[l] = 100;
            }
            var visitCosts = new VisitCosts[]
            {
                visitCostWeight
            };

            // 
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP(vehicle.Fastest(), locations, 0,
                capacityConstraint, visitCosts));
            var routes = func.TestPerf("Depot Capacitated VRP (Spijkenisse - Capacitated)");

            //#if DEBUG

            // write info about result.
            WriteStats(routes);
            routes.WriteGeoJson(DepotCVRPTests.Name + "spijkenisse-capacitated-{0}.geojson");
            //#endif
        }

        public static void Run2SpijkenisseVisitCosts()
        {
            // SPIJKENISSE
            // build routerdb and save the result.
            var spijkenisse = Staging.RouterDbBuilder.Build("query4");
            var vehicle = spijkenisse.GetSupportedVehicle("car");
            var router = new Router(spijkenisse);

            // build problem.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem2-spijkenisse.geojson"));

            // build capacity constraints
            var capacityConstraint = new CapacityConstraint[]
            {
                new CapacityConstraint()
                {
                    Name = Itinero.Optimization.Models.Metrics.Time,
                    Capacity = 3600 * 4
                }
            };

            // build visit costs.
            var visitCostWeight = new VisitCosts()
            {
                Name = Itinero.Optimization.Models.Metrics.Time,
                Costs = new float[locations.Length]
            };
            for (var l = 0; l < locations.Length; l++)
            {
                visitCostWeight.Costs[l] = 180;
            }
            var visitCosts = new VisitCosts[]
            {
                visitCostWeight
            };

            // 
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP(vehicle.Fastest(), locations, depot: 0,
                capacityConstraints: capacityConstraint, costs: visitCosts));
            var routes = func.TestPerf("Depot Visit Costs VRP (Spijkenisse - Capacitated)");

            // write info about result.
            WriteStats(routes);
            var filename = DepotCVRPTests.Name + "spijkenisse-visit-costs-{0}";
            routes.WriteGeoJson(filename + ".geojson");
            routes.WriteJson(filename + ".json");
            //#endif
        }

        public static void Run3DeHague()
        {
            // DE-HAGUE
            // build routerdb and save the result.
            var dehague = Staging.RouterDbBuilder.Build("query5");
            var vehicle = dehague.GetSupportedVehicle("car");
            var router = new Router(dehague);

            // build problem.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem3-de-hague.geojson"));

            var max = 3800;
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP(vehicle.Fastest(), locations, depot: 0, max: max));
            var routes = func.TestPerf("Depot Capacitated VRP (De Haque)");

            //#if DEBUG

            // write info about result.
            WriteStats(routes);
            routes.WriteGeoJson(DepotCVRPTests.Name + "de-hague-{0}.geojson");
            //#endif
        }

        public static void Run4Rotterdam()
        {
            // ROTTERDAM
            // build routerdb and save the result.
            var rotterdam = Staging.RouterDbBuilder.Build("query6");
            var vehicle = rotterdam.GetSupportedVehicle("car");
            var router = new Router(rotterdam);

            // build problem.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem4-rotterdam.geojson"));

            // 
            var max = 4500;
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP(vehicle.Fastest(), locations, 0, max));
            var routes = func.TestPerf("Depot Capacitated VRP (Rotterdam4)");

            //#if DEBUG

            // write info about result.
            WriteStats(routes);
            routes.WriteGeoJson(DepotCVRPTests.Name + "rotterdam4-{0}.geojson");
            //#endif 
        }

        public static void Run5Rotterdam()
        {
            // ROTTERDAM
            // build routerdb and save the result.
            var rotterdam = Staging.RouterDbBuilder.Build("query7");
            var vehicle = rotterdam.GetSupportedVehicle("car");
            var router = new Router(rotterdam);

            // build problem.
            var locations = Staging.StagingHelpers.GetLocations(
                Staging.StagingHelpers.GetFeatureCollection("data.DepotCVRP.problem5-rotterdam.geojson"));

            // 
            var max = 4500;
            var func = new Func<List<Route>>(() => router.CalculateDepotCVRP(vehicle.Fastest(), locations, 0, max));
            var routes = func.TestPerf("Depot Capacitated VRP (Rotterdam5)");

            //#if DEBUG

            // write info about result.
            WriteStats(routes);
            routes.WriteGeoJson(DepotCVRPTests.Name + "rotterdam5-{0}.geojson");
            //#endif 
        }

        /// <summary>
        /// Writes stats about the given routes.
        /// </summary>
        /// <param name="routes"></param>
        public static void WriteStats(IList<Route> routes)
        {
            Console.WriteLine("# routes: {0}", routes.Count);

            var totalStops = 0;
            var totalDistance = 0f;
            var totalTime = 0f;
            for (var i = 0; i < routes.Count; i++)
            {
                Console.Write("route_{0}:", i);
                if (routes[i] == null)
                {
                    Console.WriteLine("null");
                    continue;
                }
                Console.Write("{0}s | ", routes[i].TotalTime);
                Console.Write("{0}m | ", routes[i].TotalDistance);
                Console.Write("{0}stops", routes[i].Stops.Length - 1);
                totalStops += routes[i].Stops.Length - 1;
                totalDistance += routes[i].TotalDistance;
                totalTime += routes[i].TotalTime;
                Console.WriteLine();
            }

            Console.Write("total:");
            Console.Write("{0}s | ", totalTime);
            Console.Write("{0}m | ", totalDistance);
            Console.Write("{0}stops", totalStops);
            Console.WriteLine();
        }
    }
}