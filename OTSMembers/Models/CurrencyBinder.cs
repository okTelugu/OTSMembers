using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;


namespace OTSMembers.Models
{
    public class CurrencyBinder : DefaultModelBinder
    {
        public override object BindModel(System.Web.Mvc.ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue("Amount");

            if (result != null)
            {
                decimal amount;
                if (Decimal.TryParse(result.AttemptedValue, NumberStyles.Currency, null, out amount))
                    return new Dollars { Amount = amount };

                bindingContext.ModelState.AddModelError("Amount", "Wrong amount format");
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}