/*
 * Analysis Service
 *
 * Demonstrates all the existing operations to access and manage Adaption Rules.
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
using Analysis.Service.ApiClient.Client;
using Analysis.Service.ApiClient.Model;

namespace Analysis.Service.ApiClient.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPropertyApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Looks for the Knowledge property with the given name.
        /// </summary>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <returns>PropertyDTO</returns>
        PropertyDTO PropertyPropertyNameGet(string propertyName);

        /// <summary>
        /// Looks for the Knowledge property with the given name.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <returns>ApiResponse of PropertyDTO</returns>
        ApiResponse<PropertyDTO> PropertyPropertyNameGetWithHttpInfo(string propertyName);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPropertyApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Looks for the Knowledge property with the given name.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PropertyDTO</returns>
        System.Threading.Tasks.Task<PropertyDTO> PropertyPropertyNameGetAsync(string propertyName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Looks for the Knowledge property with the given name.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PropertyDTO)</returns>
        System.Threading.Tasks.Task<ApiResponse<PropertyDTO>> PropertyPropertyNameGetWithHttpInfoAsync(string propertyName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPropertyApi : IPropertyApiSync, IPropertyApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class PropertyApi : IDisposable, IPropertyApi
    {
        private Analysis.Service.ApiClient.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <returns></returns>
        public PropertyApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="basePath">The target service's base path in URL format.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public PropertyApi(string basePath)
        {
            this.Configuration = Analysis.Service.ApiClient.Client.Configuration.MergeConfigurations(
                Analysis.Service.ApiClient.Client.GlobalConfiguration.Instance,
                new Analysis.Service.ApiClient.Client.Configuration { BasePath = basePath }
            );
            this.ApiClient = new Analysis.Service.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            this.Client =  this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            this.ExceptionFactory = Analysis.Service.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApi"/> class using Configuration object.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="configuration">An instance of Configuration.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public PropertyApi(Analysis.Service.ApiClient.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Analysis.Service.ApiClient.Client.Configuration.MergeConfigurations(
                Analysis.Service.ApiClient.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.ApiClient = new Analysis.Service.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            this.Client = this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            ExceptionFactory = Analysis.Service.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApi"/> class.
        /// </summary>
        /// <param name="client">An instance of HttpClient.</param>
        /// <param name="handler">An optional instance of HttpClientHandler that is used by HttpClient.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        /// <remarks>
        /// Some configuration settings will not be applied without passing an HttpClientHandler.
        /// The features affected are: Setting and Retrieving Cookies, Client Certificates, Proxy settings.
        /// </remarks>
        public PropertyApi(HttpClient client, HttpClientHandler handler = null) : this(client, (string)null, handler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApi"/> class.
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
        public PropertyApi(HttpClient client, string basePath, HttpClientHandler handler = null)
        {
            if (client == null) throw new ArgumentNullException("client");

            this.Configuration = Analysis.Service.ApiClient.Client.Configuration.MergeConfigurations(
                Analysis.Service.ApiClient.Client.GlobalConfiguration.Instance,
                new Analysis.Service.ApiClient.Client.Configuration { BasePath = basePath }
            );
            this.ApiClient = new Analysis.Service.ApiClient.Client.ApiClient(client, this.Configuration.BasePath, handler);
            this.Client =  this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            this.ExceptionFactory = Analysis.Service.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApi"/> class using Configuration object.
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
        public PropertyApi(HttpClient client, Analysis.Service.ApiClient.Client.Configuration configuration, HttpClientHandler handler = null)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (client == null) throw new ArgumentNullException("client");

            this.Configuration = Analysis.Service.ApiClient.Client.Configuration.MergeConfigurations(
                Analysis.Service.ApiClient.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.ApiClient = new Analysis.Service.ApiClient.Client.ApiClient(client, this.Configuration.BasePath, handler);
            this.Client = this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            ExceptionFactory = Analysis.Service.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PropertyApi(Analysis.Service.ApiClient.Client.ISynchronousClient client, Analysis.Service.ApiClient.Client.IAsynchronousClient asyncClient, Analysis.Service.ApiClient.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Analysis.Service.ApiClient.Client.Configuration.DefaultExceptionFactory;
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
        public Analysis.Service.ApiClient.Client.ApiClient ApiClient { get; set; } = null;

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Analysis.Service.ApiClient.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Analysis.Service.ApiClient.Client.ISynchronousClient Client { get; set; }

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
        public Analysis.Service.ApiClient.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Analysis.Service.ApiClient.Client.ExceptionFactory ExceptionFactory
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
        /// Looks for the Knowledge property with the given name. 
        /// </summary>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <returns>PropertyDTO</returns>
        public PropertyDTO PropertyPropertyNameGet(string propertyName)
        {
            Analysis.Service.ApiClient.Client.ApiResponse<PropertyDTO> localVarResponse = PropertyPropertyNameGetWithHttpInfo(propertyName);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Looks for the Knowledge property with the given name. 
        /// </summary>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <returns>ApiResponse of PropertyDTO</returns>
        public Analysis.Service.ApiClient.Client.ApiResponse<PropertyDTO> PropertyPropertyNameGetWithHttpInfo(string propertyName)
        {
            // verify the required parameter 'propertyName' is set
            if (propertyName == null)
                throw new Analysis.Service.ApiClient.Client.ApiException(400, "Missing required parameter 'propertyName' when calling PropertyApi->PropertyPropertyNameGet");

            Analysis.Service.ApiClient.Client.RequestOptions localVarRequestOptions = new Analysis.Service.ApiClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
            };

            var localVarContentType = Analysis.Service.ApiClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Analysis.Service.ApiClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("propertyName", Analysis.Service.ApiClient.Client.ClientUtils.ParameterToString(propertyName)); // path parameter


            // make the HTTP request
            var localVarResponse = this.Client.Get<PropertyDTO>("/Property/{propertyName}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PropertyPropertyNameGet", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Looks for the Knowledge property with the given name. 
        /// </summary>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PropertyDTO</returns>
        public async System.Threading.Tasks.Task<PropertyDTO> PropertyPropertyNameGetAsync(string propertyName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Analysis.Service.ApiClient.Client.ApiResponse<PropertyDTO> localVarResponse = await PropertyPropertyNameGetWithHttpInfoAsync(propertyName, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Looks for the Knowledge property with the given name. 
        /// </summary>
        /// <exception cref="Analysis.Service.ApiClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="propertyName">The name of the property to look for.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PropertyDTO)</returns>
        public async System.Threading.Tasks.Task<Analysis.Service.ApiClient.Client.ApiResponse<PropertyDTO>> PropertyPropertyNameGetWithHttpInfoAsync(string propertyName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'propertyName' is set
            if (propertyName == null)
                throw new Analysis.Service.ApiClient.Client.ApiException(400, "Missing required parameter 'propertyName' when calling PropertyApi->PropertyPropertyNameGet");


            Analysis.Service.ApiClient.Client.RequestOptions localVarRequestOptions = new Analysis.Service.ApiClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
            };


            var localVarContentType = Analysis.Service.ApiClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Analysis.Service.ApiClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("propertyName", Analysis.Service.ApiClient.Client.ClientUtils.ParameterToString(propertyName)); // path parameter


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<PropertyDTO>("/Property/{propertyName}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PropertyPropertyNameGet", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
