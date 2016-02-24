using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace zzServiceList
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();

            // Display the list of services currently running on this computer.

            Console.WriteLine("Services running on the local computer:");
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.Status == ServiceControllerStatus.Running)
                {
                    // Write the service name and the display name
                    // for each running service.
                    Console.WriteLine();
                    Console.WriteLine("  Service :        {0}", scTemp.ServiceName);
                    Console.WriteLine("    Display name:    {0}", scTemp.DisplayName);

                    // Query WMI for additional information about this service.
                    // Display the start name (LocalSytem, etc) and the service
                    // description.
                    ManagementObject wmiService;
                    wmiService = new ManagementObject("Win32_Service.Name='" + scTemp.ServiceName + "'");
                    wmiService.Get();
                    Console.WriteLine("    Path:            {0}", wmiService["PathName"]);
                    Console.WriteLine("    Start name:      {0}", wmiService["StartName"]);
                    Console.WriteLine("    Description:     {0}", wmiService["Description"]);
                }
            }

            Console.ReadKey();
        }
    }
}
