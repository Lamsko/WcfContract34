using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfContract34
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackHandler))]
	public interface IEmployeeService
	{
		[OperationContract]
		void AddEmployee(Employee employee);

		[OperationContract]
		Employee GetEmployee(int id);

		[OperationContract]
		List<Employee> GetAllEmployees();

		[OperationContract]
		void DeleteEmployee(int id);

		[OperationContract]
		List<Employee> FindEmployeeBySurname(string name);

		[OperationContract]
		string ToString(Employee employee);

		[OperationContract(IsOneWay = true)]
		void SumaPensji();

		[OperationContract]
		Stream GetImage(string nazwa);
	}

	internal interface ICallbackHandler
	{
		[OperationContract(IsOneWay = true)]
		void ZwrotSumaPensji(double result);
	}

	// Use a data contract as illustrated in the sample below to add composite types to service operations.
	// You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfContract34.ContractType".
	[DataContract]
	public class Employee
	{
		[DataMember]
		public int id;

		[DataMember]
		public string firstname;

		[DataMember]
		public string surname;

		[DataMember]
		public int age;

		[DataMember]
		public double salary;

		public string ToString()
		{
			return firstname + " " + surname + " id = " +
				id.ToString() + " wiek: " + age.ToString() +
				" pensja: " + salary.ToString();
		}

		public Employee(int i, string fn, string sn, int a, double s)
		{
			id = i;
			firstname = fn;
			surname = sn;
			age = a;
			salary = s;
		}

	}
}
