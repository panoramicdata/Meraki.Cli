using Meraki.Api;
using Meraki.Api.Data;
using Microsoft.Extensions.Options;

namespace Meraki.Cli;

/// <summary>
/// The main application
/// </summary>
internal class Application : BackgroundService
{
	/// <summary>
	/// Configuration
	/// </summary>
	private readonly Configuration _config;

	/// <summary>
	/// The client to use for API interaction
	/// </summary>
	private readonly MerakiClient _merakiClient;

	/// <summary>
	/// The logger
	/// </summary>
	private readonly ILogger<Application> _logger;

	/// <summary>
	/// The application lifetime
	/// </summary>
	private readonly IHostApplicationLifetime _lifetime;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="options"></param>
	/// <param name="loggerFactory"></param>
	public Application(
		IOptions<Configuration> options,
		ILoggerFactory loggerFactory,
		IHostApplicationLifetime lifetime)
	{
		// Store the config
		_config = options.Value;

		// Create a logger
		_logger = loggerFactory.CreateLogger<Application>();

		// Create a Meraki client
		_merakiClient = new MerakiClient(
			_config.MerakiClientOptions,
			loggerFactory.CreateLogger<MerakiClient>()
		);

		// Store the lifetime
		_lifetime = lifetime;
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		try
		{
			// Use _logger for logging
			_logger.LogInformation("Application start.  Setting1 is set to {setting1}", _config.Setting1);

			// Use asynchronous calls to _merakiClient to interact with the portal
			var organizations = await _merakiClient
				.Organizations
				.GetOrganizationsAsync(cancellationToken)
				.ConfigureAwait(false);

			_logger.LogInformation("You have access to {OrganizationCount} organization(s).", organizations.Count);

			// Summarize each one:
			foreach (var organization in organizations)
			{
				var networks = await GetNetworksAsync(organization, cancellationToken)
					.ConfigureAwait(false);

				_logger.LogInformation(
					"- {OrganizationName} with {NetworkCount} network(s)",
					organization.Name,
					networks.Count);
			}
		}
		catch (TaskCanceledException)
		{
			_logger.LogWarning("Ctrl+C pressed.  Stopping.");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred: {Message}", ex.Message);
		}
		finally
		{
			_lifetime.StopApplication();
			_logger.LogInformation("Stopped.");
		}
	}

	private async Task<List<Network>> GetNetworksAsync(
		Organization organization,
		CancellationToken cancellationToken)
	{
		// Get the networks:
		try
		{
			return await _merakiClient
			.Organizations
			.Networks
			.GetOrganizationNetworksAsync(organization.Id, cancellationToken: cancellationToken)
			.ConfigureAwait(false);
		}
		catch(Exception ex) when (ex.Message.Contains("404 (Not Found)"))
		{
			return new List<Network>();
		}
	}
}