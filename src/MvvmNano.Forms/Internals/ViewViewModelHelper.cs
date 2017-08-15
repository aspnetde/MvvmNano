using System;
using System.Diagnostics;

namespace MvvmNano.Forms.Internals
{ 
    public static class ViewViewModelHelper
    {
        private const string VIEW_MODEL_SUFFIX = "ViewModel";

        private const string PAGE_SUFFIX = "Page";
 
        public static string ViewNameFromViewModel(Type viewModelType)
        { 
            var viewModelName = viewModelType.Name;
            if (!viewModelName.EndsWith(VIEW_MODEL_SUFFIX))
                throw new MvvmNanoException(
                    $"{viewModelName} is treated as view model but does not end with '{VIEW_MODEL_SUFFIX}'. Name should be '{viewModelName}{VIEW_MODEL_SUFFIX}', corresponding view should be named '{viewModelName}{PAGE_SUFFIX}'.");
                
            return viewModelName.Replace(VIEW_MODEL_SUFFIX, PAGE_SUFFIX);
        }

        public static string ViewModelNameFromView(Type viewType)
        {
            var viewName = viewType.Name;
            if (!viewName.EndsWith(PAGE_SUFFIX))
                throw new MvvmNanoException($"{viewName} is treated as view model but does not end with '{PAGE_SUFFIX}'. Name should be '{viewName}{PAGE_SUFFIX}', corresponding view should be named '{viewName}{VIEW_MODEL_SUFFIX}'.");

            return viewName.Replace(PAGE_SUFFIX, VIEW_MODEL_SUFFIX);
        }
    }
}