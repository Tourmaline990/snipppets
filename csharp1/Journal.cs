public class Journal
{
    private List<Entry>_entries = new List<Entry>();

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }
    public int CountEntry()
    {
        if (_entries.Count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_entries),"Empty Journal, no entries recorded");
        }
       return _entries.Count;
    }
    public void DisplayEntry()
    {
        if (_entries.Count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_entries),"Empty Journal, no entries recorded");
        }
        int i = 1;
        foreach (Entry e in _entries)
        {
            Console.WriteLine($"{i} {e.DisplayFormat()}");
            Console.WriteLine("");
            i++;
        }
    }
    public void RemoveEntry(int index)
    {
        index = index - 1;
        if (index > 0 && index < _entries.Count  )
        {    
        _entries.RemoveAt(index);
        }
        else{
        throw new ArgumentOutOfRangeException(nameof(index), "Index not found.");
        }
    }
}