# Monitoring.Service.ApiClient.Api.ServiceApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ServiceServiceNameConfigurationConfigurationNameGet**](ServiceApi.md#serviceservicenameconfigurationconfigurationnameget) | **GET** /Service/{serviceName}/configuration/{configurationName} | Gets a configuration property given its name.
[**ServiceServiceNameConfigurationPut**](ServiceApi.md#serviceservicenameconfigurationput) | **PUT** /Service/{serviceName}/configuration | Sets the values for the given configuration properties. If a given property   does not exist, it will be created.


<a name="serviceservicenameconfigurationconfigurationnameget"></a>
# **ServiceServiceNameConfigurationConfigurationNameGet**
> ConfigurationDTO ServiceServiceNameConfigurationConfigurationNameGet (string serviceName, string configurationName)

Gets a configuration property given its name.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Monitoring.Service.ApiClient.Api;
using Monitoring.Service.ApiClient.Client;
using Monitoring.Service.ApiClient.Model;

namespace Example
{
    public class ServiceServiceNameConfigurationConfigurationNameGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ServiceApi(httpClient, config, httpClientHandler);
            var serviceName = "serviceName_example";  // string | The name of the service whose configuration we are looking for.
            var configurationName = "configurationName_example";  // string | The name of the configuration property to find.

            try
            {
                // Gets a configuration property given its name.
                ConfigurationDTO result = apiInstance.ServiceServiceNameConfigurationConfigurationNameGet(serviceName, configurationName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServiceApi.ServiceServiceNameConfigurationConfigurationNameGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **serviceName** | **string**| The name of the service whose configuration we are looking for. | 
 **configurationName** | **string**| The name of the configuration property to find. | 

### Return type

[**ConfigurationDTO**](ConfigurationDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The configuration property was found. Returns the value of the property. |  -  |
| **400** | There was an error with the provided arguments. |  -  |
| **404** | The configuration property was not found. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="serviceservicenameconfigurationput"></a>
# **ServiceServiceNameConfigurationPut**
> void ServiceServiceNameConfigurationPut (string serviceName, List<SetPropertyDTO>? setPropertyDTO = null)

Sets the values for the given configuration properties. If a given property   does not exist, it will be created.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Monitoring.Service.ApiClient.Api;
using Monitoring.Service.ApiClient.Client;
using Monitoring.Service.ApiClient.Model;

namespace Example
{
    public class ServiceServiceNameConfigurationPutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ServiceApi(httpClient, config, httpClientHandler);
            var serviceName = "serviceName_example";  // string | The name of the service.
            var setPropertyDTO = new List<SetPropertyDTO>?(); // List<SetPropertyDTO>? | The collection of properties to set. (optional) 

            try
            {
                // Sets the values for the given configuration properties. If a given property   does not exist, it will be created.
                apiInstance.ServiceServiceNameConfigurationPut(serviceName, setPropertyDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServiceApi.ServiceServiceNameConfigurationPut: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **serviceName** | **string**| The name of the service. | 
 **setPropertyDTO** | [**List&lt;SetPropertyDTO&gt;?**](SetPropertyDTO.md)| The collection of properties to set. | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | The properties were created or updated successfully. |  -  |
| **400** | There was an error with the provided arguments. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

