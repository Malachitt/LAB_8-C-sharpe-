using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
	interface IOperation<T>
	{
		void Add1(T a);
		void Remove(T b);
		void Write();
	}
	class Program
	{
		static void Main(string[] args)
		{
		try
		{
				Tovar tovar = new Tovar(1) { Name = "Bob", Price = 500 };
				Tovar tovar1 = new Tovar(2) { Name = "Tom", Price = 5000 };
				Technology<Tovar> technology = new Technology<Tovar>
				{
					Info1 = tovar,
					Info2 = tovar1,
					TypeOfTechnology = "Technology"
				};
				technology.Display();


				MySet<int> ms1 = new MySet<int>(new List<int>() { 1, 7, 3, 4 });

			Console.WriteLine(ms1);

			MySet<int> ms2 = new MySet<int>(new List<int>() { 2, 7, 3, 4 });

			MySet<string> ms3 = new MySet<string>(new List<string>() { "adwd", "awda" });

			Console.WriteLine(ms2);

			Console.WriteLine(ms1 > ms2);                               //проверка на подмножество

			Console.WriteLine(ms1 % ms2);                               //пересечение

			Console.WriteLine(ms1 ^ ms2);                               //проверка на неравенство

			Console.WriteLine(ms1 - 2);                                 //удаление

			WordPoint.ShortString("Мы учимся программировать" +
   " на многочисленных языках программрования!");                       //Поиск самого короткого слова

			WordPoint.Sets(ms1.Set);                                    //упорядочивание множества

				ms3.Write();
				ms3.Add1("10");
				ms3.Write();
				ms3.Remove("10");
				ms3.Write();
				ms2.Write();
				ms2.Add1(10);
				ms2.Write();
				ms2.Remove(10);
				ms2.Write();

			}
			catch(PersonNullReference ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			catch(Exception ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			finally
			{
				Console.WriteLine("Блок finally");
			}
			Console.WriteLine("Конец работы программы");

			Console.ReadKey();

		}
	}

	class MySet<T> : IOperation<T>
	{
		public List<T> Set { get; set; }

		public MySet()
		{
			Set = new List<T>();
		}

		public MySet(List<T> set)
		{
			Set = set;
		}
		//Пересечение множеств
		public static MySet<T> operator %(MySet<T> mas1, MySet<T> mas2)
		{
			List<T> resultList = mas1.Set.Intersect(mas2.Set).ToList();
			MySet<T> resultSet = new MySet<T>();
			resultSet.Set = resultList;
			return resultSet;
		}
		//Сравнение множеств
		public static bool operator <(MySet<T> mas1, MySet<T> mas2)
		{
			mas1.Set.Sort();
			mas2.Set.Sort();
			bool x = false;
			foreach (T value in mas1.Set)
			{
				foreach (T value1 in mas2.Set)
				{
					if (value.ToString() == value1.ToString())
					{
						x = true;
					}
					else
					{
						x = false;
						break;
					}
					break;
				}
				break;
			}
			return x;
		}
		//Проверка на неравенство
		public static bool operator ^(MySet<T> mas1, MySet<T> mas2)
		{
			mas1.Set.Sort();
			mas2.Set.Sort();
			bool x = false;
			foreach (T value in mas1.Set)
			{
				foreach (T value1 in mas2.Set)
				{
					if (value.ToString() != value1.ToString())
					{
						x = true;
					}
					else
					{
						x = false;
						break;
					}
					break;
				}
				break;
			}
			return x;
		}
		//Удалить элемент из множества
		public static MySet<T> operator -(MySet<T> mas1, T x)
		{
			mas1.Set.Remove(x);
			MySet<T> result = new MySet<T>();
			result.Set = mas1.Set;
			return result;
		}
		//Проверка на подмножество
		public static bool operator >(MySet<T> mas1, MySet<T> mas2)
		{

			// Проверяем входные данные на пустоту.
			if (mas1 == null)
			{
				throw new ArgumentNullException(nameof(mas1));
			}

			if (mas2 == null)
			{
				throw new ArgumentNullException(nameof(mas2));
			}

			var result = mas1.Set.All(s => mas2.Set.Contains(s));
			return result;
		}

		//Переопределяем методы
		public override string ToString()
		{
			string s = "";
			foreach (T val in Set)
			{
				if (val == null)
				{
					continue;
				}
				else
				{
					s += val + "; ";
				}
			}

			return s + " . ";
		}

		public void Add1(T a)
		{
			if (a == null)
			{
				throw new PersonNullReference("Удаляемый элемент имеет значение null");
			}
			else
			{
				Set.Add(a);
			}
		}

		public void Remove(T b)
		{
			if (b == null)
			{
				throw new PersonNullReference("Удаляемый элемент имеет значение null");
			}
			else
			{
				Set.Remove(b);
			}

		}

		public void Write()
		{
			string s = "";
			foreach (T val in Set)
			{
				if (val == null)
				{
					throw new PersonNullReference("В списке имеется значение null");
				}
				else
				{
					s += val + "; ";
				}
			}
			Console.WriteLine(s);
		}
	}
	//Методы расширения
	public static class WordPoint
	{
		//Поиск самого короткого слова
		public static void ShortString(this string s1)
		{
			int minInd = 0;
			string[] dd = s1.Split(' ');
			int min = dd[0].Length;

			for (int i = 0; i < dd.Length; i++)
				if (min > dd[i].Length) { min = dd[i].Length; minInd = i; }

			Console.WriteLine("самое кроткое слово: {0} ", dd[minInd]);
			Console.WriteLine("индекс самого кроткого слова: {0} ", minInd);
			Console.WriteLine(s1);

		}
		//Упорядочивание списка
		public static void Sets(this List<int> list)
		{
			list.Sort();
			foreach (object i in list)
				Console.Write(i + "; ");
		}
	}
	class PersonException : Exception
	{
		public PersonException(string message)
			: base(message)
		{ }
	}
	class PersonNullReference : NullReferenceException
	{
		public PersonNullReference(string message) : base(message)
		{ }
	}
	class Tovar                    
	{
		public string Name { get; set; }
		public int Price { get; set; }
		public int ID { get; set; }
		public Tovar(int id)
		{
			ID = id;
		}
	}
	class Technology<T> where T : Tovar        //наследование + ограничение
	{
		public T Info1 { get; set; }
		public T Info2 { get; set; }
		public string TypeOfTechnology { get; set; }

		public void Display()
		{
			Console.WriteLine($"Имя - {Info1.Name}, цена - {Info1.Price}, тип техники - {TypeOfTechnology} .");
			Console.WriteLine($"Имя - {Info2.Name}, цена - {Info2.Price}, тип техники - {TypeOfTechnology} .");
		}
	}
}