# Analysis.Service.ApiClient.Api.SystemApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**SystemRequestChangePost**](SystemApi.md#systemrequestchangepost) | **POST** /System/request-change | Requests a change in a configuration key of a given service. For example,  could be used to set the target temperature of an AC system.


<a name="systemrequestchangepost"></a>
# **SystemRequestChangePost**
> void SystemRequestChangePost (SystemConfigurationChangeRequestDTO? systemConfigurationChangeRequestDTO = null)

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
    public class SystemRequestChangePostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new SystemApi(httpClient, config, httpClientHandler);
            var systemConfigurationChangeRequestDTO = new SystemConfigurationChangeRequestDTO?(); // SystemConfigurationChangeRequestDTO? | The DTO containing the request to change the property. (optional) 

            try
            {
                // Requests a change in a configuration key of a given service. For example,  could be used to set the target temperature of an AC system.
                apiInstance.SystemRequestChangePost(systemConfigurationChangeRequestDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SystemApi.SystemRequestChangePost: " + e.Message );
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
 **systemConfigurationChangeRequestDTO** | [**SystemConfigurationChangeRequestDTO?**](SystemConfigurationChangeRequestDTO?.md)| The DTO containing the request to change the property. | [optional] 

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

