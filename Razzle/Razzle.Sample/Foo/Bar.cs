using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razzle.Sample.Foo {

	public class Bar {
		/// <summary>
		/// Gets or sets my zippy.
		/// </summary>
		/// <value>
		/// zippy
		/// </value>
		public int BazZippy { get; set; }

		/// <summary>
		/// This will not have documentation because of the "ignore".
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException">Not yet implemented.</exception>
		/// <ignore>true</ignore>
		public Razzle.Sample.Baz.Foo GetBarFoo() {
			throw new NotImplementedException("Not yet implemented.");
		}

		/// <summary>
		/// Gets the baz.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException">Not yet implemented.</exception>
		/// <gist id="3038130495f4ca39683d">Get the foo</gist>
		public Razzle.Sample.Bar.Baz GetBarBaz() {
			throw new NotImplementedException("Not yet implemented.");
		}

		/// <summary>
		/// Gets the bar foo bar.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		/// <see cref="Razzle.Sample.Bar.Baz">This is related</see>
		public Razzle.Sample.Foo.Bar GetBarFooBar() {
			throw new NotImplementedException("");
		}
	}
}
