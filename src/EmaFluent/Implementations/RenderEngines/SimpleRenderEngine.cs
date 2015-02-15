using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyFlow.EmaFluent.Implementations.RenderEngines {
	
	/// <summary>
	/// Simple render engine.
	/// </summary>
	public class SimpleRenderEngine : IRenderEngine {

		/// <summary>
		/// Get body.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <returns></returns>
		public string GetBody<T> ( T model ) {
			throw new NotImplementedException ();
		}

	}

}
