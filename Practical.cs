using System.ComponentModel;

public class Book
{
    private string _title;
    private string _author;
    private int _pages;

    public Book(string author, string title, int pages)
    {
        _author = author;
        _title = title;
        _pages = pages;

    }
    public bool IsLong()
    {
        bool value =  false;
        if(_pages > 300)
        {
            value = true;
        }
        else
        {
            value =  false;
        }
        return value;
    }
}