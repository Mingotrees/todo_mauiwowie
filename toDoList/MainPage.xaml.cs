using System.Collections.ObjectModel;
namespace toDoList;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ToDoClass> TodoItems { get; set; }
    private ToDoClass? _editingItem;
    private int _nextId = 1;

    public MainPage()
    {
        InitializeComponent();
        TodoItems = new ObservableCollection<ToDoClass>();
        BindingContext = this;
    }

    private async void AddToDoItem(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(titleEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a title", "OK");
            return;
        }

        var newItem = new ToDoClass
        {
            id = _nextId++,
            title = titleEntry.Text,
            detail = detailsEditor.Text ?? string.Empty
        };

        TodoItems.Add(newItem);
        ResetEditMode();
    }

    private void EditToDoItem(object sender, EventArgs e)
    {
        if (_editingItem == null) return;

        _editingItem.title = titleEntry.Text ?? string.Empty;
        _editingItem.detail = detailsEditor.Text ?? string.Empty;

        ResetEditMode();
    }

    private void CancelEdit(object sender, EventArgs e) => ResetEditMode();

    private void OnCardTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not ToDoClass selected) return;

        _editingItem = selected;
        titleEntry.Text = _editingItem.title;
        detailsEditor.Text = _editingItem.detail;

        addBtn.IsVisible = false;
        editBtn.IsVisible = true;
        cancelBtn.IsVisible = true;
    }

    private void DeleteToDoItem(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        if (button.CommandParameter is not ToDoClass item) return;

        TodoItems.Remove(item);

        if (_editingItem == item)
            ResetEditMode();
    }

    private void ResetEditMode()
    {
        _editingItem = null;
        titleEntry.Text = string.Empty;
        detailsEditor.Text = string.Empty;
        addBtn.IsVisible = true;
        editBtn.IsVisible = false;
        cancelBtn.IsVisible = false;
    }
}