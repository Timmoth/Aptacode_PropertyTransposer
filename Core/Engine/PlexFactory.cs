﻿using System;
using System.Drawing;
using System.Linq;
using Aptacode.TaskPlex.Engine.Tasks;
using Aptacode.TaskPlex.Engine.Tasks.Transformations;
using Aptacode.TaskPlex.Engine.Tasks.Transformations.Interpolation;
using Aptacode.TaskPlex.Interpolation.Easers;

namespace Aptacode.TaskPlex.Engine
{
    public static class PlexFactory
    {
        public static SequentialGroupTask Sequential(params BaseTask[] tasks)
        {
            return new SequentialGroupTask(tasks.ToList());
        }

        public static ParallelGroupTask Parallel(params BaseTask[] tasks)
        {
            return new ParallelGroupTask(tasks.ToList());
        }

        public static RepeatTask Repeat(BaseTask task, int count)
        {
            return new RepeatTask(task, count);
        }

        public static WaitTask Wait(TimeSpan duration)
        {
            return new WaitTask(duration);
        }

        public static IntTransformation<T> Create<T>(T target, string property, TimeSpan duration,
            EaserFunction easerFunction = null, bool useStartValue = true,
            params int[] values) where T : class
        {
            return new IntTransformation<T>(target, property,
                duration, easerFunction, useStartValue, values);
        }

        public static DoubleTransformation<T> Create<T>(T target, string property, TimeSpan duration,
            EaserFunction easerFunction = null, bool useStartValue = true,
            params double[] values) where T : class
        {
            return new DoubleTransformation<T>(target, property,
                duration, easerFunction, useStartValue, values);
        }

        public static StringTransformation<T> Create<T>(T target, string property, TimeSpan duration, string endValue)
            where T : class
        {
            return new StringTransformation<T>(target, property, duration, endValue);
        }

        public static ColorTransformation<T> Create<T>(T target, string property, TimeSpan duration,
            EaserFunction easerFunction = null, bool useStartValue = true,
            params Color[] values) where T : class
        {
            return new ColorTransformation<T>(target, property, duration, easerFunction, useStartValue, values);
        }

        public static PointTransformation<T> Create<T>(T target, string property, TimeSpan duration,
            EaserFunction easerFunction = null, bool useStartValue = true,
            params Point[] values) where T : class
        {
            return new PointTransformation<T>(target, property,
                duration, easerFunction, useStartValue, values);
        }

        public static BoolTransformation<T> Create<T>(T target, string property, TimeSpan duration, bool value)
            where T : class
        {
            return new BoolTransformation<T>(target, property, duration, value);
        }
    }
}