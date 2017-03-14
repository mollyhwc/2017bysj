using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Impinj.OctaneSdk;
namespace TxPowerRamp
{
    class Program
    {
        // Create an instance of the SpeedwayReader class.        
        static SpeedwayReader Reader = new SpeedwayReader();
        static double minTx, maxTx;
        static void Main(string[] args)
        {
            try
            {
                // Connect to the reader.           

                // Replace "SpeedwayR-xx-xx-xx" with your     
                // reader's host name or IP address.              
                Reader.Connect("SpeedwayR-10-74-87.local");
                // Query the reader features to get the min and max Tx power.  
                FeatureSet features = Reader.QueryFeatureSet();
                minTx = features.TxPowers.Entries.First().Dbm;
                maxTx = features.TxPowers.Entries.Last().Dbm;

                // Remove all settings from the reader.      
                Reader.ClearSettings();

                // Get the factory default settings              
                // We'll use these as a starting point            
                // and then modify the settings we're              
                // interested in             
                Settings settings = Reader.QueryFactorySettings();

                // Tell the reader to include the Peak RSSI     
                // in all tag reports. Other fields can be added      
                // to the reports in the same way by setting the      
                // appropriate Report.IncludeXXXXXXX property.          
                settings.Report.IncludePeakRssi = true;

                // Send a tag report for every tag read.          
                settings.Report.Mode = ReportMode.Individual;



                // Loop, increasing the transmit power in 1 dBm steps.       
                for (double power = minTx; power <= maxTx; power += 1.0)
                {                     // Set the transmit power (in dBm).        
                    settings.Antennas[1].TxPowerInDbm = 30;
                
                    // Apply the new transmit power settings.  
                    Reader.ApplySettings(settings);
                    // Read tags for two seconds.                
                    TagReport report = Reader.QueryTags(2);

                    // Loop through all the tags in the report    
                    // and print out the EPC, Tx Power and Peak RSSI.      
                    foreach (Tag tag in report.Tags)
                    {
                        Console.WriteLine("EPC : {0}, Tx Power : {1} dBm {2}",
                            tag.Epc,power, String.Format("{0:0.00}",
                            tag.PeakRssiInDbm));
                        
                    }
                }
                // Disconnect from the reader.          
                Reader.Disconnect();
            }
            catch (OctaneSdkException e)
            {
                Console.WriteLine("Octane SDK exception: {0}", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0}", e.Message);
            }

            // Wait for the user to press enter.         
            Console.WriteLine("Press enter when done.");
            Console.ReadLine();
        }
    }
}
