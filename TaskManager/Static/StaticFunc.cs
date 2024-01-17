using NAudio.Wave;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using TaskManager.Data;

namespace TaskManager.Static
{
    public class StaticFunc
    {
        private static readonly string JsonPath = "Data/tasks.json";

        public static async Task PlaySound(string filename)
        {
            // Specify the path to your MP3 file
            string filePath = "Audio/" + filename + ".mp3";
            // Create an instance of the Mp3FileReader class from NAudio
            await using (var reader = new Mp3FileReader(filePath))
            {
                // Create an instance of the WaveOutEvent class for playback
                using (var waveOut = new WaveOutEvent())
                {
                    // Set the desired volume (0.5 is half of the maximum volume)
                    waveOut.Volume = 0.5f;
                    // Initialize the WaveOutEvent with the Mp3FileReader
                    waveOut.Init(reader);
                    // Start playback
                    waveOut.Play();
                    // Wait for the playback to complete (you can perform other actions here)
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        
                    }
                }
            }
        }

        public static ObservableCollection<UserTask> readJsonData()
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(JsonPath);
            // Deserialize the JSON data into a list of UserTask objects
            ObservableCollection<UserTask> tasks = JsonSerializer.Deserialize<ObservableCollection<UserTask>>(jsonData)!;
            return tasks!;
        }

        public static async Task exporttoJson(ObservableCollection<UserTask> tasks)
        {
            // Read the existing JSON data from the file
            string existingData = await File.ReadAllTextAsync(JsonPath);
            // Configure JsonSerializer settings to include line breaks
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true, // This ensures the output is formatted with indentation
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            // Serialize the updated list of UserTask objects to JSON
            string updatedData = JsonSerializer.Serialize(tasks, options);
            // Write the updated JSON data back to the file
            File.WriteAllText(JsonPath, updatedData);
        }

        public static async Task savetoJson(UserTask newTask)
        {
            // Read the existing JSON data from the file
            string existingData = await File.ReadAllTextAsync(JsonPath);
            // Deserialize the existing JSON data into a list of UserTask objects
            ObservableCollection<UserTask> existingTasks = JsonSerializer.Deserialize<ObservableCollection<UserTask>>(existingData) ?? new ObservableCollection<UserTask>();
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
    }
}
