# JSON Dictionary Binding

This project contains a custom **ModelBinder** and **ValueProviderFactory** for binding JSON dictionaries to MVC models.

Usage is simple.  Modify your **Global.asax.cs** file to replace the standard MVC3 JsonValueProviderFactory with the ExtendedJsonValueProviderFactory.  Then, if you have a JSON dictionary of a type other than **Dictionary<string, object\>**, you can either use the **TypedDictionaryModelBinder** in your Controller's action method declaration, or you can set the binding for your typed dictionary in the **Global.asax.cs** file.

An example page can be seen at [this page at Form.Vu](http://oss.form.vu/json-dictionary-binding).

Thanks to [Jeroen](http://www.blogger.com/profile/04006240249657777976), whose [blog post here](http://buildingwebapps.blogspot.com/2012/01/passing-javascript-json-dictionary-to.html) inspired me to revisit and fix my ExtendedJsonValueProviderFactory.