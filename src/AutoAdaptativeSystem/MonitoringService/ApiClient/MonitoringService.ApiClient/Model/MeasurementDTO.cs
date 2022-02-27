/*
 * Monitoring Service
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
using FileParameter = MonitoringService.ApiClient.Client.FileParameter;
using OpenAPIDateConverter = MonitoringService.ApiClient.Client.OpenAPIDateConverter;

namespace MonitoringService.ApiClient.Model
{
    /// <summary>
    /// MeasurementDTO
    /// </summary>
    [DataContract(Name = "MeasurementDTO")]
    public partial class MeasurementDTO : IEquatable<MeasurementDTO>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementDTO" /> class.
        /// </summary>
        /// <param name="probeId">probeId.</param>
        /// <param name="property">property.</param>
        public MeasurementDTO(Guid probeId = default(Guid), Property property = default(Property))
        {
            this.ProbeId = probeId;
            this.Property = property;
        }

        /// <summary>
        /// Gets or Sets ProbeId
        /// </summary>
        [DataMember(Name = "probeId", EmitDefaultValue = false)]
        public Guid ProbeId { get; set; }

        /// <summary>
        /// Gets or Sets Property
        /// </summary>
        [DataMember(Name = "property", EmitDefaultValue = false)]
        public Property Property { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MeasurementDTO {\n");
            sb.Append("  ProbeId: ").Append(ProbeId).Append("\n");
            sb.Append("  Property: ").Append(Property).Append("\n");
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
            return this.Equals(input as MeasurementDTO);
        }

        /// <summary>
        /// Returns true if MeasurementDTO instances are equal
        /// </summary>
        /// <param name="input">Instance of MeasurementDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MeasurementDTO input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.ProbeId == input.ProbeId ||
                    (this.ProbeId != null &&
                    this.ProbeId.Equals(input.ProbeId))
                ) && 
                (
                    this.Property == input.Property ||
                    (this.Property != null &&
                    this.Property.Equals(input.Property))
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
                if (this.ProbeId != null)
                {
                    hashCode = (hashCode * 59) + this.ProbeId.GetHashCode();
                }
                if (this.Property != null)
                {
                    hashCode = (hashCode * 59) + this.Property.GetHashCode();
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
