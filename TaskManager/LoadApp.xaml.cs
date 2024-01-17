using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Data;
using TaskManager.Static;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for LoadApp.xaml
    /// </summary>
    public partial class LoadApp : UserControl
    {
        private string ScheduleTaskDate;
        private string JsonPath;
        private UserTask? SelectedTask;
        private DateTime currentDate;
        private List<UserTask> tasks = new();
        public LoadApp()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            TaskDate.SelectedDate = currentDate;
            ScheduleTaskDate = currentDate.ToString("dd/MM/yyyy");
            JsonPath = "Data/tasks.json";
            tasks = readJsonData();
            DeleteTaskButton.IsEnabled = false;
            showData();
        }

        private List<UserTask> readJsonData()
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(JsonPath);
            // Deserialize the JSON data into a list of UserTask objects
            List<UserTask> tasks = JsonSerializer.Deserialize<List<UserTask>>(jsonData)!;
            if (tasks is null)
            {
                MessageBox.Show("Το Αρχείο Δημιουργήθηκε Κενό");
            }
            return tasks!;
        }

        private void savetoJson()
        {
            // Serialize the updated list of UserTask objects to JSON
            string updatedData = JsonSerializer.Serialize(tasks);
            // Write the updated JSON data back to the file
            File.WriteAllText(JsonPath, updatedData);
        }

        private void showData()
        {
            if (tasks.Count != 0)
            {
                MainList.ItemsSource = tasks.FindAll(t => t.TaskScheduleDate == ScheduleTaskDate).OrderBy(task => task.StartTime).ToList();
            }
        }

        private void AppInfo(object sender, RoutedEventArgs e)
        {
            string message = "Task Manager - Εφαρμογή Διαχειριστής Εργασιών Version 1.0\n";
            message += "Δημιουργήθηκε με την c#, .net 8.0 και wpf\n";
            message += "Όνομα Προγραμματιστή : Ψαλτάκης Νικόλαος (npsalt)\n";
            message += "(C) 1/2024";
            MessageBox.Show(message, "Πληροφορίες");
        }

        private void AddNewTask(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new AddTaskWindow((DateTime)TaskDate.SelectedDate!);

        }

        private void TaskDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskDate.SelectedDate.HasValue)
            {
                DateTime selectedDate = TaskDate.SelectedDate.Value;
                ScheduleTaskDate = selectedDate.ToString("dd/MM/yyyy");
                showData();
            }
        }

        private async void RemoveTask(object sender, RoutedEventArgs e)
        {
            if (SelectedTask is not null)
            {
                tasks.Remove(SelectedTask);
                savetoJson();
                DeleteTaskButton.IsEnabled = false;
                showData();
                await StaticFunc.PlaySound("deleteTask");
            }
        }

        private void MainList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainList.SelectedItem is not null)
            {
                SelectedTask = ((UserTask)MainList.SelectedItem);
                DeleteTaskButton.IsEnabled = true;
            } 
        }
    }
}

