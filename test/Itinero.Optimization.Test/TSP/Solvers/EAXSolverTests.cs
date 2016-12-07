﻿// Itinero.Optimization - Route optimization for .NET
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

using Itinero.Optimization.Algorithms.Random;
using Itinero.Optimization.Algorithms.Solvers.GA;
using Itinero.Optimization.TSP;
using Itinero.Optimization.TSP.Solvers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Itinero.Optimization.Test.TSP.Solvers
{
    /// <summary>
    /// Contains tests for the EAX-solver.
    /// </summary>
    [TestFixture]
    public class EAXSolverTests
    {
        /// <summary>
        /// Tests the solver with only 1 customer.
        /// </summary>
        [Test]
        public void TestSolution1()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 0, 1, 0);

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (int i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(0, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver with only 2 customers.
        /// </summary>
        [Test]
        public void TestSolution2()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 1, 2, 10);
            problem.Weights[0][1] = 1;
            problem.Weights[1][0] = 1;

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (int i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(1, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver with a non-fixed ending.
        /// </summary>
        [Test]
        public void TestSolutions5NotClosedNotFixed()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 4, 5, 10);
            problem.Weights[0][1] = 1;
            problem.Weights[1][2] = 1;
            problem.Weights[2][3] = 1;
            problem.Weights[3][4] = 1;
            problem.Weights[4][0] = 1;

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (int i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(4, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.IsTrue(solutionList.Remove(2));
                Assert.IsTrue(solutionList.Remove(3));
                Assert.IsTrue(solutionList.Remove(4));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver on an 'open' solution but with a fixed end.
        /// </summary>
        [Test]
        public void TestSolutions5NotClosedFixed()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 4, 5, 10);
            problem.Weights[0][1] = 1;
            problem.Weights[1][2] = 1;
            problem.Weights[2][3] = 1;
            problem.Weights[3][4] = 1;
            problem.Weights[4][0] = 1;

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (int i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(4, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.IsTrue(solutionList.Remove(2));
                Assert.IsTrue(solutionList.Remove(3));
                Assert.IsTrue(solutionList.Remove(4));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver.
        /// </summary>
        [Test]
        public void TestSolutions5ClosedNotFixed()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 0, 5, 10);
            problem.Weights[0][1] = 1;
            problem.Weights[1][2] = 1;
            problem.Weights[2][3] = 1;
            problem.Weights[3][4] = 1;
            problem.Weights[4][0] = 1;

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (int i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(5, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.IsTrue(solutionList.Remove(2));
                Assert.IsTrue(solutionList.Remove(3));
                Assert.IsTrue(solutionList.Remove(4));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver.
        /// </summary>
        [Test]
        public void TestSolutions5ClosedFixed()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 4, 5, 10);
            problem.Weights[0][1] = 1;
            problem.Weights[1][2] = 1;
            problem.Weights[2][3] = 1;
            problem.Weights[3][4] = 1;
            problem.Weights[4][0] = 1;

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (int i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(4, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.IsTrue(solutionList.Remove(2));
                Assert.IsTrue(solutionList.Remove(3));
                Assert.IsTrue(solutionList.Remove(4));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver.
        /// </summary>
        [Test]
        public void TestSolutionbr17Fixed()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            var weights = new float[][] {
                new float[] {9999,   3,   5,  48,  48,   8,   8,   5,   5,   3,   3,   0,   3,   5,   8,   8,   5},
                new float[] {   3,9999,   3,  48,  48,   8,   8,   5,   5,   0,   0,   3,   0,   3,   8,   8,   5},
                new float[] {   5,   3,9999,  72,  72,  48,  48,  24,  24,   3,   3,   5,   3,   0,  48,  48,  24},
                new float[] {  48,  48,  74,9999,   0,   6,   6,  12,  12,  48,  48,  48,  48,  74,   6,   6,  12},
                new float[] {  48,  48,  74,   0,9999,   6,   6,  12,  12,  48,  48,  48,  48,  74,   6,   6,  12},
                new float[] {   8,   8,  50,   6,   6,9999,   0,   8,   8,   8,   8,   8,   8,  50,   0,   0,   8},
                new float[] {   8,   8,  50,   6,   6,   0,9999,   8,   8,   8,   8,   8,   8,  50,   0,   0,   8},
                new float[] {   5,   5,  26,  12,  12,   8,   8,9999,   0,   5,   5,   5,   5,  26,   8,   8,   0},
                new float[] {   5,   5,  26,  12,  12,   8,   8,   0,9999,   5,   5,   5,   5,  26,   8,   8,   0},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,9999,   0,   3,   0,   3,   8,   8,   5},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,   0,9999,   3,   0,   3,   8,   8,   5},
                new float[] {   0,   3,   5,  48,  48,   8,   8,   5,   5,   3,   3,9999,   3,   5,   8,   8,   5},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,   0,   0,   3,9999,   3,   8,   8,   5},
                new float[] {   5,   3,   0,  72,  72,  48,  48,  24,  24,   3,   3,   5,   3,9999,  48,  48,  24},
                new float[] {   8,   8,  50,   6,   6,   0,   0,   8,   8,   8,   8,   8,   8,  50,9999,   0,   8},
                new float[] {   8,   8,  50,   6,   6,   0,   0,   8,   8,   8,   8,   8,   8,  50,   0,9999,   8},
                new float[] {   5,   5,  26,  12,  12,   8,   8,   0,   0,   5,   5,   5,   5,  26,   8,   8,9999}};

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 16, weights);

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 5,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 200
            });

            for (var i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(0, solution.First);
                Assert.AreEqual(16, solution.Last);
                Assert.AreEqual(34, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.IsTrue(solutionList.Remove(2));
                Assert.IsTrue(solutionList.Remove(3));
                Assert.IsTrue(solutionList.Remove(4));
                Assert.IsTrue(solutionList.Remove(5));
                Assert.IsTrue(solutionList.Remove(6));
                Assert.IsTrue(solutionList.Remove(7));
                Assert.IsTrue(solutionList.Remove(8));
                Assert.IsTrue(solutionList.Remove(9));
                Assert.IsTrue(solutionList.Remove(10));
                Assert.IsTrue(solutionList.Remove(11));
                Assert.IsTrue(solutionList.Remove(12));
                Assert.IsTrue(solutionList.Remove(13));
                Assert.IsTrue(solutionList.Remove(14));
                Assert.IsTrue(solutionList.Remove(15));
                Assert.IsTrue(solutionList.Remove(16));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver.
        /// </summary>
        [Test]
        public void TestSolutionbr17Closed()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            var weights = new float[][] {
                new float[] {9999,   3,   5,  48,  48,   8,   8,   5,   5,   3,   3,   0,   3,   5,   8,   8,   5},
                new float[] {   3,9999,   3,  48,  48,   8,   8,   5,   5,   0,   0,   3,   0,   3,   8,   8,   5},
                new float[] {   5,   3,9999,  72,  72,  48,  48,  24,  24,   3,   3,   5,   3,   0,  48,  48,  24},
                new float[] {  48,  48,  74,9999,   0,   6,   6,  12,  12,  48,  48,  48,  48,  74,   6,   6,  12},
                new float[] {  48,  48,  74,   0,9999,   6,   6,  12,  12,  48,  48,  48,  48,  74,   6,   6,  12},
                new float[] {   8,   8,  50,   6,   6,9999,   0,   8,   8,   8,   8,   8,   8,  50,   0,   0,   8},
                new float[] {   8,   8,  50,   6,   6,   0,9999,   8,   8,   8,   8,   8,   8,  50,   0,   0,   8},
                new float[] {   5,   5,  26,  12,  12,   8,   8,9999,   0,   5,   5,   5,   5,  26,   8,   8,   0},
                new float[] {   5,   5,  26,  12,  12,   8,   8,   0,9999,   5,   5,   5,   5,  26,   8,   8,   0},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,9999,   0,   3,   0,   3,   8,   8,   5},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,   0,9999,   3,   0,   3,   8,   8,   5},
                new float[] {   0,   3,   5,  48,  48,   8,   8,   5,   5,   3,   3,9999,   3,   5,   8,   8,   5},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,   0,   0,   3,9999,   3,   8,   8,   5},
                new float[] {   5,   3,   0,  72,  72,  48,  48,  24,  24,   3,   3,   5,   3,9999,  48,  48,  24},
                new float[] {   8,   8,  50,   6,   6,   0,   0,   8,   8,   8,   8,   8,   8,  50,9999,   0,   8},
                new float[] {   8,   8,  50,   6,   6,   0,   0,   8,   8,   8,   8,   8,   8,  50,   0,9999,   8},
                new float[] {   5,   5,  26,  12,  12,   8,   8,   0,   0,   5,   5,   5,   5,  26,   8,   8,9999}};

            // create problem.
            var problem = TSPHelper.CreateTSP(0, 0, weights);

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (var i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.AreEqual(39, fitness);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.IsTrue(solutionList.Remove(2));
                Assert.IsTrue(solutionList.Remove(3));
                Assert.IsTrue(solutionList.Remove(4));
                Assert.IsTrue(solutionList.Remove(5));
                Assert.IsTrue(solutionList.Remove(6));
                Assert.IsTrue(solutionList.Remove(7));
                Assert.IsTrue(solutionList.Remove(8));
                Assert.IsTrue(solutionList.Remove(9));
                Assert.IsTrue(solutionList.Remove(10));
                Assert.IsTrue(solutionList.Remove(11));
                Assert.IsTrue(solutionList.Remove(12));
                Assert.IsTrue(solutionList.Remove(13));
                Assert.IsTrue(solutionList.Remove(14));
                Assert.IsTrue(solutionList.Remove(15));
                Assert.IsTrue(solutionList.Remove(16));
                Assert.AreEqual(0, solutionList.Count);
            }
        }

        /// <summary>
        /// Tests the solver.
        /// </summary>
        [Test]
        public void TestSolutionbr17Open()
        {
            RandomGeneratorExtensions.GetGetNewRandom = () => new RandomGenerator(4541247);

            var weights = new float[][] {
                new float[] {9999,   3,   5,  48,  48,   8,   8,   5,   5,   3,   3,   0,   3,   5,   8,   8,   5},
                new float[] {   3,9999,   3,  48,  48,   8,   8,   5,   5,   0,   0,   3,   0,   3,   8,   8,   5},
                new float[] {   5,   3,9999,  72,  72,  48,  48,  24,  24,   3,   3,   5,   3,   0,  48,  48,  24},
                new float[] {  48,  48,  74,9999,   0,   6,   6,  12,  12,  48,  48,  48,  48,  74,   6,   6,  12},
                new float[] {  48,  48,  74,   0,9999,   6,   6,  12,  12,  48,  48,  48,  48,  74,   6,   6,  12},
                new float[] {   8,   8,  50,   6,   6,9999,   0,   8,   8,   8,   8,   8,   8,  50,   0,   0,   8},
                new float[] {   8,   8,  50,   6,   6,   0,9999,   8,   8,   8,   8,   8,   8,  50,   0,   0,   8},
                new float[] {   5,   5,  26,  12,  12,   8,   8,9999,   0,   5,   5,   5,   5,  26,   8,   8,   0},
                new float[] {   5,   5,  26,  12,  12,   8,   8,   0,9999,   5,   5,   5,   5,  26,   8,   8,   0},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,9999,   0,   3,   0,   3,   8,   8,   5},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,   0,9999,   3,   0,   3,   8,   8,   5},
                new float[] {   0,   3,   5,  48,  48,   8,   8,   5,   5,   3,   3,9999,   3,   5,   8,   8,   5},
                new float[] {   3,   0,   3,  48,  48,   8,   8,   5,   5,   0,   0,   3,9999,   3,   8,   8,   5},
                new float[] {   5,   3,   0,  72,  72,  48,  48,  24,  24,   3,   3,   5,   3,9999,  48,  48,  24},
                new float[] {   8,   8,  50,   6,   6,   0,   0,   8,   8,   8,   8,   8,   8,  50,9999,   0,   8},
                new float[] {   8,   8,  50,   6,   6,   0,   0,   8,   8,   8,   8,   8,   8,  50,   0,9999,   8},
                new float[] {   5,   5,  26,  12,  12,   8,   8,   0,   0,   5,   5,   5,   5,  26,   8,   8,9999}};

            // create problem.
            var problem = TSPHelper.CreateTSP(0, weights);

            // create the solver.
            var solver = new EAXSolver(new GASettings()
            {
                CrossOverPercentage = 10,
                ElitismPercentage = 1,
                PopulationSize = 100,
                MaxGenerations = 100000,
                MutationPercentage = 0,
                StagnationCount = 100
            });

            for (var i = 0; i < 10; i++)
            {
                // generate solution.
                float fitness;
                var solution = solver.Solve(problem, new TSPObjective(), out fitness);

                // test contents.
                Assert.IsTrue(fitness <= 39);
                var solutionList = new List<int>(solution);
                Assert.AreEqual(0, solutionList[0]);
                Assert.IsTrue(solutionList.Remove(0));
                Assert.IsTrue(solutionList.Remove(1));
                Assert.IsTrue(solutionList.Remove(2));
                Assert.IsTrue(solutionList.Remove(3));
                Assert.IsTrue(solutionList.Remove(4));
                Assert.IsTrue(solutionList.Remove(5));
                Assert.IsTrue(solutionList.Remove(6));
                Assert.IsTrue(solutionList.Remove(7));
                Assert.IsTrue(solutionList.Remove(8));
                Assert.IsTrue(solutionList.Remove(9));
                Assert.IsTrue(solutionList.Remove(10));
                Assert.IsTrue(solutionList.Remove(11));
                Assert.IsTrue(solutionList.Remove(12));
                Assert.IsTrue(solutionList.Remove(13));
                Assert.IsTrue(solutionList.Remove(14));
                Assert.IsTrue(solutionList.Remove(15));
                Assert.IsTrue(solutionList.Remove(16));
                Assert.AreEqual(0, solutionList.Count);
            }
        }
    }
}