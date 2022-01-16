namespace UndoRedo.Core
{
    public interface IUndoRedoService
    {
        bool IsExecutingUndoRedoCommand { get; set; }
        void Add(IUndoRedoCommand command);
        void Undo();
        void Redo();
    }
}