namespace ToDoList.Client.Services
{
    public class MyDataService
    {
        public event Action<bool> NoteCreated;

        public void NotifyNoteCreated(bool created)
        {
            NoteCreated?.Invoke(created);
        }
    }
        
}
