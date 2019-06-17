using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfClient34.ServiceReference1;

namespace WcfClient34
{
	class Program
	{
		static void Main(string[] args)
		{
			CallbackHandler callbackHandler = new CallbackHandler();
			InstanceContext instanceContext = new InstanceContext(callbackHandler);
			EmployeeServiceClient client = new EmployeeServiceClient(instanceContext);
			Console.WriteLine("Klient wystartowal");
			Employee witold = new Employee
			{
				id = 1,
				firstname = "Witold",
				surname = "Mormul",
				age = 30,
				salary = 3000
			};
			Employee marlena = new Employee
			{
				id = 2,
				firstname = "Marlena",
				surname = "Ziarkiewicz",
				age = 26,
				salary = 5000
			};
			client.AddEmployee(witold);
			client.AddEmployee(marlena);
			Console.WriteLine("Dostepne opcje:");
			Console.WriteLine("\t1. Dodanie pracownika");
			Console.WriteLine("\t2. Wyswietlenie pracownika");
			Console.WriteLine("\t3. Usuniecie pracownika");
			Console.WriteLine("\t4. Wyszukanie pracownika po nazwisku");
			Console.WriteLine("\t5. Wyswietlenie wszystkich pracownikow");
			Console.WriteLine("\t6. Wyswietlenie sumy pensji pracownikow");
			Console.WriteLine("\t7. Zakonczenie dzialania");

			while (true)
			{
				string decision = "";
				decision = Console.ReadLine();
				int caseSwitch = Int32.Parse(decision);

				switch (caseSwitch)
				{
					case 1:
						Console.WriteLine("Podaj imie pracownika");
						string imie = Console.ReadLine();
						Console.WriteLine("Podaj nazwisko pracownika");
						string nazwisko = Console.ReadLine();
						Console.WriteLine("Podaj id pracownika");
						string numer = Console.ReadLine();
						Console.WriteLine("Podaj wiek pracownika");
						string wiek = Console.ReadLine();
						Console.WriteLine("Podaj pensje pracownika");
						string pensja = Console.ReadLine();
						Employee newEmployee = new Employee
						{
							id = Int32.Parse(numer),
							firstname = imie,
							surname = nazwisko,
							age = Int32.Parse(wiek),
							salary = Double.Parse(pensja)
						};
						client.AddEmployee(newEmployee);
						break;
					case 2:
						Console.WriteLine("Podaj numer pracownika do wyswietlenia.");
						string numerPr = Console.ReadLine();
						client.GetEmployee(Int32.Parse(numerPr));
						break;
					case 3:
						Console.WriteLine("Podaj numer pracownika do usuniecia.");
						string numerUs = Console.ReadLine();
						client.DeleteEmployee(Int32.Parse(numerUs));
						break;
					case 4:
						Console.WriteLine("Podaj nazwisko pracownika do wyszukania");
						string nazwiskoSz = Console.ReadLine();
						client.FindEmployeeBySurname(nazwiskoSz);
						break;
					case 5:
						client.GetAllEmployees();
						break;
					case 6:
						client.SumaPensji();
						break;
					case 7:
						client.Close();
						Console.WriteLine("Klient zakonczyl dzialanie");
						return;
					default:
						Console.WriteLine("Niepoprawny numer opcji.");
						break;
				}
				Console.WriteLine("Podaj jaka jest kolejna funkcja");
			}
			client.Close();
			Console.WriteLine("Klient zakonczyl dzialanie");
		}
	}
}
