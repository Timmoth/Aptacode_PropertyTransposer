﻿using System.Collections.Generic;
using Aptacode.TaskPlex.Interpolators.Easers;

namespace Aptacode.TaskPlex.Interpolators
{
    public class DoubleInterpolator : Interpolator<double>
    {
        public IEnumerable<double> Interpolate(double startValue, double endValue, int stepCount, EaserFunction easer)
        {
            if (stepCount <= 0)
            {
                yield break;
            }

            //The difference between the start and end value
            var totalDifference = endValue - startValue;

            for (var stepIndex = 1; stepIndex <= stepCount; stepIndex++)
            {
                yield return startValue + easer(stepIndex, stepCount) * totalDifference;
            }
        }
    }
}