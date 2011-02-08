using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.NetMicroFramework.Tools.MFDeployTool.Engine;

namespace MFDeployAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MFDeploy deploy = new MFDeploy())
            {
                // Obtain devices connected to USB port
                IList<MFPortDefinition> portDefs = deploy.EnumPorts(TransportType.USB);

                // List devices
                Debug.WriteLine("USB Devices:");
                foreach (MFPortDefinition portDef in portDefs)
                {
                    Debug.WriteLine(portDef.Name);
                }

                // Return if no device was found
                if (portDefs.Count == 0)
                {
                    Debug.WriteLine("No device.");
                    return;
                }

                // Connect to first device that was found
                using (MFDevice device = deploy.Connect(portDefs[0]))
                {
                    uint entryPoint = 0;
                    if (device.Deploy("c:\\myapp.hex", // deployment image 
                                      "c:\\myapp.sig", // signature file (optional)
                                      ref entryPoint   // return apps entry point
                                      ))
                    {
                        Debug.WriteLine("Deploying succeded.");

                        if (entryPoint != 0) // check if image has an entry point
                        {
                            Debug.WriteLine("Executing application.");
                            device.Execute(entryPoint);
                        }
                    }
                    else
                        Debug.WriteLine("Deploying failed.");
                }
            }
        }
    }
}