﻿using System;
using System.Drawing;
using Aptacode.TaskPlex.Interpolators;
using Aptacode.TaskPlex.Interpolators.Easers;

namespace Aptacode.TaskPlex.Tasks.Transformations.Interpolation
{
    public sealed class ColorTransformation<TClass> : InterpolatedTransformation<TClass, Color> where TClass : class
    {
        private ColorTransformation(TClass target,
            string property,
            Func<Color> endValue,
            TimeSpan duration, EaserFunction easerFunction = null) : base(target,
            property,
            endValue,
            duration,
            new ColorInterpolator(), easerFunction)
        {
        }

        /// <summary>
        ///     Transform an int property on the target object to the value returned by the given Func at intervals
        ///     specified by     the step duration up to the task duration
        /// </summary>
        public static ColorTransformation<T> Create<T>(T target, string property, Color endValue, TimeSpan duration,
            EaserFunction easerFunction = null) where T : class
        {
            try
            {
                return new ColorTransformation<T>(target, property, () => endValue, duration, easerFunction);
            }
            catch
            {
                return null;
            }
        }
    }
}