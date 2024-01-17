﻿using System.Collections.ObjectModel;
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
        private UserTask? SelectedTask;
        private DateTime currentDate;
        private ObservableCollection<UserTask> tasks;
        public LoadApp()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            TaskDate.SelectedDate = currentDate;
            ScheduleTaskDate = currentDate.ToString("dd/MM/yyyy");
            tasks = StaticFunc.readJsonData();
            DeleteTaskButton.IsEnabled = false;
            ExporttoJson.IsEnabled = false;
            showData();
        }

        private void showData()
        {
            if (tasks != null)
            {
                MainList.ItemsSource = tasks
                    .Where(task => task.TaskScheduleDate == ScheduleTaskDate)
                    .OrderBy(task => task.StartTime)
                    .ToList();
            }
            else
            {
                MainList.ItemsSource = new ObservableCollection<UserTask>();
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
                await StaticFunc.exporttoJson(tasks);
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

        private async void ExporttoJson_Click(object sender, RoutedEventArgs e)
        {
            await StaticFunc.exporttoJson(tasks);
            ExporttoJson.IsEnabled = false;
        }

        private void MainList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ExporttoJson.IsEnabled = true;
        }
    }
}