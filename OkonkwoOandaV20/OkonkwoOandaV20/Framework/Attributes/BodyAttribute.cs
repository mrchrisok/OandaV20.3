using System;

namespace OkonkwoOandaV20.Framework
{
   public class BodyAttribute : RequestAttribute
   {
	  public BodyAttribute(string name = null, Type converter = null)
		 : base(name, converter)
	  {
	  }
   }
}
