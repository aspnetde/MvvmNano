/*****************************************************************************
 * Copyright (c) Thomas Bandt (https://thomasbandt.com). Licensed under the 
 * MIT License. See LICENSE file in the project root for detailed information. 
 ****************************************************************************/
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    public class MvvmNanoApplication : Application
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpPresenter();
        }

        protected virtual void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterConcreteInstance<IPresenter>(
                new MvvmNanoFormsPresenter(this)
            );
        }
    }
}

