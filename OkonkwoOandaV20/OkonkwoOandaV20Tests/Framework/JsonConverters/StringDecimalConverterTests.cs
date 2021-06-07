using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using System.Text;

namespace OkonkwoOandaV20Tests.Framework.JsonConverters
{
   [TestClass]
   public class StringDecimalConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_decimal()
	  {
		 //arrange
		 var converter = new StringDecimalConverter();

		 //act
		 var canConvertDecimal = converter.CanConvert(typeof(decimal));
		 var canConvertDecimalNullable = converter.CanConvert(typeof(decimal?));

		 //assert
		 Assert.IsTrue(canConvertDecimal);
		 Assert.IsTrue(canConvertDecimalNullable);
	  }

	  [TestMethod]
	  public void success_WriteJson_writes_decmial()
	  {
		 // arrange
		 var stringBuilder = new StringBuilder();
		 var textWriter = new System.IO.StringWriter(stringBuilder);
		 var writer = new JsonTextWriter(textWriter);
		 var value = (decimal)12.5;
		 var serializer = new JsonSerializer();
		 var converter = new StringDecimalConverter();

		 //act
		 converter.WriteJson(writer, value, serializer);

		 //assert
		 Assert.IsTrue(stringBuilder.ToString() == $"\"{value}\"");
	  }
   }
}
