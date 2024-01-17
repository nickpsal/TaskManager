using NAudio.Wave;

namespace TaskManager.Static
{
    public class StaticFunc
    {
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
                }
            }
        }
    }
}
