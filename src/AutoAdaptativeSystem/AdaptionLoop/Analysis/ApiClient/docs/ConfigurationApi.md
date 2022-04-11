# Analysis.Service.ApiClient.Api.ConfigurationApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConfigurationConfigurationNameGet**](ConfigurationApi.md#configurationconfigurationnameget) | **GET** /Configuration/{configurationName} | Gets a configuration property given its name.
[**ConfigurationRequestChangePost**](ConfigurationApi.md#configurationrequestchangepost) | **POST** /Configuration/request-change | Requests a change in a configuration key of a given service. For example,  could be used to set the target temperature of an AC system.


<a name="configurationconfigurationnameget"></a>
# **ConfigurationConfigurationNameGet**
> ConfigurationDTO ConfigurationConfigurationNameGet (string name, string configurationName)

Gets a configuration property given its name.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Client;
using Analysis.Service.ApiClient.Model;

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
            var name = "name_example";  // string | 
            var configurationName = "configurationName_example";  // string | 

            try
            {
                // Gets a configuration property given its name.
                ConfigurationDTO result = apiInstance.ConfigurationConfigurationNameGet(name, configurationName);
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
 **name** | **string**|  | 
 **configurationName** | **string**|  | 

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
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Client;
using Analysis.Service.ApiClient.Model;

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
| **204** | The configuration change request was submitted successfully. |  -  |
| **400** | The request was formed incorrectly (null request DTO, invalid values..). |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

