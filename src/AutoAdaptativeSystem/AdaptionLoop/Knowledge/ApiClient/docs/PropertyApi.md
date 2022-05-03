# Knowledge.Service.ApiClient.Api.PropertyApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**PropertyGet**](PropertyApi.md#propertyget) | **GET** /Property | Gets all the property registered in the knowledge.
[**PropertyPropertyNameDelete**](PropertyApi.md#propertypropertynamedelete) | **DELETE** /Property/{propertyName} | Deletes the value of a given property.
[**PropertyPropertyNameGet**](PropertyApi.md#propertypropertynameget) | **GET** /Property/{propertyName} | Gets a property given its name.
[**PropertyPut**](PropertyApi.md#propertyput) | **PUT** /Property | Sets the value of the given properties. If a given property does not exist, it will be created.


<a name="propertyget"></a>
# **PropertyGet**
> Dictionary&lt;string, PropertyDTO&gt; PropertyGet ()

Gets all the property registered in the knowledge.

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
    public class PropertyGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PropertyApi(httpClient, config, httpClientHandler);

            try
            {
                // Gets all the property registered in the knowledge.
                Dictionary<string, PropertyDTO> result = apiInstance.PropertyGet();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyApi.PropertyGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**Dictionary&lt;string, PropertyDTO&gt;**](PropertyDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The collection of registered properties. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

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

<a name="propertyput"></a>
# **PropertyPut**
> void PropertyPut (List<SetPropertyDTO>? setPropertyDTO = null)

Sets the value of the given properties. If a given property does not exist, it will be created.

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
    public class PropertyPutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PropertyApi(httpClient, config, httpClientHandler);
            var setPropertyDTO = new List<SetPropertyDTO>?(); // List<SetPropertyDTO>? | The collection of properties to set.. (optional) 

            try
            {
                // Sets the value of the given properties. If a given property does not exist, it will be created.
                apiInstance.PropertyPut(setPropertyDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyApi.PropertyPut: " + e.Message );
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
 **setPropertyDTO** | [**List&lt;SetPropertyDTO&gt;?**](SetPropertyDTO.md)| The collection of properties to set.. | [optional] 

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

