using System;
using System.ComponentModel;

namespace TaskManager.Data
{
    public class UserTask : INotifyPropertyChanged
    {
        private string taskName = string.Empty;
        private string taskDescription = string.Empty;
        private string taskSetupDate;
        private string taskScheduleDate;
        private string startTime;
        private int taskDuration;
        private string endTime;

        public string TaskName
        {
            get { return taskName; }
            set
            {
                if (value != taskName)
                {
                    taskName = value;
                    OnPropertyChanged(nameof(TaskName));
                }
            }
        }

        public string TaskDescription
        {
            get { return taskDescription; }
            set
            {
                if (value != taskDescription)
                {
                    taskDescription = value;
                    OnPropertyChanged(nameof(TaskDescription));
                }
            }
        }

        public string TaskSetupDate
        {
            get { return taskSetupDate; }
            set
            {
                if (value != taskSetupDate)
                {
                    taskSetupDate = value;
                    OnPropertyChanged(nameof(TaskSetupDate));
                }
            }
        }

        public string TaskScheduleDate
        {
            get { return taskScheduleDate; }
            set
            {
                if (value != taskScheduleDate)
                {
                    taskScheduleDate = value;
                    OnPropertyChanged(nameof(TaskScheduleDate));
                }
            }
        }

        public string StartTime
        {
            get { return startTime; }
            set
            {
                if (value != startTime)
                {
                    startTime = value;
                    OnPropertyChanged(nameof(StartTime));
                }
            }
        }

        public int TaskDuration
        {
            get { return taskDuration; }
            set
            {
                if (value != taskDuration)
                {
                    taskDuration = value;
                    OnPropertyChanged(nameof(TaskDuration));
                }
            }
        }

        public string EndTime
        {
            get { return endTime; }
            set
            {
                if (value != endTime)
                {
                    endTime = value;
                    OnPropertyChanged(nameof(EndTime));
                }
            }
        }

        public UserTask(string taskName, string taskDescription, string taskSetupDate, string taskScheduleDate, string startTime, int taskDuration, string endTime)
        {
            TaskName = taskName;
            TaskDescription = taskDescription;
            this.taskSetupDate = taskSetupDate ?? throw new ArgumentNullException(nameof(taskSetupDate));
            this.taskScheduleDate = taskScheduleDate ?? throw new ArgumentNullException(nameof(taskScheduleDate));
            this.startTime = startTime ?? throw new ArgumentNullException(nameof(startTime));
            TaskDuration = taskDuration;
            this.endTime = endTime ?? throw new ArgumentNullException(nameof(endTime));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
