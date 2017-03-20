﻿using Itinero.Optimization.Test.TSP;
using NUnit.Common;
using NUnitLite;
using System;
using System.Reflection;

namespace Itinero.Test.Runner
{
    class Program
    {
        static int Main(string[] args)
        {
            var res = new AutoRun(typeof(BruteForceSolverTests).GetTypeInfo().Assembly)
                .Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);

#if DEBUG
            Console.ReadLine();
#endif
            return res;
        }
    }
}