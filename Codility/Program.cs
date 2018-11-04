using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution;

namespace Codility
{
	public interface ISolutionMetadata
	{
		string Name { get; }
	}

	class Solutions
	{
		[ImportMany(typeof(ISolution))]
		public List<Lazy<ISolution, ISolutionMetadata>> _solutions = null;
	}

	class Program
	{
		static void Main(string[] args)
		{
			var catalog = new AggregateCatalog();
			catalog.Catalogs.Add(new ApplicationCatalog());
			var container = new CompositionContainer(catalog);
			var solutions = new Solutions();
			container.SatisfyImportsOnce(solutions);

			var tRes = true;

			foreach (var solution in solutions._solutions)
			{
				Console.WriteLine($"Runnung {solution.Metadata.Name}...");
				var res = solution.Value.Test();
				Console.WriteLine($"{(res ? "Success" : "Failed")}\n");
				tRes = tRes && res;
			}

			Console.WriteLine($"Is Succress?\t{tRes}");

			Console.ReadKey();
		}
	}
}
