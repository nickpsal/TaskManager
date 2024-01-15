using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public class UserTask
    {
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public DateTime TaskSetupDate { get; set; }
        public DateTime TaskScheduleDate { get; set; }
        public DateTime StartTime { get; set; }
        public int TaskDuration { get; set; }
        public DateTime EndTime { get; set; }

        public UserTask(string TaskName, string TaskDescription, DateTime TaskSetupDate, DateTime TaskScheduleDate, DateTime StartTime, int TaskDuration, DateTime endTime)
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
            this.TaskSetupDate = TaskSetupDate;
            this.TaskScheduleDate = TaskScheduleDate;
            this.StartTime = StartTime;
            this.TaskDuration = TaskDuration;
            EndTime = endTime;
        }
    }
}
