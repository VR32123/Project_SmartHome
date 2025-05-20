

using System;
using NAudio.Wave; // NAudio library to handle audio playback.

namespace MySmartHomeAssistant // organize the code and prevent naming conflicts.
{
    class Program //the main container of this application.
    {
        // Variables
        static bool isLightOn = false; // this tracks whether the light is currently on.
        static int currentTemperature = 20;// Default  temperature set.
        static WaveOutEvent outputDevice; // outputDevice and                                     
        static AudioFileReader reader;   //  reader handle music playback using NAudio.

        static void Main(string[] args) //the entry point of this program.
        {
            Console.WriteLine("Welcome to My Smart Home Assistant!");// welcome greeting
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("");  // NOTE: these are just for a line space in console.
            Console.WriteLine("Available commands:");
            Console.WriteLine(""); // below shows available commands for lights.
            Console.WriteLine(" To Control Lights, type:       light on / light off");
            Console.WriteLine(""); // below shows available commands for temperature.
            Console.WriteLine(" To Control Temperature, type:  set temp / show temp");
            Console.WriteLine(""); // below shows available commands for music.
            Console.WriteLine(" To Play Your Music, type:      play music / stop music");
            Console.WriteLine(""); // below commands shows how to exit.
            Console.WriteLine(" To Quit the program, type:     exit");

            while (true) // Keeps the program running in a loop until the user types "exit".
            {
                Console.Write("\nPlease enter your command: ");// asked users command.
                string command = Console.ReadLine().Trim().ToLower();
                // above string command reads user input, removes extra spaces with Trim(), 
                // and converts it to lowercase.

                switch (command) // switch Statement for Commands which Handles user commands.
                {
                    case "light on":
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        HandleLight(true);
                        break;

                    case "light off":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        HandleLight(false);
                        break;

                    case "set temp":
                        SetTemperature();
                        break;

                    case "show temp":
                        Console.WriteLine("Current temperature is " + currentTemperature + "°C.");
                        break;

                    case "play music":
                        PlayMusic();
                        break;

                    case "stop music":
                        StopMusic();
                        Console.WriteLine("music stopped...");
                        break;

                    case "exit":
                        StopMusic(); // Stop music before exiting
                        Console.WriteLine("");          // program exiting greetings.
                        Console.WriteLine("Exiting My Smart Home Assistant. Goodbye!");
                        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                        return;

                    default:  // if invalid command is entered, asked user to try again.
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }

        static void HandleLight(bool turnOn) // Controls light status.
        {
            if (turnOn)
            {
                if (isLightOn)// if user type light on, it tells user that light is already on.
                    Console.WriteLine("The light is already ON.");
                else
                {
                    isLightOn = true;// if the light was off then lights will turn on.
                    Console.WriteLine("The light is now ON.");
                }
            }
            else
            {
                if (!isLightOn)//if user type light off, it tells user that light is already off.
                    Console.WriteLine("The light is already OFF.");
                else
                {
                    isLightOn = false;// if the light is on then lights will turn off.
                    Console.WriteLine("The light is now OFF.");
                }
            }
        }

        static void SetTemperature() // Set and show temperature method.
        {
            Console.Write("Enter desired temperature (14 - 30 °C): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int temp))//tries to convert the input to an integer.
            {
                if (temp >= 14 && temp <= 30)// temperature range validation
                {
                    currentTemperature = temp;  // current temperature 
                    Console.WriteLine("Temperature set to " + currentTemperature + "°C.");
                }
                else
                {           // ask user to enter a desired temperature between 14°C and 30°C.
                    Console.WriteLine("Please enter a temperature between 14 and 30.");
                }
            }
            else
            {         // shows user put invalid number and asking to put valid number again.
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        static void PlayMusic() // Play music method
        {                  // ask user to put full file path to music file.
            Console.WriteLine("");
            Console.WriteLine("Enter full file path to music file then press enter (e.g., C:\\Music\\song.mp3): ");
            Console.WriteLine("");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))// Checks if the file actually exists at the given path.
            {
                Console.WriteLine("");
                Console.WriteLine("File not found. Please try again...");
                return;  // 
            }

            try
            {
                StopMusic(); // Stop any existing music
                reader = new AudioFileReader(filePath); // read MP3 or other audio files.
                outputDevice = new WaveOutEvent(); // prepare speakers to play the music.
                outputDevice.Init(reader);// prepares the audio player to play a sound file.
                outputDevice.Play(); // then Play the music or any sound file.
                Console.WriteLine("Now playing your favourite music...");
            }
            catch (Exception ex)
            {                    // tells user if any error occurs.
                Console.WriteLine("Error playing music: " + ex.Message);
            }
        }

        static void StopMusic()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop(); // stop the music.
                outputDevice.Dispose(); // Frees system resources.
                outputDevice = null; // Prevents accidental reuse.
            }




        }
    }
}

