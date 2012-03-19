using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Reflection;

namespace JsonDictionaryExample.Extensions
{
    public class TypedDictionaryModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var modelType = bindingContext.ModelType;
            if (!typeof(Dictionary<string, object>).Equals(modelType) && modelType.IsGenericType && 
                modelType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                // Dictionary<string, [some type]>, must use shenanigans

                // Must build a BindingContext which will work for a 
                // generic Dictionary<string, object> type.
                // This is lifted directly from action BindComplexModel() in the 
                // DefaultModelBinder class in MVC 3
                var dictionaryBindingContext = new ModelBindingContext()
                {
                    ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, typeof(Dictionary<string, object>)),
                    ModelName = bindingContext.ModelName,
                    ModelState = bindingContext.ModelState,
                    PropertyFilter = bindingContext.PropertyFilter,
                    ValueProvider = bindingContext.ValueProvider
                };
                // end of lifted code

                // bind our generic Dictionary<string, object> model
                var genericDictionary = base.BindModel(controllerContext, dictionaryBindingContext) as Dictionary<string, object>;
                // create our Dictionary<string, [some type]> object
                var targetDictionary = (IDictionary)Activator.CreateInstance(modelType);

                // copy entries from our generic dictionary
                if (genericDictionary != null)
                    foreach (var item in genericDictionary)
                        targetDictionary.Add(item.Key, item.Value);

                return targetDictionary;
            }

            // either a simple Dictionary<string, object> object, or
            // "I'm not any type of Dictionary<,> object at all!" said Dorothy, no need for any shenanigans
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}