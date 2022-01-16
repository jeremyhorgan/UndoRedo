using System.ComponentModel;
using UndoRedo.Core;

namespace UndoRedo.WinForms
{
    internal class UndoRedoBindingListItemAddedCommand<T> : IUndoRedoCommand
    {
        private readonly BindingList<T> _list;
        private readonly int _position;
        private readonly T _newItem;

        public UndoRedoBindingListItemAddedCommand(BindingList<T> list, int position)
        {
            _list = list;
            _position = position;
            _newItem = list[position];
        }

        public void Undo()
        {
            _list.RemoveAt(_position);
        }

        public void Redo()
        {
            _list.Insert(_position, _newItem);
        }
    }
}