using System.ComponentModel;
using UndoRedo.Core;

namespace UndoRedo.WinForms
{
    internal class UndoRedoBindingListItemDeletedCommand<T> : IUndoRedoCommand
    {
        private readonly BindingList<T> _list;
        private readonly int _position;
        private readonly T _deletedItem;

        public UndoRedoBindingListItemDeletedCommand(BindingList<T> list, int position)
        {
            _list = list;
            _position = position;
            _deletedItem = list[position];
        }

        public void Undo()
        {
            _list.Insert(_position, _deletedItem);
        }

        public void Redo()
        {
            _list.RemoveAt(_position);
        }
    }
}