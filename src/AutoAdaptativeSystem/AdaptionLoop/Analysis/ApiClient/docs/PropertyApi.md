# Analysis.Service.ApiClient.Api.PropertyApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**PropertyPropertyNameGet**](PropertyApi.md#propertypropertynameget) | **GET** /Property/{propertyName} | Looks for the Knowledge property with the given name.


<a name="propertypropertynameget"></a>
# **PropertyPropertyNameGet**
> PropertyDTO PropertyPropertyNameGet (string propertyName)

Looks for the Knowledge property with the given name.

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
    public class PropertyPropertyNameGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PropertyApi(httpClient, config, httpClientHandler);
            var propertyName = "propertyName_example";  // string | The name of the property to look for.

            try
            {
                // Looks for the Knowledge property with the given name.
                PropertyDTO result = apiInstance.PropertyPropertyNameGet(propertyName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyApi.PropertyPropertyNameGet: " + e.Message );
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
 **propertyName** | **string**| The name of the property to look for. | 

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
| **200** | The property was found. Returns the property&#39;s value. |  -  |
| **404** | No property with that name was found. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

