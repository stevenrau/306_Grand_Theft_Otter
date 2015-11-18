using System;
using System.Collections.Generic;
using System.Linq;

//for key send
using System.Windows.Forms; //ensure this has been added as a reference
using System.Diagnostics;
using System.Runtime.InteropServices;

//for breathing sensor
using TTLLiveCtrlLib; //ensure TTLAPI.manifest was added to references (or TTLLiveCtrl.dll)

namespace BreathingSensorWithKeyPress
{
    class Program
    {

        //  Arbitrarily chosen size
        const int BUFSIZE = 4096;  

        // The array that will contain data 
        static float[] m_data; 

        // The object that represents the sensor box
        public static TTLLive axTTLLive1;

        // The timer that will invoke the possible keypress
        public static System.Timers.Timer timer;

        // The current data value
        public static float dataA = 0.0f;
        public static float dataB = 0.0f;

        // The previous data value
        public static float prevDataA = 0.0f;
        public static float prevDataB = 0.0f;

        // How much must the value change to register a key press
        public static float threshold = 0.05f;
        
        //an instance of unity that should be currently running, key strokes will be sent to this process
        static Process unityProc;

        // Needed to Enable focus on unity so key strokes end up in that window
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello.");

            //initialize the array that data will be read into
            m_data = new float[BUFSIZE]; 

            // Find the open instance of unity
            unityProc = Process.GetProcessesByName("unity").FirstOrDefault();
            if(unityProc == null)
            {
                Console.WriteLine("Could not find a running instance of Unity.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Found running instance of Unity.");
            }

            // The object that reads the breathing data (the "sensor box")
            axTTLLive1 = new TTLLiveCtrlLib.TTLLive();

            // Create a timer. The parameter is how many milliseconds between ticks
            timer = new System.Timers.Timer(200);
            timer.Elapsed += UseDataValue; // Each timer tick, this method will be called
            timer.AutoReset = true; // Have the timer fire repeated events (true is the default)

            ConnectDevice();
            Console.WriteLine("Device Connected");

            // Start the timer
            timer.Enabled = true;
            Console.WriteLine("Timer started.");
            Console.WriteLine("Reading data...");

            //continuously read data from the sensor
            while (true)
            {
                ReadDataValue();
            }

            Console.WriteLine("Reading finished");
            Console.ReadKey();
        }

      
        //private void buttonConnect_Click(object sender, EventArgs e)
        private static void ConnectDevice()
        {
            try
            {
                int scanned = 0;
                int detected = 0;
                //Cursor = Cursors.WaitCursor;
                axTTLLive1.OpenConnections(
                      (int)TTLLiveCtrlLib.TTLAPI_OPENCONNECTIONS_CMD_BITS.TTLAPI_OCCMD_AUTODETECT
                    , 1000
                    , ref scanned
                    , ref detected
                    );
                //Cursor = Cursors.Default;
                if (axTTLLive1.EncoderCount > 0)
                {
                    SetupChannels();
                    if (axTTLLive1.ChannelCount > 0)
                    {
                        axTTLLive1.StartChannels();
                        //timer1.Start();
                        //timer1.Enabled = true;
                    }
                }
                else
                {
                    //MessageBox.Show("No encoder found.");
                    System.Console.WriteLine("No encoder found.");
                }
            }
            catch (Exception Ex) //changed from COMException to Exception
            {
                //timer1.Stop();
                axTTLLive1.CloseConnections();
                string S = "Exception! : ";
                S += Ex.Message;
                //MessageBox.Show(S);
                System.Console.WriteLine(S);
            }
        }


        private static void SetupChannels()
        {
            System.Console.WriteLine("Setting up channels.");

            axTTLLive1.AutoSetupChannels();
            int hChan = axTTLLive1.GetFirstChannelHND();
            while (hChan >= 0)
            {
                // Setting SensorType sets default unit type.
                int sensor_id = axTTLLive1.get_SensorID(hChan);
                axTTLLive1.set_SensorType(hChan, sensor_id);
                hChan = axTTLLive1.GetNextChannelHND();
            }
        }


        // Each timer tick, this method is called.
        // DataA and DataB should contain a valid float
        // A key press is sent to unity if the sensor data shows breathing in or out
        private static void UseDataValue(Object source, System.Timers.ElapsedEventArgs e)
        {

            //Console.WriteLine("using Data: " + dataA.ToString("####.###"));
            float difA = dataA - prevDataA;
            float difB = dataB - prevDataB;

            // Is player A breathing?
            if (Math.Abs(difA) > threshold)
            {
                if (difA > 0) //positive
                {
                    //breathing in
                    GenerateKeyStroke("i");
                    Console.WriteLine("printing i");
                }
                else //negative
                {
                    //breathing out
                    GenerateKeyStroke("o");
                    Console.WriteLine("printing o");
                }
            }

            //is player B breathing?
            if (Math.Abs(difB) > threshold)
            {
                if (difB > 0) //positive
                {
                    //breathing in
                    GenerateKeyStroke("k");
                    Console.WriteLine("printing k");
                }
                else //negative
                {
                    //breathing out
                    GenerateKeyStroke("l");
                    Console.WriteLine("printing l");
                }
            }

            //update the previous values
            prevDataA = dataA;
            prevDataB = dataB;

        }

        // Actually send a key stroke to unity
        private static void GenerateKeyStroke(string key)
        {
            IntPtr p = unityProc.MainWindowHandle;
            SetForegroundWindow(p);
            SendKeys.SendWait(key);
        }

        // This value is called as fast as possible to read data from the sensor.
        // Some of the data that is produced is empty. The value of DataA and DataB are only set for valid float values.
        // Originally, this method was called by a timer, but that was long ago and the Timer API has changed.
        private static void ReadDataValue()
        {
            try
            {
                int hChan = axTTLLive1.GetFirstChannelHND();
                while (hChan >= 0)
                {

                    int available = axTTLLive1.get_SamplesAvailable(hChan);
                    if (available > BUFSIZE) available = BUFSIZE;
                    int readSuccess = axTTLLive1.ReadChannelData(hChan, out m_data[0], ref available);

                    if (1 != readSuccess)
                    {
                        // Read from channel A
                        if ((int)TTLLiveCtrlLib.TTLAPI_CHANNELS.TTLAPI_CHANNEL_A == hChan)
                        {
                            // If the value was non null update dataB
                            if (m_data[0].ToString("####.###") != "")
                            {
                                dataA = m_data[0];
                                //Console.WriteLine(m_data[0].ToString("####.###"));
                            }
                        }
                        
                        // Read from channel B
                        else if ((int)TTLLiveCtrlLib.TTLAPI_CHANNELS.TTLAPI_CHANNEL_B == hChan)
                        {
                            // If the value was non null update dataB
                            if (m_data[0].ToString("####.###") != "")
                            {
                                dataB = m_data[0];
                                //Console.WriteLine(m_data[0].ToString("####.###"));
                            }
                        }
                    }
                    else
                    {
                        //System.Console.WriteLine("not setting data");
                    }
                    hChan = axTTLLive1.GetNextChannelHND();
                }
            }
            catch (Exception Ex) //changed from COMException to Exception
            {
                try
                {
                    timer.Enabled = false;
                    axTTLLive1.CloseConnections();
                    string S = "Exception! : ";
                    S += Ex.Message;
                    S += '\n';
                    S += "Stopping Channels...";
                    Console.WriteLine(S);

                }
                catch (Exception Exxx)
                {
                    timer.Enabled = false;
                    // oops how unlucky!!!
                    String S = "Unexpected exception: ";
                    S += Exxx.Message;
                    Console.WriteLine(S);
                }
            }
        }
    }
}
