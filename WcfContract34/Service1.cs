using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfContract34
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
	public class MyEmployeeService : IEmployeeService
	{
		static List<Employee> employees = new List<Employee>();
		ICallbackHandler callback = null;
		public MyEmployeeService()
		{
			callback = OperationContext.Current.GetCallbackChannel<ICallbackHandler>();
		}
		public void AddEmployee(Employee employee)
		{
			employees.Add(employee);
			Console.WriteLine("Dodano pracownika {0} {1}", employee.firstname, employee.surname);
		}

		public void DeleteEmployee(int id)
		{
			int idx = employees.FindIndex(e => e.id == id);
			if (idx != -1)
			{
				employees.RemoveAt(idx);
				Console.WriteLine("Usunieto pracownika o id {0}", id);
			}
			else
			{
				Console.WriteLine("Nie istnieje pracownik o id {0}", id);
			}
		}

		public List<Employee> FindEmployeeBySurname(string name)
		{
			List<Employee> result = new List<Employee>();
			foreach (Employee  employee in employees)
			{
				if (employee.surname == name)
				{
					result.Add(employee);
					Console.WriteLine(employee.ToString());
				}
			}
			return result;
		}

		public List<Employee> GetAllEmployees()
		{
			foreach (Employee empolyee in employees)
			{
				Console.WriteLine(empolyee.ToString());
			}
			return employees;
		}

		public Employee GetEmployee(int id)
		{
			Employee result = employees.Find(e => e.id == id);
			Console.WriteLine(result.ToString());
			return result;
		}

		public void SumaPensji()
		{
			double result = 0;
			foreach(Employee e in employees)
			{
				result += e.salary;
				Thread.Sleep(1000);
			}
			callback.ZwrotSumaPensji(result);
		}

		public string ToString(Employee employee)
		{
			return employee.firstname + " " + employee.surname + " id = " +
				employee.id.ToString() + " wiek: " + employee.age.ToString() +
				" pensja: " + employee.salary.ToString();
		}
	}
}
