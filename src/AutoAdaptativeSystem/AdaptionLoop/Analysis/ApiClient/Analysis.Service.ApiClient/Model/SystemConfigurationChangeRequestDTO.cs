/*
 * Analysis Service
 *
 * Demonstrates all the existing operations to access and manage Adaption Rules.
 *
 * The version of the OpenAPI document: v1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using FileParameter = Analysis.Service.ApiClient.Client.FileParameter;
using OpenAPIDateConverter = Analysis.Service.ApiClient.Client.OpenAPIDateConverter;

namespace Analysis.Service.ApiClient.Model
{
    /// <summary>
    /// SystemConfigurationChangeRequestDTO
    /// </summary>
    [DataContract(Name = "SystemConfigurationChangeRequestDTO")]
    public partial class SystemConfigurationChangeRequestDTO : IEquatable<SystemConfigurationChangeRequestDTO>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemConfigurationChangeRequestDTO" /> class.
        /// </summary>
        /// <param name="timestamp">timestamp.</param>
        /// <param name="symptoms">symptoms.</param>
        /// <param name="serviceConfiguration">serviceConfiguration.</param>
        public SystemConfigurationChangeRequestDTO(DateTime timestamp = default(DateTime), List<SymptomDTO> symptoms = default(List<SymptomDTO>), List<ServiceConfigurationDTO> serviceConfiguration = default(List<ServiceConfigurationDTO>))
        {
            this.Timestamp = timestamp;
            this.Symptoms = symptoms;
            this.ServiceConfiguration = serviceConfiguration;
        }

        /// <summary>
        /// Gets or Sets Timestamp
        /// </summary>
        [DataMember(Name = "timestamp", EmitDefaultValue = false)]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or Sets Symptoms
        /// </summary>
        [DataMember(Name = "symptoms", EmitDefaultValue = true)]
        public List<SymptomDTO> Symptoms { get; set; }

        /// <summary>
        /// Gets or Sets ServiceConfiguration
        /// </summary>
        [DataMember(Name = "serviceConfiguration", EmitDefaultValue = true)]
        public List<ServiceConfigurationDTO> ServiceConfiguration { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class SystemConfigurationChangeRequestDTO {\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  Symptoms: ").Append(Symptoms).Append("\n");
            sb.Append("  ServiceConfiguration: ").Append(ServiceConfiguration).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as SystemConfigurationChangeRequestDTO);
        }

        /// <summary>
        /// Returns true if SystemConfigurationChangeRequestDTO instances are equal
        /// </summary>
        /// <param name="input">Instance of SystemConfigurationChangeRequestDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SystemConfigurationChangeRequestDTO input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Timestamp == input.Timestamp ||
                    (this.Timestamp != null &&
                    this.Timestamp.Equals(input.Timestamp))
                ) && 
                (
                    this.Symptoms == input.Symptoms ||
                    this.Symptoms != null &&
                    input.Symptoms != null &&
                    this.Symptoms.SequenceEqual(input.Symptoms)
                ) && 
                (
                    this.ServiceConfiguration == input.ServiceConfiguration ||
                    this.ServiceConfiguration != null &&
                    input.ServiceConfiguration != null &&
                    this.ServiceConfiguration.SequenceEqual(input.ServiceConfiguration)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Timestamp != null)
                {
                    hashCode = (hashCode * 59) + this.Timestamp.GetHashCode();
                }
                if (this.Symptoms != null)
                {
                    hashCode = (hashCode * 59) + this.Symptoms.GetHashCode();
                }
                if (this.ServiceConfiguration != null)
                {
                    hashCode = (hashCode * 59) + this.ServiceConfiguration.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}