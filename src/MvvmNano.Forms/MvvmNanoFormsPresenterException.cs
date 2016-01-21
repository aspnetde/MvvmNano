/*****************************************************************************
 * Copyright (c) Thomas Bandt (https://thomasbandt.com). Licensed under the 
 * MIT License. See LICENSE file in the project root for detailed information. 
 ****************************************************************************/
using System;

namespace MvvmNano.Forms
{
    public class MvvmNanoFormsPresenterException : Exception
    {
        internal MvvmNanoFormsPresenterException(string message) : base(message)
        {
        }
    }
}

