using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Data;
using TaskManager.Static;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : UserControl
    {
        private string ScheduleTaskDate;
        public AddTaskWindow(DateTime selectedDate)
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Now;
            newTaskDate.SelectedDate = selectedDate;
            ScheduleTaskDate = currentDate.ToString("dd/MM/yyyy");
            FillComboBoxes();
        }

        public void FillComboBox(ComboBox comboBox, int maxValue)
        {
            for (int i = 0; i < maxValue; i++)
            {
                comboBox.Items.Add(i.ToString("D2"));
            }
        }

        public void FillComboBoxes()
        {
            FillComboBox(newTaskStartHour, 24);
            FillComboBox(newTaskStartMin, 60);
        }

        private void returnToTasks_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new LoadApp();
        }

        private void TaskDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (newTaskDate.SelectedDate.HasValue)
            {
                DateTime selectedDate = newTaskDate.SelectedDate.Value;
                ScheduleTaskDate = selectedDate.ToString("dd/MM/yyyy");
            }
        }

        private async void NewTaskAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(newTaskDuration.Text, out int taskDuration) || taskDuration <= 0)
            {
                MessageBox.Show("Δεν έδωσες Σωστή Διάρκεια ή δεν Έδωσες Αριθμό", "Προσοχή");
            }
            else
            {
                string taskName = newTaskName.Text;
                string taskDesc = newTaskDesc.Text;
                string taskSchedule = DateTime.Now.ToString("dd/MM/yyyy");
                string taskDate = DateTime.Parse(ScheduleTaskDate).ToString("dd/MM/yyyy");

                string startTimeStr = $"{newTaskStartHour.Text}:{newTaskStartMin.Text}";
                if (DateTime.TryParse(startTimeStr, out DateTime TimeStart))
                {
                    string taskTimeEnd = TimeStart.AddHours(taskDuration).ToString("HH:mm");
                    string taskTimeStart = TimeStart.ToString("HH:mm");
                    UserTask newTask = new UserTask(taskName, taskDesc, taskSchedule, taskDate, taskTimeStart, taskDuration, taskTimeEnd);
                    await StaticFunc.savetoJson(newTask);
                    await StaticFunc.PlaySound("addTask");
                    Window window = Window.GetWindow(this);
                    window.Content = new LoadApp();
                }
                else
                {
                    MessageBox.Show("Λανθασμένη μορφή ώρας", "Προσοχή");
                }
            }
        }
    }
}
