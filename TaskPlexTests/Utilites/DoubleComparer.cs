﻿using System;
using System.Collections.Generic;

namespace Aptacode.TaskPlex.Tests.Utilites
{
    public class DoubleComparer : IEqualityComparer<double>
    {
        private readonly double _epsilon;

        public DoubleComparer()
        {
            _epsilon = 0.001;
        }

        public bool Equals(double x, double y)
        {
            return Math.Abs(x - y) < _epsilon;
        }

        public int GetHashCode(double obj)
        {
            return obj.GetHashCode();
        }
    }
}