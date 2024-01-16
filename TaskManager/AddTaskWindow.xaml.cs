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
        public AddTaskWindow(DateTime selectedDate)
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Now;
            newTaskDate.SelectedDate = selectedDate;
            JsonPath = "Data/tasks.json";
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

        private void savetoJson(UserTask newTask)
        {
            // Read the existing JSON data from the file
            string existingData = File.ReadAllText(JsonPath);
            // Deserialize the existing JSON data into a list of UserTask objects
            List<UserTask> existingTasks = JsonSerializer.Deserialize<List<UserTask>>(existingData) ?? new List<UserTask>();
            // Add the new UserTask objects to the existing list
            existingTasks.Add(newTask);
            // Configure JsonSerializer settings to include line breaks
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true, // This ensures the output is formatted with indentation
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            // Serialize the updated list of UserTask objects to JSON
            string updatedData = JsonSerializer.Serialize(existingTasks, options);
            // Write the updated JSON data back to the file
            File.WriteAllText(JsonPath, updatedData);
        }

        private void NewTaskAdd_Click(object sender, RoutedEventArgs e)
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
                    savetoJson(newTask);
                    MessageBox.Show("Η Εργασία Αποθηκεύτηκε με Επιτυχία", "Πληροφορία");
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
