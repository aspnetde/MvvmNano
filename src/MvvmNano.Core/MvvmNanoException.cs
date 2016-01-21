/*****************************************************************************
 * Copyright (c) Thomas Bandt (https://thomasbandt.com). Licensed under the 
 * MIT License. See LICENSE file in the project root for detailed information. 
 ****************************************************************************/
using System;

namespace MvvmNano
{
    /// <summary>
    /// Is thrown whenever something bad happens internally of MvvmNano.
    /// </summary>
    public class MvvmNanoException : Exception
    {
        internal MvvmNanoException(string message) : base(message)
        {
        }
    }
}

