public class Todoitem
{
    public Todoitem(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
    public bool Completed {get; set;}
}
