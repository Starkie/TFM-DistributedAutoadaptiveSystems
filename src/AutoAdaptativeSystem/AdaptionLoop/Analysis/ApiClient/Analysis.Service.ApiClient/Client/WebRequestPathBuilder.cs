/*
 * Analysis Service
 *
 * Demonstrates all the existing operations to access and manage Adaption Rules.
 *
 * The version of the OpenAPI document: v1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System.Collections.Generic;
using System.Web;

namespace Analysis.Service.ApiClient.Client
{
    /// <summary>
    /// A URI builder
    /// </summary>
    class WebRequestPathBuilder
    {
            private string _baseUrl;
            private string _path;
            private string _query = "?";
            public WebRequestPathBuilder(string baseUrl, string path)
            {
                _baseUrl = baseUrl;
                _path = path;
            }

            public void AddPathParameters(Dictionary<string, string> parameters)
            {
                foreach (var parameter in parameters)
                {
                    _path = _path.Replace("{" + parameter.Key + "}", HttpUtility.UrlEncode(parameter.Value));
                }
            }

            public void AddQueryParameters(Multimap<string, string> parameters)
            {
                foreach (var parameter in parameters)
                {
                    foreach (var value in parameter.Value)
                    {
                        _query = _query + parameter.Key + "=" + HttpUtility.UrlEncode(value) + "&";
                    }
                }
            }

            public string GetFullUri()
            {
                return _baseUrl + _path + _query.Substring(0, _query.Length - 1);
            }
    }
}