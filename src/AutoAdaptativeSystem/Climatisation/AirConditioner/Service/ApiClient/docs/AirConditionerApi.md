# Climatisation.AirConditioner.Service.ApiClient.Api.AirConditionerApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AirConditionerCoolPost**](AirConditionerApi.md#airconditionercoolpost) | **POST** /AirConditioner/Cool | 
[**AirConditionerHeatPost**](AirConditionerApi.md#airconditionerheatpost) | **POST** /AirConditioner/Heat | 
[**AirConditionerTurnOffPost**](AirConditionerApi.md#airconditionerturnoffpost) | **POST** /AirConditioner/TurnOff | 


<a name="airconditionercoolpost"></a>
# **AirConditionerCoolPost**
> void AirConditionerCoolPost ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Climatisation.AirConditioner.Service.ApiClient.Api;
using Climatisation.AirConditioner.Service.ApiClient.Client;
using Climatisation.AirConditioner.Service.ApiClient.Model;

namespace Example
{
    public class AirConditionerCoolPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AirConditionerApi(httpClient, config, httpClientHandler);

            try
            {
                apiInstance.AirConditionerCoolPost();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AirConditionerApi.AirConditionerCoolPost: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="airconditionerheatpost"></a>
# **AirConditionerHeatPost**
> void AirConditionerHeatPost ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Climatisation.AirConditioner.Service.ApiClient.Api;
using Climatisation.AirConditioner.Service.ApiClient.Client;
using Climatisation.AirConditioner.Service.ApiClient.Model;

namespace Example
{
    public class AirConditionerHeatPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AirConditionerApi(httpClient, config, httpClientHandler);

            try
            {
                apiInstance.AirConditionerHeatPost();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AirConditionerApi.AirConditionerHeatPost: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="airconditionerturnoffpost"></a>
# **AirConditionerTurnOffPost**
> void AirConditionerTurnOffPost ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Climatisation.AirConditioner.Service.ApiClient.Api;
using Climatisation.AirConditioner.Service.ApiClient.Client;
using Climatisation.AirConditioner.Service.ApiClient.Model;

namespace Example
{
    public class AirConditionerTurnOffPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new AirConditionerApi(httpClient, config, httpClientHandler);

            try
            {
                apiInstance.AirConditionerTurnOffPost();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AirConditionerApi.AirConditionerTurnOffPost: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

