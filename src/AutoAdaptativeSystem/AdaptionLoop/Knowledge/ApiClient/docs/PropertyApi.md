# Knowledge.Service.ApiClient.Api.PropertyApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**PropertyPropertyNameDelete**](PropertyApi.md#propertypropertynamedelete) | **DELETE** /Property/{propertyName} | Deletes the value of a given property.
[**PropertyPropertyNameGet**](PropertyApi.md#propertypropertynameget) | **GET** /Property/{propertyName} | Gets a property given its name.
[**PropertyPropertyNamePut**](PropertyApi.md#propertypropertynameput) | **PUT** /Property/{propertyName} | Sets value of a given property. If the property does not exist, it will be created.


<a name="propertypropertynamedelete"></a>
# **PropertyPropertyNameDelete**
> void PropertyPropertyNameDelete (string propertyName)

Deletes the value of a given property.

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
    public class PropertyPropertyNameDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PropertyApi(httpClient, config, httpClientHandler);
            var propertyName = "propertyName_example";  // string | The name of the property to delete.

            try
            {
                // Deletes the value of a given property.
                apiInstance.PropertyPropertyNameDelete(propertyName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyApi.PropertyPropertyNameDelete: " + e.Message );
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
 **propertyName** | **string**| The name of the property to delete. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | The property was deleted successfully. |  -  |
| **400** | There was an error with the provided arguments. |  -  |
| **404** | The property was not found. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="propertypropertynameget"></a>
# **PropertyPropertyNameGet**
> PropertyDTO PropertyPropertyNameGet (string propertyName)

Gets a property given its name.

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
            var propertyName = "propertyName_example";  // string | The name of the property to find.

            try
            {
                // Gets a property given its name.
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
 **propertyName** | **string**| The name of the property to find. | 

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
| **200** | The property was found. Returns the value of the property. |  -  |
| **404** | The property was not found. |  -  |
| **400** | There was an error with the provided arguments. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="propertypropertynameput"></a>
# **PropertyPropertyNamePut**
> void PropertyPropertyNamePut (string propertyName, SetPropertyDTO? setPropertyDTO = null)

Sets value of a given property. If the property does not exist, it will be created.

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
            var setPropertyDTO = new SetPropertyDTO?(); // SetPropertyDTO? | The DTO containing the value to set. (optional) 

            try
            {
                // Sets value of a given property. If the property does not exist, it will be created.
                apiInstance.PropertyPropertyNamePut(propertyName, setPropertyDTO);
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
| **204** | The property was updated or created successfully. |  -  |
| **400** | There was an error with the provided arguments. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
