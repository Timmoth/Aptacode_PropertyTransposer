﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aptacode.TaskPlex.Tasks
{
    public class SequentialGroupTask : GroupTask
    {
        /// <summary>
        ///     Execute the specified tasks sequentially in the order they occur in the input list
        /// </summary>
        /// <param name="tasks"></param>
        public SequentialGroupTask(List<BaseTask> tasks) : base(tasks)
        {
            Duration = GetTotalDuration(Tasks);
        }

        protected sealed override TimeSpan GetTotalDuration(IEnumerable<BaseTask> tasks)
        {
            return tasks.Aggregate(TimeSpan.Zero, (current, task) => current.Add(task.Duration));
        }

        protected override async Task InternalTask()
        {
            foreach (var task in Tasks)
            {
                await task.StartAsync(CancellationToken).ConfigureAwait(false);
            }
        }
    }
}