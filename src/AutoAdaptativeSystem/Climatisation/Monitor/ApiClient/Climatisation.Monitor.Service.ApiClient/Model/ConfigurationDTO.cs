/*
 * Room Monitor Service
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
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
using FileParameter = Climatisation.Monitor.Service.ApiClient.Client.FileParameter;
using OpenAPIDateConverter = Climatisation.Monitor.Service.ApiClient.Client.OpenAPIDateConverter;

namespace Climatisation.Monitor.Service.ApiClient.Model
{
    /// <summary>
    /// ConfigurationDTO
    /// </summary>
    [DataContract(Name = "ConfigurationDTO")]
    public partial class ConfigurationDTO : IEquatable<ConfigurationDTO>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDTO" /> class.
        /// </summary>
        /// <param name="name">Gets or Sets Name.</param>
        /// <param name="value">Gets or Sets Value.</param>
        /// <param name="lastModification">Gets or Sets LastModification.</param>
        public ConfigurationDTO(string name = default(string), string value = default(string), DateTime lastModification = default(DateTime))
        {
            this.Name = name;
            this.Value = value;
            this.LastModification = lastModification;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        /// <value>Gets or Sets Name</value>
        [DataMember(Name = "name", EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        /// <value>Gets or Sets Value</value>
        [DataMember(Name = "value", EmitDefaultValue = true)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or Sets LastModification
        /// </summary>
        /// <value>Gets or Sets LastModification</value>
        [DataMember(Name = "lastModification", EmitDefaultValue = false)]
        public DateTime LastModification { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ConfigurationDTO {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  LastModification: ").Append(LastModification).Append("\n");
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
            return this.Equals(input as ConfigurationDTO);
        }

        /// <summary>
        /// Returns true if ConfigurationDTO instances are equal
        /// </summary>
        /// <param name="input">Instance of ConfigurationDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ConfigurationDTO input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
                ) && 
                (
                    this.LastModification == input.LastModification ||
                    (this.LastModification != null &&
                    this.LastModification.Equals(input.LastModification))
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
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.Value != null)
                {
                    hashCode = (hashCode * 59) + this.Value.GetHashCode();
                }
                if (this.LastModification != null)
                {
                    hashCode = (hashCode * 59) + this.LastModification.GetHashCode();
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
