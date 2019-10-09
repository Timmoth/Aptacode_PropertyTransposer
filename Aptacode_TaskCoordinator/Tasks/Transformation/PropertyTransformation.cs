﻿using System;
using System.Reflection;

namespace TaskCoordinator.Tasks.Transformation
{
    public abstract class PropertyTransformation : BaseTask
    {
        public TimeSpan SteoDuration = TimeSpan.FromMilliseconds(20);

        public object Target { get; set; }
        public PropertyInfo Property { get; set; }
        public TimeSpan TaskDuration { get; set; }
        public PropertyTransformation(object target, PropertyInfo property, TimeSpan duration)
        {
            Target = target;
            Property = property;
            TaskDuration = duration;
        }

        public override bool CollidesWith(BaseTask otherTask)
        {
            PropertyTransformation otherTransformation = otherTask as PropertyTransformation;
            if (otherTransformation == null)
                return false;
            else
                return Target == otherTransformation.Target && Property.Name == otherTransformation.Property.Name;
        }
    }

    public abstract class PropertyTransformation<T> : PropertyTransformation
    {
        public Func<T> DestinationValue { get; set; }
        public PropertyTransformation(object target, PropertyInfo property, Func<T> destinationValue, TimeSpan duration) : base(target, property, duration)
        {
            DestinationValue = destinationValue;
        }

        public PropertyTransformation(object target, PropertyInfo property, T destinationValue, TimeSpan duration) : base(target, property, duration)
        {
            DestinationValue = new Func<T>(() => { return destinationValue; });
        }

        protected T GetStartValue()
        {
            return (T)Property.GetValue(Target);
        }
        protected T GetEndValue()
        {
            return (T)DestinationValue.Invoke();
        }

        protected void UpdateValue(T value)
        {
            Property.SetValue(Target, value);
        }
    }
}
