using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Impinj.OctaneSdk;

namespace ProgramEpc
{
    class Program
    {
        // Create an instance of the SpeedwayReader class.    
        static SpeedwayReader Reader = new SpeedwayReader();

        static void Main(string[] args)
        {
            try
            {
                // Connect to the reader.       
                // Replace "SpeedwayR-xx-xx-xx" with your         
                // reader's host name or IP address.             
                Reader.Connect("speedwayr-10-74-87.local");

                // Configure the reader with the factory deafult settings. 
                Reader.ApplyFactorySettings();

                // Define how we want to perform the write.           
                ProgramEpcParams epcParams = new ProgramEpcParams();
                // Use antenna #1.         
                epcParams.AntennaPortNumber = 1;
                // Write to the first tag we see.        
                // Alternatively, we could choose a specific     
                // tag by EPC or other identifier.              
                epcParams.TargetTag = null;
                // Timeout in 5 seconds if the write operation fails.    
                epcParams.TimeoutInMs = 5000;
                // This is the new EPC we will write to the tag.            
                epcParams.NewEpc = "1111-1111-9000";

                // Perform the write and check the results.    
                ProgramEpcResult result = Reader.ProgramEpc(epcParams);
                if (result.WriteResult.Result == AccessResult.Success)
                {                     // Show how many words were written to the tag.    
                    Console.WriteLine("Tag write successful. {0} words written.",
                        result.WriteResult.NumWordsWritten);
                    // Read back the EPC and print it out.                
                    Console.WriteLine("Querying tag...");
                    TagReport report = Reader.QueryTags(2);
                      foreach (Tag tag in report.Tags)
                    {
                       Console.WriteLine("Tag EPC: {0}", tag.Epc);
                     }
                    //Console.WriteLine("Tag EPC: {0}", report.Tags[0].Epc);
                }
                else
                {
                    Console.WriteLine("Error writing to tag : {0}",
                        result.WriteResult.Result);
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
