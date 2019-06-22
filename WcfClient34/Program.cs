using System;
using System.Collections.Generic;
using System.IO;
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
			string filePath = Path.Combine(Environment.CurrentDirectory, "klient.jpg");
			CallbackHandler callbackHandler = new CallbackHandler();
			InstanceContext instanceContext = new InstanceContext(callbackHandler);
			EmployeeServiceClient client = new EmployeeServiceClient(instanceContext);
			CallbackHandler callbackHandler2 = new CallbackHandler();
			InstanceContext instanceContext2 = new InstanceContext(callbackHandler2);
			EmployeeServiceClient client2 = new EmployeeServiceClient(instanceContext);
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
			Console.WriteLine("\t6. Wyswietlenie sumy pensji pracownikow (Callback)");
			Console.WriteLine("\t7. Pobranie obrazka (nie dziala)");
			Console.WriteLine("\t8. Zakonczenie dzialania");

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
						Console.WriteLine("Dodano pracownika");
						Console.WriteLine(client.ToString(newEmployee));
						break;
					case 2:
						Console.WriteLine("Podaj numer pracownika do wyswietlenia.");
						string numerPr = Console.ReadLine();
						Employee showEmployee = client.GetEmployee(Int32.Parse(numerPr));
						Console.WriteLine(client.ToString(showEmployee));
						break;
					case 3:
						Console.WriteLine("Podaj numer pracownika do usuniecia.");
						string numerUs = Console.ReadLine();
						Employee deleteEmployee = client.GetEmployee(Int32.Parse(numerUs));
						Console.WriteLine("\n" + client.ToString(deleteEmployee));
						client.DeleteEmployee(Int32.Parse(numerUs));
						Console.WriteLine("Usunieto pracownika");
						break;
					case 4:
						Console.WriteLine("Podaj nazwisko pracownika do wyszukania");
						string nazwiskoSz = Console.ReadLine();
						List<Employee> employeefind = new List<Employee>(client.FindEmployeeBySurname(nazwiskoSz));
						foreach (Employee e in employeefind)
						{
							Console.WriteLine(client.ToString(e));
						}
						break;
					case 5:
						List<Employee> allEmployees = new List<Employee>(client.GetAllEmployees());
						foreach (Employee e in allEmployees)
						{
							Console.WriteLine(client.ToString(e));
						}
						break;
					case 6:
						client2.SumaPensjiAsync();
						break;
					case 7:
						Stream stream = client.GetImage("image.jpg");
						ZapiszPlik(stream, filePath);
						break;
					case 8:
						client.Close();
						Console.WriteLine("Klient zakonczyl dzialanie");
						return;
					default:
						Console.WriteLine("Niepoprawny numer opcji.");
						break;
				}
				Console.WriteLine("Podaj jaka jest kolejna funkcja");
			}
		}

		private static void ZapiszPlik(Stream instream, string filePath)
		{
			const int bufferLength = 8192;
			int bytecount = 0;
			int counter = 0;
			byte[] buffer = new byte[bufferLength];
			Console.WriteLine("Zapisuje plik {0}", filePath);
			FileStream outsream = File.Open(filePath, FileMode.Create, FileAccess.Write);
			while ((counter = instream.Read(buffer, 0, bufferLength)) > 0)
			{
				outsream.Write(buffer, 0, counter);
				Console.Write(".{0}", counter);
				bytecount += counter;
			}
			Console.WriteLine("\nZapisano {0} bajtow", bytecount);
			outsream.Close();
			instream.Close();
			Console.WriteLine("\n Plik zapisany", filePath);
		}
	}
}
