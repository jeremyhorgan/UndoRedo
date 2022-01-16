using System;
using System.Collections.Generic;
using System.ComponentModel;
using UndoRedo.Core;

namespace UndoRedo.WinForms
{
    public class UndoRedoBindingList<T> : BindingList<T>
    {
        private readonly IUndoRedoService _undoRedoService;

        public UndoRedoBindingList(IUndoRedoService undoRedoService)
        {
            _undoRedoService = undoRedoService;
        }

        public UndoRedoBindingList(IUndoRedoService undoRedoService, IList<T> list) 
            : base(list)
        {
            _undoRedoService = undoRedoService ?? throw new ArgumentNullException(nameof(undoRedoService));
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            if (!_undoRedoService.IsExecutingUndoRedoCommand)
            {
                _undoRedoService.Add(new UndoRedoBindingListItemAddedCommand<T>(this, index));
            }
        }

        protected override void RemoveItem(int index)
        {
            if (!_undoRedoService.IsExecutingUndoRedoCommand)
            {
                // Add the command before deleting the item from the collection to record the item being deleted
                _undoRedoService.Add(new UndoRedoBindingListItemDeletedCommand<T>(this, index));
            }

            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            if (!_undoRedoService.IsExecutingUndoRedoCommand)
            {
                // Add the command before setting the 'item' at the 'index' 'position to record the item being deleted
                _undoRedoService.Add(new UndoRedoBindingListItemChangedCommand<T>(this, index, item));
            }
            
            base.SetItem(index, item);
        }
    }
}
