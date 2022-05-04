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
    /// SetPropertyDTO
    /// </summary>
    [DataContract(Name = "SetPropertyDTO")]
    public partial class SetPropertyDTO : IEquatable<SetPropertyDTO>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetPropertyDTO" /> class.
        /// </summary>
        /// <param name="name">Gets or Sets Name.</param>
        /// <param name="value">Gets or Sets Value.</param>
        public SetPropertyDTO(string name = default(string), string value = default(string))
        {
            this.Name = name;
            this.Value = value;
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
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class SetPropertyDTO {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
            return this.Equals(input as SetPropertyDTO);
        }

        /// <summary>
        /// Returns true if SetPropertyDTO instances are equal
        /// </summary>
        /// <param name="input">Instance of SetPropertyDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SetPropertyDTO input)
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
