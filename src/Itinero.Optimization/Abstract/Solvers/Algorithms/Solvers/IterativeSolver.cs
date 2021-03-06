﻿/*
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

using Itinero.Optimization.Algorithms.Solvers.Objective;
using System.Text;

namespace Itinero.Optimization.Algorithms.Solvers
{
    /// <summary>
    /// A solver that let's another solver try n-times, apply some operator(s), and keeps the best obtained solution.
    /// </summary>
    public class IterativeSolver<TWeight, TProblem, TObjective, TSolution, TFitness> : SolverBase<TWeight, TProblem, TObjective, TSolution, TFitness>
        where TObjective : ObjectiveBase<TProblem, TSolution, TFitness>
        where TWeight : struct
        where TSolution : ISolution
    {
        private readonly int _n; // The number of times to try.
        private readonly ISolver<TWeight, TProblem, TObjective, TSolution, TFitness> _solver; // The solver to try.
        private readonly SolverDelegates.StopConditionDelegate<TProblem, TObjective, TSolution> _stopCondition; // Holds the stop condition.
        private readonly IOperator<TWeight, TProblem, TObjective, TSolution, TFitness>[] _operators;

        /// <summary>
        /// Creates a new iterative improvement solver.
        /// </summary>
        public IterativeSolver(ISolver<TWeight, TProblem, TObjective, TSolution, TFitness> solver, int n, 
            params IOperator<TWeight, TProblem, TObjective, TSolution, TFitness>[] operators)
        {
            _solver = solver;
            _n = n;
            _operators = operators;
        }

        /// <summary>
        /// Creates a new iterative improvement solver.
        /// </summary>
        /// <param name="solver">The solver.</param>
        /// <param name="n">The numbers of iterations.</param>
        /// <param name="stopCondition">The stop condition.</param>
        /// <param name="operators">The operators to apply.</param>
        public IterativeSolver(ISolver<TWeight, TProblem, TObjective, TSolution, TFitness> solver,
         int n,
            SolverDelegates.StopConditionDelegate<TProblem, TObjective, TSolution> stopCondition,
            params IOperator<TWeight, TProblem, TObjective, TSolution, TFitness>[] operators)
        {
            _solver = solver;
            _n = n;
            _stopCondition = stopCondition;
            _operators = operators;
        }

        /// <summary>
        /// Returns the name of this solver.
        /// </summary>
        public override string Name
        {
            get
            {
                if (_operators != null && 
                    _operators.Length > 0)
                {
                    var op = new StringBuilder();
                    op.Append(_operators[0].Name);
                    for (var i = 1; i < _operators.Length; i++)
                    {
                        op.Append('+');
                        op.Append(_operators[i].Name);
                    }
                    if (_n == 1)
                    {
                        return string.Format("[{0}+{1}]", _solver.Name, op.ToString());
                    }
                    return string.Format("ITER_[{0}x({1}+{2}]", _n, _solver.Name, op.ToString());
                }
                if (_n == 1)
                {
                    return string.Format("{0}", _solver.Name);
                }
                return string.Format("ITER_[{0}x{1}]", _n, _solver.Name);
            }
        }

        /// <summary>
        /// Solves the given problem.
        /// </summary>
        /// <param name="problem">The problem to solve.</param>
        /// <param name="objective">The objective to reach.</param>
        /// <param name="bestFitness">The fitness of the solution found.</param>
        /// <returns></returns>
        public override TSolution Solve(TProblem problem, TObjective objective, out TFitness bestFitness)
        {
            var i = 0;
            var best = default(TSolution);
            bestFitness = objective.Infinite;
            while (i < _n && !this.IsStopped &&
                (_stopCondition == null || best == null || !_stopCondition.Invoke(i, problem, objective, best)))
            {
                TFitness nextFitness;

                Itinero.Logging.Logger.Log("IterativeSolver", Itinero.Logging.TraceEventType.Verbose,
                    "Started iteration {0}: fitness is {1}", i, bestFitness);

                // execute solver.
                var nextSolution = _solver.Solve(problem, objective, out nextFitness);
                if (objective.IsBetterThan(problem, nextFitness, bestFitness) ||
                    best == null)
                { // yep, found a better solution!
                    Itinero.Logging.Logger.Log("IterativeSolver", Itinero.Logging.TraceEventType.Verbose,
                        "Found a better solution at iteration {0}: {1} -> {2}", i, bestFitness, nextFitness);

                    best = (TSolution)nextSolution.Clone();
                    bestFitness = nextFitness;                    

                    // report new solution.
                    this.ReportIntermidiateResult(best);
                }

                // apply operators if any.
                if (_operators != null)
                {
                    for (var o = 0; o < _operators.Length; o++)
                    {
                        TFitness delta;
                        if (_operators[o].Apply(problem, objective, nextSolution, out delta))
                        { // yep, found a better solution!
                            nextFitness = objective.Calculate(problem, nextSolution);
                            if (objective.IsBetterThan(problem, nextFitness, bestFitness))
                            { // yep, found a better solution!
                                best = (TSolution)nextSolution.Clone();
                                bestFitness = nextFitness;

                                // report new solution.
                                this.ReportIntermidiateResult(best);
                            }
                        }

                        // always apply to best.
                        nextSolution = (TSolution)best.Clone();
                    }
                }

                i++;
            }
            return best;
        }
    }
}