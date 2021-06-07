using Microsoft.VisualStudio.TestTools.UnitTesting;
using OkonkwoOandaV20.Framework.TypeConverters;
using System.Collections.Generic;

namespace OkonkwoOandaV20Tests.Framework.TypeConverters
{
   [TestClass]
   public class ListToCsvConverterTests
   {
	  [TestMethod]
	  public void success_Can_Convert_type_IList()
	  {
		 // arrange
		 var delimiter = ",";
		 var converter = new ListToCsvConverter(delimiter);

		 // act
		 var canConvertList = converter.CanConvert(typeof(List<string>));

		 // assert
		 Assert.IsTrue(canConvertList);
	  }

	  [TestMethod]
	  public void failure_can_convert_type_not_IList()
	  {
		 // arrange
		 var delimiter = ",";
		 var converter = new ListToCsvConverter(delimiter);

		 // act
		 var canConvertObject = converter.CanConvert(typeof(object));

		 // assert
		 Assert.IsFalse(canConvertObject);
	  }

	  [TestMethod]
	  public void success_ToOuput_List_string_returns_correct_value()
	  {
		 // arrange
		 var delimiter = ',';
		 var list = new List<string>("one.two.three".Split('.'));
		 var converter = new ListToCsvConverter(delimiter.ToString());

		 // act
		 var convertedList = converter.ToOutput(list);
		 var convertedListArray = convertedList.Split(delimiter);

		 // assert
		 Assert.IsTrue(convertedListArray.Length == 3);
		 Assert.AreEqual(convertedListArray[0], "one");
		 Assert.AreEqual(convertedListArray[1], "two");
		 Assert.AreEqual(convertedListArray[2], "three");
	  }
   }
}
