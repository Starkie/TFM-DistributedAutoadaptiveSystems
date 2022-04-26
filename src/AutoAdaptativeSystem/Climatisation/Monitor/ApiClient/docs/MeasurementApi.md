# Climatisation.Monitor.Service.ApiClient.Api.MeasurementApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**MeasurementHumidityPost**](MeasurementApi.md#measurementhumiditypost) | **POST** /Measurement/Humidity | 
[**MeasurementTemperaturePost**](MeasurementApi.md#measurementtemperaturepost) | **POST** /Measurement/Temperature | 


<a name="measurementhumiditypost"></a>
# **MeasurementHumidityPost**
> void MeasurementHumidityPost (HumidityMeasurementDTO? humidityMeasurementDTO = null)



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
    public class MeasurementHumidityPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MeasurementApi(httpClient, config, httpClientHandler);
            var humidityMeasurementDTO = new HumidityMeasurementDTO?(); // HumidityMeasurementDTO? |  (optional) 

            try
            {
                apiInstance.MeasurementHumidityPost(humidityMeasurementDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MeasurementApi.MeasurementHumidityPost: " + e.Message );
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
 **humidityMeasurementDTO** | [**HumidityMeasurementDTO?**](HumidityMeasurementDTO?.md)|  | [optional] 

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
| **200** | Success |  -  |
| **400** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="measurementtemperaturepost"></a>
# **MeasurementTemperaturePost**
> void MeasurementTemperaturePost (TemperatureMeasurementDTO? temperatureMeasurementDTO = null)



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
    public class MeasurementTemperaturePostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MeasurementApi(httpClient, config, httpClientHandler);
            var temperatureMeasurementDTO = new TemperatureMeasurementDTO?(); // TemperatureMeasurementDTO? |  (optional) 

            try
            {
                apiInstance.MeasurementTemperaturePost(temperatureMeasurementDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MeasurementApi.MeasurementTemperaturePost: " + e.Message );
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
 **temperatureMeasurementDTO** | [**TemperatureMeasurementDTO?**](TemperatureMeasurementDTO?.md)|  | [optional] 

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
| **200** | Success |  -  |
| **400** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

