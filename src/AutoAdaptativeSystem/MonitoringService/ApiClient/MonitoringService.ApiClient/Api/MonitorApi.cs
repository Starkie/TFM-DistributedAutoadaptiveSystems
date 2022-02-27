/*
 * Monitoring Service
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using MonitoringService.ApiClient.Client;
using MonitoringService.ApiClient.Model;

namespace MonitoringService.ApiClient.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IMonitorApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Registers a measurement from a monitor.
        /// </summary>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <returns></returns>
        void MonitorMonitorIdMeasurementPost(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?));

        /// <summary>
        /// Registers a measurement from a monitor.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> MonitorMonitorIdMeasurementPostWithHttpInfo(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IMonitorApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Registers a measurement from a monitor.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task MonitorMonitorIdMeasurementPostAsync(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Registers a measurement from a monitor.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> MonitorMonitorIdMeasurementPostWithHttpInfoAsync(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IMonitorApi : IMonitorApiSync, IMonitorApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class MonitorApi : IDisposable, IMonitorApi
    {
        private MonitoringService.ApiClient.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <returns></returns>
        public MonitorApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="basePath">The target service's base path in URL format.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public MonitorApi(string basePath)
        {
            this.Configuration = MonitoringService.ApiClient.Client.Configuration.MergeConfigurations(
                MonitoringService.ApiClient.Client.GlobalConfiguration.Instance,
                new MonitoringService.ApiClient.Client.Configuration { BasePath = basePath }
            );
            this.ApiClient = new MonitoringService.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            this.Client =  this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            this.ExceptionFactory = MonitoringService.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorApi"/> class using Configuration object.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="configuration">An instance of Configuration.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public MonitorApi(MonitoringService.ApiClient.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = MonitoringService.ApiClient.Client.Configuration.MergeConfigurations(
                MonitoringService.ApiClient.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.ApiClient = new MonitoringService.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            this.Client = this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            ExceptionFactory = MonitoringService.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorApi"/> class.
        /// </summary>
        /// <param name="client">An instance of HttpClient.</param>
        /// <param name="handler">An optional instance of HttpClientHandler that is used by HttpClient.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        /// <remarks>
        /// Some configuration settings will not be applied without passing an HttpClientHandler.
        /// The features affected are: Setting and Retrieving Cookies, Client Certificates, Proxy settings.
        /// </remarks>
        public MonitorApi(HttpClient client, HttpClientHandler handler = null) : this(client, (string)null, handler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorApi"/> class.
        /// </summary>
        /// <param name="client">An instance of HttpClient.</param>
        /// <param name="basePath">The target service's base path in URL format.</param>
        /// <param name="handler">An optional instance of HttpClientHandler that is used by HttpClient.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        /// <remarks>
        /// Some configuration settings will not be applied without passing an HttpClientHandler.
        /// The features affected are: Setting and Retrieving Cookies, Client Certificates, Proxy settings.
        /// </remarks>
        public MonitorApi(HttpClient client, string basePath, HttpClientHandler handler = null)
        {
            if (client == null) throw new ArgumentNullException("client");

            this.Configuration = MonitoringService.ApiClient.Client.Configuration.MergeConfigurations(
                MonitoringService.ApiClient.Client.GlobalConfiguration.Instance,
                new MonitoringService.ApiClient.Client.Configuration { BasePath = basePath }
            );
            this.ApiClient = new MonitoringService.ApiClient.Client.ApiClient(client, this.Configuration.BasePath, handler);
            this.Client =  this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            this.ExceptionFactory = MonitoringService.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorApi"/> class using Configuration object.
        /// </summary>
        /// <param name="client">An instance of HttpClient.</param>
        /// <param name="configuration">An instance of Configuration.</param>
        /// <param name="handler">An optional instance of HttpClientHandler that is used by HttpClient.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        /// <remarks>
        /// Some configuration settings will not be applied without passing an HttpClientHandler.
        /// The features affected are: Setting and Retrieving Cookies, Client Certificates, Proxy settings.
        /// </remarks>
        public MonitorApi(HttpClient client, MonitoringService.ApiClient.Client.Configuration configuration, HttpClientHandler handler = null)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (client == null) throw new ArgumentNullException("client");

            this.Configuration = MonitoringService.ApiClient.Client.Configuration.MergeConfigurations(
                MonitoringService.ApiClient.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.ApiClient = new MonitoringService.ApiClient.Client.ApiClient(client, this.Configuration.BasePath, handler);
            this.Client = this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            ExceptionFactory = MonitoringService.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MonitorApi(MonitoringService.ApiClient.Client.ISynchronousClient client, MonitoringService.ApiClient.Client.IAsynchronousClient asyncClient, MonitoringService.ApiClient.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = MonitoringService.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Disposes resources if they were created by us
        /// </summary>
        public void Dispose()
        {
            this.ApiClient?.Dispose();
        }

        /// <summary>
        /// Holds the ApiClient if created
        /// </summary>
        public MonitoringService.ApiClient.Client.ApiClient ApiClient { get; set; } = null;

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public MonitoringService.ApiClient.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public MonitoringService.ApiClient.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public MonitoringService.ApiClient.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public MonitoringService.ApiClient.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Registers a measurement from a monitor. 
        /// </summary>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <returns></returns>
        public void MonitorMonitorIdMeasurementPost(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?))
        {
            MonitorMonitorIdMeasurementPostWithHttpInfo(monitorId, measurementDTO);
        }

        /// <summary>
        /// Registers a measurement from a monitor. 
        /// </summary>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public MonitoringService.ApiClient.Client.ApiResponse<Object> MonitorMonitorIdMeasurementPostWithHttpInfo(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?))
        {
            MonitoringService.ApiClient.Client.RequestOptions localVarRequestOptions = new MonitoringService.ApiClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json",
                "text/json",
                "application/_*+json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
            };

            var localVarContentType = MonitoringService.ApiClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = MonitoringService.ApiClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("monitorId", MonitoringService.ApiClient.Client.ClientUtils.ParameterToString(monitorId)); // path parameter
            localVarRequestOptions.Data = measurementDTO;


            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/Monitor/{monitorId}/measurement", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MonitorMonitorIdMeasurementPost", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Registers a measurement from a monitor. 
        /// </summary>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task MonitorMonitorIdMeasurementPostAsync(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await MonitorMonitorIdMeasurementPostWithHttpInfoAsync(monitorId, measurementDTO, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Registers a measurement from a monitor. 
        /// </summary>
        /// <exception cref="MonitoringService.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="monitorId">The identifier of the monitor reporting the measurement.</param>
        /// <param name="measurementDTO">The DTO containing information of the measurement. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<MonitoringService.ApiClient.Client.ApiResponse<Object>> MonitorMonitorIdMeasurementPostWithHttpInfoAsync(Guid monitorId, MeasurementDTO? measurementDTO = default(MeasurementDTO?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            MonitoringService.ApiClient.Client.RequestOptions localVarRequestOptions = new MonitoringService.ApiClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json", 
                "text/json", 
                "application/_*+json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
            };


            var localVarContentType = MonitoringService.ApiClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = MonitoringService.ApiClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("monitorId", MonitoringService.ApiClient.Client.ClientUtils.ParameterToString(monitorId)); // path parameter
            localVarRequestOptions.Data = measurementDTO;


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/Monitor/{monitorId}/measurement", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MonitorMonitorIdMeasurementPost", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
