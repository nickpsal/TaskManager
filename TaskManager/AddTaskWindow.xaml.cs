using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.Data;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : UserControl
    {
        private string ScheduleTaskDate;
        private string JsonPath;
        public AddTaskWindow()
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Now;
            JsonPath = "Data/tasks.json";
            ScheduleTaskDate = currentDate.ToString("dd/MM/yyyy");
            fillComboBox();
        }

        public void fillComboBox()
        {
            for (int i = 0; i<24; i++)
            {
                if (i < 10)
                {
                    newTaskStartHour.Items.Add("0" + i);
                }else
                {
                    newTaskStartHour.Items.Add(i);
                }
            }
            for (int j = 0; j<60; j++)
            {
                if (j < 10)
                {
                    newTaskStartMin.Items.Add("0" + j);
                }
                else
                {
                    newTaskStartMin.Items.Add(j);
                }
            }
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
                ScheduleTaskDate = selectedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }

        private void savetoJson(UserTask newTask)
        {
            // Read the existing JSON data from the file
            string existingData = File.ReadAllText(JsonPath);
            // Deserialize the existing JSON data into a list of UserTask objects
            List<UserTask> existingTasks = JsonSerializer.Deserialize<List<UserTask>>(existingData) ?? new List<UserTask>();
            // Add the new UserTask objects to the existing list
            existingTasks.Add(newTask);
            // Serialize the updated list of UserTask objects to JSON
            string updatedData = JsonSerializer.Serialize(existingTasks);
            // Write the updated JSON data back to the file
            File.WriteAllText(JsonPath, updatedData);
        }

        private void NewTaskAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(newTaskDuration.Text, out int TaskDuration) || TaskDuration <= 0)
            {
                MessageBox.Show("Δεν έδωσες Σωστή Διάρκεια ή δεν Έδωσες Αριθμό", "Προσοχή");
            }else
            {
                //"Task1", "Task 1", DateTime.Now, DateTime.Parse("13/01/2024"), DateTime.Parse("20:00:00"), 60, DateTime.Parse("20:00:00").AddMinutes(60));
                string TaskName = newTaskName.Text;
                string TaskDesc = newTaskDesc.Text;
                DateTime TaskSchedule = DateTime.Now;
                DateTime TaskDate = DateTime.Parse(ScheduleTaskDate);
                DateTime TaskTimeStart = DateTime.Parse(newTaskStartHour.Text + ":" + newTaskStartMin.Text);
                DateTime TaskTimeEnd = TaskTimeStart.AddMinutes(TaskDuration);
                UserTask newTask = new(TaskName, TaskDesc, TaskSchedule, TaskDate, TaskTimeStart, TaskDuration ,TaskTimeEnd);
                savetoJson(newTask);
                MessageBox.Show("Η Εργασία Αποθηκεύτηκε με Επιτυχία");
                Window window = Window.GetWindow(this);
                window.Content = new LoadApp();
            }
        }
    }
}
