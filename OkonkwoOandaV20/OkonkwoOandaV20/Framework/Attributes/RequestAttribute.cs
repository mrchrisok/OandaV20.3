using System;

namespace OkonkwoOandaV20.Framework
{
   public abstract class RequestAttribute : Attribute
   {
	  protected RequestAttribute(string name = null, Type converter = null)
	  {
		 Name = name ?? Name;
		 Converter = converter ?? Converter;
	  }

	  public string Name { get; set; }
	  public Type Converter { get; set; }

	  public virtual object GetValue(object value)
	  {
		 return value;
	  }
   }
}
