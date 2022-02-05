/*
 * Knowledge Service
 *
 * Demonstrates all the existing operations to access and manage Knowledge properties.
 *
 * The version of the OpenAPI document: v1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xunit;

using KnowledgeService.ApiClient.Client;
using KnowledgeService.ApiClient.Api;
// uncomment below to import models
//using KnowledgeService.ApiClient.Model;

namespace KnowledgeService.ApiClient.Test.Api
{
    /// <summary>
    ///  Class for testing PropertyApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class PropertyApiTests : IDisposable
    {
        private PropertyApi instance;

        public PropertyApiTests()
        {
            instance = new PropertyApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of PropertyApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' PropertyApi
            //Assert.IsType<PropertyApi>(instance);
        }

        /// <summary>
        /// Test PropertyPropertyNameDelete
        /// </summary>
        [Fact]
        public void PropertyPropertyNameDeleteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string propertyName = null;
            //instance.PropertyPropertyNameDelete(propertyName);
        }

        /// <summary>
        /// Test PropertyPropertyNameGet
        /// </summary>
        [Fact]
        public void PropertyPropertyNameGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string propertyName = null;
            //var response = instance.PropertyPropertyNameGet(propertyName);
            //Assert.IsType<PropertyDTO>(response);
        }

        /// <summary>
        /// Test PropertyPropertyNamePut
        /// </summary>
        [Fact]
        public void PropertyPropertyNamePutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string propertyName = null;
            //SetPropertyDTO setPropertyDTO = null;
            //instance.PropertyPropertyNamePut(propertyName, setPropertyDTO);
        }
    }
}
