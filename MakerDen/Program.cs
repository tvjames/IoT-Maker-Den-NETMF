using Coatsy.Netduino.NeoPixel;
using Coatsy.Netduino.NeoPixel.Grid;
using Glovebox.MicroFramework.Sensors;
using Glovebox.Netduino.Actuators;
using Glovebox.Netduino.Sensors;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.Threading;
using Microsoft.SPOT.Net.NetworkInformation;

namespace MakerDen {
    public class Program : MakerBaseIoT {

        public static void Main() {
            // main code marker
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            if (interfaces.Length > 0)
            {
                var tries = 0;
                var ethernet = interfaces[0];
                ethernet.EnableDhcp();
                while (ethernet.IPAddress == "0.0.0.0" && tries < 120)
                {
                    Thread.Sleep(500);
                    tries++;
                }
                Debug.Print("IP Address: " + ethernet.IPAddress);
            }
            
            StartNetworkServices("tvj", true, "5C-86-4A-00-58-3C");

            using (SensorTemp temp = new SensorTemp(Pins.GPIO_PIN_D8, 10000, "temp01"))
            using (SensorLight light = new SensorLight(AnalogChannels.ANALOG_PIN_A0, 1000, "light01"))
            using ( rgb = new RgbLed(Pins.GPIO_PIN_D3, Pins.GPIO_PIN_D5, Pins.GPIO_PIN_D6, "rgb01")) 
            {
                temp.OnBeforeMeasurement += OnBeforeMeasure;
                temp.OnAfterMeasurement += OnMeasureCompleted;
                light.OnBeforeMeasurement += OnBeforeMeasure;
                light.OnAfterMeasurement += OnMeasureCompleted;

                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}