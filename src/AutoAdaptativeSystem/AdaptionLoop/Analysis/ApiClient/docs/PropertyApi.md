# Analysis.Service.ApiClient.Api.PropertyApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**PropertyPropertyNameGet**](PropertyApi.md#propertypropertynameget) | **GET** /Property/{propertyName} | Looks for the Knowledge property with the given name.
[**PropertyPropertyNamePut**](PropertyApi.md#propertypropertynameput) | **PUT** /Property/{propertyName} | Sets value of a given property. If the property does not exist, it will be created.


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

<a name="propertypropertynameput"></a>
# **PropertyPropertyNamePut**
> void PropertyPropertyNamePut (string propertyName, PropertyDTO? propertyDTO = null)

Sets value of a given property. If the property does not exist, it will be created.

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
    public class PropertyPropertyNamePutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PropertyApi(httpClient, config, httpClientHandler);
            var propertyName = "propertyName_example";  // string | The name of the property to set.
            var propertyDTO = new PropertyDTO?(); // PropertyDTO? | The DTO containing the value to set. (optional) 

            try
            {
                // Sets value of a given property. If the property does not exist, it will be created.
                apiInstance.PropertyPropertyNamePut(propertyName, propertyDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyApi.PropertyPropertyNamePut: " + e.Message );
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
 **propertyName** | **string**| The name of the property to set. | 
 **propertyDTO** | [**PropertyDTO?**](PropertyDTO?.md)| The DTO containing the value to set. | [optional] 

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
| **201** | Created |  -  |
| **400** | There was an error with the provided arguments. |  -  |
| **204** | The property was updated or created successfully. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

