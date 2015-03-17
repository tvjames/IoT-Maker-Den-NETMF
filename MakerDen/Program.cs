using Coatsy.Netduino.NeoPixel;
using Coatsy.Netduino.NeoPixel.Grid;
using Glovebox.MicroFramework.Sensors;
using Glovebox.Netduino.Actuators;
using Glovebox.Netduino.Sensors;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.Threading;

namespace MakerDen {
    public class Program : MakerBaseIoT {

        public static void Main() {
            // main code marker
            using (SensorTemp temp = new SensorTemp(Pins.GPIO_PIN_D8, 10000, "temp01"))
            using (SensorLight light = new SensorLight(AnalogChannels.ANALOG_PIN_A0, 1000, "light01"))
            using (var rgb = new RgbLed(Pins.GPIO_PIN_D3, Pins.GPIO_PIN_D5, Pins.GPIO_PIN_D6, "rgb01")) 
            {
                uint index = 0;
                while (true) {

                    Debug.Print(light.ToString());
                    Debug.Print(temp.ToString());

                    var led = RgbLed.Led.Red;
                    if (index % 3 == 1)
                        led = RgbLed.Led.Blue;
                    if (index % 3 == 2)
                        led = RgbLed.Led.Green;

                    rgb.On(led);
                    Thread.Sleep(500);
                    rgb.Off(led);
                    Thread.Sleep(500);
                    index++;
                }
            }
        }
    }
}