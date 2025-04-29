using System.Threading.Tasks;
using ApiExtension.Models.RequestModels;
using ApiExtension.Services;
using NAudio.Wave;

namespace ApiTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var reader = new MediaFoundationReader("https://prod-1.storage.jamendo.com/?trackid=1593988&format=mp32&from=e2WOlXv3goEJv3BQCVWEtw%3D%3D%7C0HauUXrWf5S%2F0FXpPEsB7w%3D%3D"))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(reader);
                outputDevice.Play();

                Console.WriteLine("Playing...");
                Console.ReadLine(); 
            }
        }
    }
}
