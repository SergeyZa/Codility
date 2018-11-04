using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Solution;

namespace Cantaloupe.two
{
	public class SolutionIter : IEnumerable<int>
	{
		Stream _stream = null;

		public SolutionIter(Stream stream)
		{
			_stream = stream;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new IntLinesEnumerator(_stream);
		}

		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			return new IntLinesEnumerator(_stream);
		}
	}

	public class IntLinesEnumerator : IEnumerator<int>
	{
		Stream _stream = null;

		public IntLinesEnumerator(Stream stream)
		{
			_stream = stream;
		}

		protected int _current = 0;

		public int Current
		{
			get { return _current; }
		}

		object IEnumerator.Current => this.Current;

		public void Dispose()
		{
			if (null != _reader)
				_reader.Dispose();
		}

		protected StreamReader _reader;
		protected StreamReader Reader
		{
			get
			{
				if (null == _stream || !_stream.CanSeek || !_stream.CanRead)
					throw new InvalidOperationException();

				if (null == _reader)
					_reader = new StreamReader(_stream);
				return _reader;
			}
		}

		public bool MoveNext()
		{
			try
			{
				var read = false;
				do
				{
					var s = Reader.ReadLine();
					if (null == s)
						return false;
					//Console.WriteLine($"read: '{s}'");
					s = s.Trim();
					if ((s.Length > 1 && s.StartsWith("0")) ||
						(s.Length > 2 && (s.StartsWith("-0") || s.StartsWith("+0"))))
						continue;
					read = int.TryParse(s, out int n);
					if (read)
					{
						if (n < -1000000000 || n > 1000000000)
						{
							read = false;
							continue;
						}
						_current = n;
						return true;
					}
				} while (!read);
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	[Export(typeof(ISolution))]
	[ExportMetadata("Name", nameof(Cantaloupe.two))]
	public class Solution : ISolution
	{
		public bool Test()
		{
			var lines = new string[]
			{
				"137",
				"-104",
				"",
				"2 58",
				" +0",
				"++3",
				"+1",
				"00",
				" 23.9",
				"",
				"2000000000",
				"-0",
				"+017",
				"five",
				" -1",
			};
			using (var stream = new MemoryStream())
			{
				var writer = new StreamWriter(stream);
				foreach (var line in lines)
					writer.WriteLine(line);
				writer.Flush();
				stream.Position = 0;
				var iter = new SolutionIter(stream).ToArray();
				foreach (var n in iter)
				{
					Console.WriteLine($"{n}");
				}
			}
			return true;
		}
	}

}
