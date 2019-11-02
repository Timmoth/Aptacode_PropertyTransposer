﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Aptacode.TaskPlex.Tasks.Transformation
{
    public abstract class PropertyTransformation : BaseTask
    {
        protected readonly Stopwatch StepTimer;

        protected PropertyTransformation(
            object target,
            string property,
            TimeSpan duration,
            TimeSpan stepDuration) : base(duration)
        {
            Target = target;
            Property = property;
            StepDuration = stepDuration;
            StepTimer = new Stopwatch();
        }

        /// <summary>
        ///     the object who's property is to be transformed
        /// </summary>
        public object Target { get; }

        /// <summary>
        ///     The property to be updated
        /// </summary>
        public string Property { get; }

        /// <summary>
        ///     The time between each property update
        /// </summary>
        protected TimeSpan StepDuration { get; }

        public override int GetHashCode()
        {
            return (Target, Property).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is PropertyTransformation other && StepTimer.Equals(other.StepTimer);
        }

        protected async Task DelayAsync(int currentStep)
        {
            var millisecondsAhead =
                (int) (StepDuration.TotalMilliseconds * currentStep - StepTimer.ElapsedMilliseconds);
            //the Task.Delay function will only accurately sleep for >8ms
            if (millisecondsAhead > 8)
            {
                await Task.Delay(millisecondsAhead, CancellationToken.Token).ConfigureAwait(false);
            }
        }
    }

    public abstract class PropertyTransformation<T> : PropertyTransformation
    {
        private readonly Func<T> _endValue;
        private readonly Func<T> _startValue;
        private readonly Action<T> _valueUpdater;

        protected PropertyTransformation(
            object target,
            string property,
            Func<T> startValue,
            Func<T> endValue,
            Action<T> valueUpdater,
            TimeSpan duration,
            TimeSpan stepDuration) : base(target, property, duration, stepDuration)
        {
            _startValue = startValue;
            _endValue = endValue;
            _valueUpdater = valueUpdater;
        }

        /// <summary>
        ///     When invoked returns the destination value of the transformation
        /// </summary>
        protected T GetStartValue()
        {
            return _startValue.Invoke();
        }

        protected T GetEndValue()
        {
            return _endValue.Invoke();
        }

        protected void SetValue(T value)
        {
            _valueUpdater.Invoke(value);
        }
    }
}