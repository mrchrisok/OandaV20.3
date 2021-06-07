using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.TypeConverters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public abstract class Parameters
   {
	  #region headers

	  [Header("Accept-Datetime-Format")]
	  public virtual AcceptDatetimeFormat? AcceptDatetimeFormat { get; set; } = REST.AcceptDatetimeFormat.RFC3339;

	  #endregion

	  /// <summary>
	  /// Retrieves a collection request paramters.
	  /// </summary>
	  /// <typeparam name="P">The type of reqeust attributes to retrieve.</typeparam>
	  /// <param name="converters">List of custom string converters to use for writing values.</param>
	  /// <param name="excludeNulls">Flag that indicates if null values should be excluded</param>
	  /// <returns></returns>
	  internal virtual IDictionary<string, object> GetRequestParameters<P>(IList<ITypeConverter<string>> converters = null, bool excludeNulls = true)
		 where P : RequestAttribute
	  {
		 var requestParameters = this.GetType()
			.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
			.Where(prop => prop.GetCustomAttribute<P>() != null)
			.Select(prop =>
			{
			   var customName = prop.GetCustomAttribute<P>().Name;
			   var propName = !string.IsNullOrWhiteSpace(customName) ? customName : prop.Name;
			   var propValue = GetPropertyValue<P>(prop, converters);

			   return new KeyValuePair<string, object>(propName, propValue);
			})
			.Where(kvp => !excludeNulls || kvp.Value != null)
			.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

		 return requestParameters;
	  }

	  private object GetPropertyValue<P>(PropertyInfo propInfo, IList<ITypeConverter<string>> converters)
		 where P : RequestAttribute
	  {
		 var typeConverter = converters?.FirstOrDefault(converter => converter.CanConvert(propInfo.PropertyType));

		 if (typeConverter != null)
		 {
			return typeConverter.ToOutput(propInfo.GetValue(this));
		 }

		 return propInfo.GetCustomAttribute<P>().GetValue(propInfo.GetValue(this));
	  }

	  /// <summary>
	  /// The type converters used to write parameter values.
	  /// </summary>
	  internal List<ITypeConverter<string>> TypeConverters
	  {
		 get
		 {
			var converters = new List<ITypeConverter<string>>();

			if (AcceptDatetimeFormat.HasValue)
			{
			   converters.Add(new AcceptDateTimeToStringConverter(AcceptDatetimeFormat.Value));
			}

			return converters;
		 }
	  }
   }
}
