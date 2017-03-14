using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Impinj.OctaneSdk;

namespace ReadConsole
{
    class Program
    {
        static void Main(string[] args)
        {
             try             {                 // Connect to the reader.                 // Replace "SpeedwayR-xx-xx-xx" with your                  // reader's host name or IP address.                 Reader.Connect("SpeedwayR-xx-xx-xx"); 
 
                // Remove all settings from the reader.                 Reader.ClearSettings(); 
 
                // Get the factory default settings                 // We'll use these as a starting point                 // and then modify the settings we're                  // interested in                 Settings settings = Reader.QueryFactorySettings(); 
 
                // Tell the reader to include the antenna number
                  // in all tag reports. Other fields can be added                 // to the reports in the same way by setting the                  // appropriate Report.IncludeXXXXXXX property.                 settings.Report.IncludeAntennaPortNumber = true; 
 
                // Send a tag report for every tag read.                 settings.Report.Mode = ReportMode.Individual; 
 
                // Apply the newly modified settings.                 Reader.ApplySettings(settings); 
 
                // Assign the TagsReported handler.                 // This specifies which function to call                 // when tags reports are available.                 Reader.TagsReported += new EventHandler                     <TagsReportedEventArgs>(OnTagsReported); 
 
                // Start reading.                 Reader.Start(); 
 
                // Wait for the user to press enter.                 Console.WriteLine("Press enter when done.");                 Console.ReadLine(); 
 
                // Stop reading.                 Reader.Stop(); 
 
                // Disconnect from the reader.                 Reader.Disconnect();             }             catch (OctaneSdkException e)             {                 Console.WriteLine("Octane SDK exception: {0}", e.Message);             }             catch (Exception e)             {                 Console.WriteLine("Exception : {0}", e.Message);             }         } 
 
        static void OnTagsReported(object sender, TagsReportedEventArgs args)         {             // This function is called asynchronously              // when tag reports are available.             // Loop through each tag in the report              // and print the data.             foreach (Tag tag in args.TagReport.Tags)             {                 Console.WriteLine("EPC : {0} Antenna : {1}",                                     tag.Epc, tag.AntennaPortNumber);             }   
        }
    }
}
