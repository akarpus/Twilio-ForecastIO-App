using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using ForecastIO;
using ForecastIO.Extensions;

namespace Twilio-ForecastIO-App
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = new ForecastIORequest("Your ForecastIO API Key", 45.292f, -75.896f, Unit.si); // Set your location by inputting a longitude/latitude coordinate
            var response = request.Get();

            string temp = Math.Round(response.currently.temperature, 1).ToString();
            string time = response.currently.time.ToDateTime().ToLocalTime().ToShortTimeString().ToString(); //unix timestamp converted to local time

            string AccountSid = "Your Twilio AccountSid Key";
            string AuthToken = "Your Twilio AuthToken Key";
 
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            var message = twilio.SendMessage("Your Twilio Generated Number", "Your Personal Phone Number","TIME: " + time + " WEATHER: " + temp + " degrees", "");
 
            if (message.RestException != null)
            {
                var error = message.RestException.Message;
                Console.WriteLine(error);
            }
        }
    }
}