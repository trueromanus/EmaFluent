
namespace EmptyFlow.EmaFluent.Models {
	
	/// <summary>
	/// Email send settings model.
	/// </summary>
	public class EmailSettingsModel {

		/// <summary>
		/// Enable SSL.
		/// </summary>
		public bool EnableSSL {
			get;
			set;
		}

		/// <summary>
		/// User default credentials.
		/// </summary>
		public bool UseDefaultCredentials {
			get;
			set;
		}

		/// <summary>
		/// Timeout.
		/// </summary>
		public int Timeout {
			get;
			set;
		}

		/// <summary>
		/// Port.
		/// </summary>
		public int Port {
			get;
			set;
		}

		/// <summary>
		/// Host.
		/// </summary>
		public string Host {
			get;
			set;
		}

		/// <summary>
		/// User login.
		/// </summary>
		public string UserLogin {
			get;
			set;
		}

		/// <summary>
		/// User password.
		/// </summary>
		public string UserPassword {
			get;
			set;
		}

	}

}
