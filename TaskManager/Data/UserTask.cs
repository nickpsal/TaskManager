namespace TaskManager.Data
{
    public class UserTask
    {
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public string TaskSetupDate { get; set; }
        public string TaskScheduleDate { get; set; }
        public string StartTime { get; set; }
        public int TaskDuration { get; set; }
        public string EndTime { get; set; }

        public UserTask(string TaskName, string TaskDescription, string TaskSetupDate, string TaskScheduleDate, string StartTime, int TaskDuration, string endTime)
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
            this.TaskSetupDate = TaskSetupDate;
            this.TaskScheduleDate = TaskScheduleDate;
            this.StartTime = StartTime;
            this.TaskDuration = TaskDuration;
            this.EndTime = endTime;
        }
    }
}
