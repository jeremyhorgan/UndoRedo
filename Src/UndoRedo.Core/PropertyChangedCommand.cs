namespace UndoRedo.Core
{
    public class PropertyChangedCommand : IUndoRedoCommand
    {
        private readonly object _item;
        private readonly string _propertyName;
        private readonly object _oldValue;
        private readonly object _newValue;

        public PropertyChangedCommand(object item, string propertyName, object oldValue, object newValue)
        {
            _item = item;
            _propertyName = propertyName;
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public void Undo()
        {
            SetPropertyValue(_oldValue);
        }

        public void Redo()
        {
            SetPropertyValue(_newValue);
        }

        private void SetPropertyValue(object value)
        {
            var propertyInfo = _item.GetType().GetProperty(_propertyName);
            propertyInfo?.SetValue(_item, value);
        }
    }
}