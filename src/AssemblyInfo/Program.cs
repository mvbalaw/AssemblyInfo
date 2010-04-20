using System;
using System.Reflection;

namespace AssemblyInfo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Usage: AssemblyInfo <assembly>\nexample:\n\n\tAssemblyInfo foo.exe\n");
				return;
			}

			var assembly = Assembly.LoadFrom(args[0]);
			var name = assembly.GetName();
			string description = GetAttribute<AssemblyDescriptionAttribute>(assembly).Description;
			var publicKey = name.GetPublicKey();


			if (!String.IsNullOrEmpty(description))
			{
				Write("Description", description);
			}
			Write("Name", name.Name);
			Write("Version", name.Version);
			bool signed = publicKey != null && publicKey.Length > 0;
			Write("Signed", signed);
			Write("Culture", name.CultureInfo);
			Write("Architecture", name.ProcessorArchitecture == ProcessorArchitecture.MSIL
			                      	? "AnyCPU"
			                      	: name.ProcessorArchitecture.ToString());
		}

		private static void Write<T>(string key, T value)
		{
			Console.WriteLine(key.PadRight(13) + " : " + value);
		}

		private static T GetAttribute<T>(Assembly assembly)
		{
			return (T) assembly.GetCustomAttributes(typeof (T), false)[0];
		}
	}
}