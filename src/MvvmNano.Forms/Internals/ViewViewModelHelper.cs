using System;

namespace MvvmNano.Forms.Internals
{ 
    /// <summary>
    /// Helper to get the name of a view based on the associated view model and vice versa.
    /// </summary>
    public static class ViewViewModelHelper
    {
        /// <summary>
        /// Suffix view models are required to have.
        /// </summary>
        private const string VIEW_MODEL_SUFFIX = "ViewModel";

        /// <summary>
        /// Suffix pages are required to have.
        /// </summary>
        private const string PAGE_SUFFIX = "Page";

        /// <summary>
        /// Gets the name of a view based on the associated view model.
        /// </summary>
        public static string ViewNameFromViewModel(Type viewModelType)
        { 
            var viewModelName = viewModelType.Name;
            if (!viewModelName.EndsWith(VIEW_MODEL_SUFFIX))
                throw new MvvmNanoException(
                    $"{viewModelName} is treated as view model but does not end with '{VIEW_MODEL_SUFFIX}'. Name should be '{viewModelName}{VIEW_MODEL_SUFFIX}', the corresponding view should be named '{viewModelName}{PAGE_SUFFIX}'.");
                
            return viewModelName.Replace(VIEW_MODEL_SUFFIX, PAGE_SUFFIX);
        }

        /// <summary>
        /// Gets the name of a view model based on the associated view.
        /// </summary>
        public static string ViewModelNameFromView(Type viewType)
        {
            var viewName = viewType.Name;
            if (!viewName.EndsWith(PAGE_SUFFIX))
                throw new MvvmNanoException($"{viewName} is treated as page but does not end with '{PAGE_SUFFIX}'. Name should be '{viewName}{PAGE_SUFFIX}', the corresponding view model should be named '{viewName}{VIEW_MODEL_SUFFIX}'.");

            return viewName.Replace(PAGE_SUFFIX, VIEW_MODEL_SUFFIX);
        }
    }
}