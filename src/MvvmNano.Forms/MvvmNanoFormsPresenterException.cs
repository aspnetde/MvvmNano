using System;

namespace MvvmNano.Forms
{
    /// <summary>
    /// Is thrown whenever something bad happens within 
    /// the MvvmNanoFormsPresenter
    /// </summary>
    public class MvvmNanoFormsPresenterException : Exception
    {
        internal MvvmNanoFormsPresenterException(string message) : base(message)
        {
        }
    }
}

