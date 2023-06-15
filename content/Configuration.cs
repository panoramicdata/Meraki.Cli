using Meraki.Api;

namespace Meraki.Cli
{
	/// <summary>
	/// Application configuration, loaded from an appsettings.json file upon execution
	/// You can modify/extend this class and provide your own settings
	/// </summary>
	internal class Configuration
	{
		/// <summary>
		/// Meraki credentials
		/// </summary>
		public MerakiClientOptions MerakiClientOptions { get; set; } = new();

		/// <summary>
		/// DELETE THIS!
		/// Provided as a first example
		/// </summary>
		public string Setting1 { get; set; } = string.Empty;
	}
}
