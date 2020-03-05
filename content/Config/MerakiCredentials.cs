using System;
using System.Collections.Generic;
using System.Linq;

namespace Meraki.Cli.Config
{
	/// <summary>
	/// Meraki credentials
	/// </summary>
	public class MerakiCredentials
	{
		/// <summary>
		/// The API key.
		/// See https://documentation.meraki.com/zGeneral_Administration/Other_Topics/The_Cisco_Meraki_Dashboard_API
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		/// Ensures that all values are set and are of the expected length
		/// Throws an exception if this is not the case
		/// </summary>
		internal void Validate()
		{
			// Create a list of issues
			var issues = new List<ConfigurationIssue>();

			// AccessKey
			if (string.IsNullOrWhiteSpace(ApiKey))
			{
				issues.Add(new ConfigurationIssue("ApiKey is not set"));
			}

			// Is everything OK?
			if (issues.Count == 0)
			{
				// Yes - return
				return;
			}
			// No

			throw new ConfigurationException(issues);
		}
	}
}