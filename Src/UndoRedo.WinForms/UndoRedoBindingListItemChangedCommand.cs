using System.ComponentModel;
using UndoRedo.Core;

namespace UndoRedo.WinForms
{
    internal class UndoRedoBindingListItemChangedCommand<T> : IUndoRedoCommand
    {
        private readonly BindingList<T> _list;
        private readonly int _position;
        private readonly T _deletedItem;
        private readonly T _newItem;

        public UndoRedoBindingListItemChangedCommand(BindingList<T> list, int position, T newItem)
        {
            _list = list;
            _position = position;
            _deletedItem = list[position];
            _newItem = newItem;
        }

        public void Undo()
        {
            _list[_position] = _deletedItem;
        }

        public void Redo()
        {
            _list[_position] = _newItem;
        }
    }
}