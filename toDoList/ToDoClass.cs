using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace toDoList;

public class ToDoClass : INotifyPropertyChanged
{
    private int _id;
    private string _title = string.Empty;
    private string _detail = string.Empty;
    private string _cardColor = string.Empty;

    private static readonly Random _random = new Random();

    private static readonly string[] WarmColors =
    {
        "#F5E6D3", "#FFD6D6", "#FFE9D6", "#D6F5E6",
        "#FFF9D6", "#E6D6F5", "#FFE6E6", "#D6F0FF"
    };

    public int id
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); }
    }

    public string title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    public string detail
    {
        get => _detail;
        set { _detail = value; OnPropertyChanged(); }
    }

    public string CardColor
    {
        get => _cardColor;
        set { _cardColor = value; OnPropertyChanged(); }
    }

    public ToDoClass()
    {
        _cardColor = WarmColors[_random.Next(WarmColors.Length)];
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}