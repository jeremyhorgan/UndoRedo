using System;
using System.Collections.Generic;

namespace UndoRedo.Core
{
    public class UndoRedoService : IUndoRedoService
    {
        private readonly Stack<IUndoRedoCommand> _undoStack = new();
        private readonly Stack<IUndoRedoCommand> _redoStack = new();

        public event EventHandler<UndoRedoErrorEventArgs> Error;

        public bool IsExecutingUndoRedoCommand { get; set; }

        public void Add(IUndoRedoCommand command) => _undoStack.Push(command);

        public void Undo()
        {
            IsExecutingUndoRedoCommand = true;

            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                command.Undo();

                _redoStack.Push(command);
            }

            IsExecutingUndoRedoCommand = false;
        }

        public void Redo()
        {
            IsExecutingUndoRedoCommand = true;

            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                command.Redo();

                _undoStack.Push(command);
            }

            IsExecutingUndoRedoCommand = false;
        }

        public void Clear()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        public bool CanUndo => _undoStack.Count > 0;

        public bool CanRedo => _redoStack.Count > 0;

        protected internal virtual void OnError(Exception exception)
        {
            Error?.Invoke(this, new UndoRedoErrorEventArgs(exception));
        }
    }
}
