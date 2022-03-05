# Monitoring.Service.ApiClient.Api.MonitorApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**MonitorMonitorIdMeasurementPost**](MonitorApi.md#monitormonitoridmeasurementpost) | **POST** /Monitor/{monitorId}/measurement | Registers a measurement from a monitor.


<a name="monitormonitoridmeasurementpost"></a>
# **MonitorMonitorIdMeasurementPost**
> void MonitorMonitorIdMeasurementPost (Guid monitorId, MeasurementDTO? measurementDTO = null)

Registers a measurement from a monitor.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Monitoring.Service.ApiClient.Api;
using Monitoring.Service.ApiClient.Client;
using Monitoring.Service.ApiClient.Model;

namespace Example
{
    public class MonitorMonitorIdMeasurementPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new MonitorApi(httpClient, config, httpClientHandler);
            var monitorId = "monitorId_example";  // Guid | The identifier of the monitor reporting the measurement.
            var measurementDTO = new MeasurementDTO?(); // MeasurementDTO? | The DTO containing information of the measurement. (optional) 

            try
            {
                // Registers a measurement from a monitor.
                apiInstance.MonitorMonitorIdMeasurementPost(monitorId, measurementDTO);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling MonitorApi.MonitorMonitorIdMeasurementPost: " + e.Message );
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
 **monitorId** | **Guid**| The identifier of the monitor reporting the measurement. | 
 **measurementDTO** | [**MeasurementDTO?**](MeasurementDTO?.md)| The DTO containing information of the measurement. | [optional] 

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
| **200** | Indicates that the measurement was reported successfully. |  -  |
| **400** | Indicates that there was an error with the request. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

