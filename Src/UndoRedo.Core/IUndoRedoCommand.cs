namespace UndoRedo.Core
{
    public interface IUndoRedoCommand
    {
        void Undo();
        void Redo();
    }
}