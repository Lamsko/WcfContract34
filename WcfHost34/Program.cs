using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfContract34;

namespace WcfHost34
{
	class Program
	{
		static void Main(string[] args)
		{
			Uri baseAddress = new Uri("http://localhost:30034/employeeservice");
			ServiceHost host = new ServiceHost(typeof(MyEmployeeService), baseAddress);
			try
			{
				WSDualHttpBinding binding = new WSDualHttpBinding();
				ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(IEmployeeService), binding, baseAddress);
				ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				host.Description.Behaviors.Add(smb);
				host.Open();
				Console.WriteLine("EmployeeService wystartowal.");
				Console.WriteLine("Nacisnij <ENTER> aby zakonczyc.");
				Console.ReadLine();
				host.Close();
				Console.WriteLine("EmployeeService zakonczyl dzialanie.");
			}
			catch (CommunicationException ce)
			{
				Console.WriteLine("Wystapil wyjatek {0}", ce.Message);
				host.Abort();
			}
		}
	}
}
