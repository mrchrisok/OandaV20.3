using System;
using System.Collections;
using System.Linq;

namespace OkonkwoOandaV20.Framework.TypeConverters
{
   public class ListToCsvConverter : ITypeConverter<string>
   {
	  public ListToCsvConverter() : this(",")
	  {
	  }

	  /// <summary>
	  /// Constructs an instance of the ListToCsvConverter object.
	  /// </summary>
	  /// <param name="delimiter">The delimiter to use to concatenate the List items</param>
	  public ListToCsvConverter(string delimiter)
	  {
		 _delimeter = delimiter;
	  }

	  private string _delimeter;

	  public bool CanConvert(Type objectType)
	  {
		 var canConvert = objectType.GetInterfaces().FirstOrDefault(x => x == typeof(IList));

		 return canConvert != null;
	  }

	  /// <summary>
	  /// Concatenates the items of a List into a delimiter-separated string.
	  /// </summary>
	  /// <param name="input">The List object.</param>
	  /// <returns>A delimiter-separated string representing the items in the List object.</returns>
	  public string ToOutput(object input)
	  {
		 if (input == null)
		 {
			return null;
		 }

		 if (input is IList list)
		 {
			var output = Utilities.ConvertListToDelimitedValues(list, _delimeter);

			return output;
		 }

		 throw new ArgumentException($"The input type '{input.GetType().Name}' is invalid.");
	  }
   }
}
