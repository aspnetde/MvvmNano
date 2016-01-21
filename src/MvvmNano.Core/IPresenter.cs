/*****************************************************************************
 * Copyright (c) Thomas Bandt (https://thomasbandt.com). Licensed under the 
 * MIT License. See LICENSE file in the project root for detailed information. 
 ****************************************************************************/

namespace MvvmNano
{
    /// <summary>
    /// The central point of navigation, which is responsible for creating 
    /// the Views along with their View Models and pushing those views to 
    /// the navigation stack.
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Navigates to the View Model of the given type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the View Model which should be shown.</typeparam>
        void NavigateToViewModel<TViewModel>();

        /// <summary>
        /// Navigates to the View Model of the given type and passes a parameter.
        /// </summary>
        /// <param name="parameter">The parameter which is passed to the View Model's Initialize() method.</param>
        /// <typeparam name="TViewModel">The type of the View Model which should be shown.</typeparam>
        /// <typeparam name="TNavigationParameter">The type of the parameter which should be passed to the View Model's Initialize() method.</typeparam>
        void NavigateToViewModel<TViewModel, TNavigationParameter>(TNavigationParameter parameter);
    }
}

