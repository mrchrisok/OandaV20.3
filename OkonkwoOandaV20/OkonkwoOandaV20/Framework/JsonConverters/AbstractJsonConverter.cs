using Newtonsoft.Json;
using System;

namespace OkonkwoOandaV20.Framework.JsonConverters
{
   public abstract class AbstractJsonConverter : JsonConverter
   {
	  /// <summary>
	  /// Gets a value indicating whether this Newtonsoft.Json.JsonConverter can write JSON.
	  /// </summary>
	  public override bool CanWrite => true;

	  /// <summary>
	  /// Gets a value indicating whether this Newtonsoft.Json.JsonConverter can read JSON.
	  /// </summary>
	  public override bool CanRead => true;

	  /// <summary>
	  /// Determines whether this instance can convert the specified object type.
	  /// </summary>
	  /// <param name="objectType">Type of the object.</param>
	  /// <returns>true if this instance can convert the specified object type; otherwise, false.</returns>
	  public override bool CanConvert(Type objectType)
	  {
		 throw new NotImplementedException("Inheriting class must implement.");
	  }

	  /// <summary>
	  /// Reads the JSON representation of the object.
	  /// </summary>
	  /// <param name="reader">he Newtonsoft.Json.JsonReader to read from.</param>
	  /// <param name="objectType">Type of the object.</param>
	  /// <param name="existingValue">The existing value of object being read.</param>
	  /// <param name="serializer">The calling serializer.</param>
	  /// <returns>The object value.</returns>
	  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	  {
		 throw new NotImplementedException("Inheriting class must implement.");
	  }

	  /// <summary>
	  /// Writes the JSON representation of the object.
	  /// </summary>
	  /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
	  /// <param name="value">The value.</param>
	  /// <param name="serializer">The calling serializer.instance can convert the specified object type; otherwise, false.</param>
	  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	  {
		 throw new InvalidOperationException("Use default serialization.");
	  }
   }
}
