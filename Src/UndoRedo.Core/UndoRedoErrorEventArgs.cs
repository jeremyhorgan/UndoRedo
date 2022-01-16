using System;

namespace UndoRedo.Core
{
    public class UndoRedoErrorEventArgs : EventArgs
    {
        public UndoRedoErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; }
    }
}