using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfClient34.ServiceReference1;

namespace WcfClient34
{
	class CallbackHandler : IEmployeeServiceCallback
	{
		public void ZwrotSumaPensji(double result)
		{
			Console.WriteLine("Suma pensji wynosi: {0}", result);
		}
	}
}
