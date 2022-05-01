# Climatisation.Monitor.Service.ApiClient.Api.ServiceApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ServiceServiceNameConfigurationConfigurationNameGet**](ServiceApi.md#serviceservicenameconfigurationconfigurationnameget) | **GET** /Service/{serviceName}/configuration/{configurationName} | Gets a configuration property given its name.
[**ServiceServiceNameConfigurationConfigurationNamePut**](ServiceApi.md#serviceservicenameconfigurationconfigurationnameput) | **PUT** /Service/{serviceName}/configuration/{configurationName} | Sets value of a given configuration property. If the property does not exist, it will be created.


<a name="serviceservicenameconfigurationconfigurationnameget"></a>
# **ServiceServiceNameConfigurationConfigurationNameGet**
> ConfigurationDTO ServiceServiceNameConfigurationConfigurationNameGet (string serviceName, string configurationName)

Gets a configuration property given its name.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Client;
using Climatisation.Monitor.Service.ApiClient.Model;

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

<a name="serviceservicenameconfigurationconfigurationnameput"></a>
# **ServiceServiceNameConfigurationConfigurationNamePut**
> void ServiceServiceNameConfigurationConfigurationNamePut (string serviceName, string configurationName, SetPropertyDTO? setPropertyDTO = null)

Sets value of a given configuration property. If the property does not exist, it will be created.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Client;
using Climatisation.Monitor.Service.ApiClient.Model;

namespace Example
{
    public class ServiceServiceNameConfigurationConfigurationNamePutExample
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
            var configurationName = "configurationName_example";  // string | The name of the property to set.
            var setPropertyDTO = new SetPropertyDTO?(); // SetPropertyDTO? | The DTO containing the value to set. (optional) 

            try
            {
                // Sets value of a given configuration property. If the property does not exist, it will be created.
                apiInstance.ServiceServiceNameConfigurationConfigurationNamePut(serviceName, configurationName, setPropertyDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServiceApi.ServiceServiceNameConfigurationConfigurationNamePut: " + e.Message );
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
 **configurationName** | **string**| The name of the property to set. | 
 **setPropertyDTO** | [**SetPropertyDTO?**](SetPropertyDTO?.md)| The DTO containing the value to set. | [optional] 

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
| **201** | The property was updated or created successfully. |  -  |
| **400** | There was an error with the provided arguments. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

