# Knowledge.Service.ApiClient.Api.ConfigurationApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConfigurationConfigurationNameGet**](ConfigurationApi.md#configurationconfigurationnameget) | **GET** /Configuration/{configurationName} | Gets a configuration property given its name.
[**ConfigurationRequestChangePost**](ConfigurationApi.md#configurationrequestchangepost) | **POST** /Configuration/request-change | Requests a change in a configuration key of a given service. For example,  could be used to set the target temperature of an AC system.


<a name="configurationconfigurationnameget"></a>
# **ConfigurationConfigurationNameGet**
> PropertyDTO ConfigurationConfigurationNameGet (string configurationName)

Gets a configuration property given its name.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Client;
using Knowledge.Service.ApiClient.Model;

namespace Example
{
    public class ConfigurationConfigurationNameGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var configurationName = "configurationName_example";  // string | The name of the configuration property to find.

            try
            {
                // Gets a configuration property given its name.
                PropertyDTO result = apiInstance.ConfigurationConfigurationNameGet(configurationName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationConfigurationNameGet: " + e.Message );
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
 **configurationName** | **string**| The name of the configuration property to find. | 

### Return type

[**PropertyDTO**](PropertyDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The configuration property was found. Returns the value of the property. |  -  |
| **404** | The configuration property was not found. |  -  |
| **400** | There was an error with the provided arguments. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="configurationrequestchangepost"></a>
# **ConfigurationRequestChangePost**
> void ConfigurationRequestChangePost (ConfigurationChangeRequestDTO? configurationChangeRequestDTO = null)

Requests a change in a configuration key of a given service. For example,  could be used to set the target temperature of an AC system.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Client;
using Knowledge.Service.ApiClient.Model;

namespace Example
{
    public class ConfigurationRequestChangePostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new ConfigurationApi(httpClient, config, httpClientHandler);
            var configurationChangeRequestDTO = new ConfigurationChangeRequestDTO?(); // ConfigurationChangeRequestDTO? | The DTO containing the request to change the property. (optional) 

            try
            {
                // Requests a change in a configuration key of a given service. For example,  could be used to set the target temperature of an AC system.
                apiInstance.ConfigurationRequestChangePost(configurationChangeRequestDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConfigurationApi.ConfigurationRequestChangePost: " + e.Message );
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
 **configurationChangeRequestDTO** | [**ConfigurationChangeRequestDTO?**](ConfigurationChangeRequestDTO?.md)| The DTO containing the request to change the property. | [optional] 

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
| **204** | The configuration property was updated or created successfully. |  -  |
| **400** | There was an error with the provided arguments. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

