namespace BlazorTeacher.Dashboard.Services;

/// <summary>
/// State management service for the Dashboard.
/// Demonstrates state management patterns in Blazor.
/// </summary>
public class AppState
{
    private string _currentTheme = "light";
    private bool _isSidebarCollapsed = false;

    public string CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme != value)
            {
                _currentTheme = value;
                NotifyStateChanged();
            }
        }
    }

    public bool IsSidebarCollapsed
    {
        get => _isSidebarCollapsed;
        set
        {
            if (_isSidebarCollapsed != value)
            {
                _isSidebarCollapsed = value;
                NotifyStateChanged();
            }
        }
    }

    public string? SelectedCourseTitle { get; set; }

    /// <summary>
    /// Event fired when state changes.
    /// Components can subscribe to this to re-render on state changes.
    /// </summary>
    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public void ToggleSidebar()
    {
        IsSidebarCollapsed = !IsSidebarCollapsed;
    }

    public void ToggleTheme()
    {
        CurrentTheme = CurrentTheme == "light" ? "dark" : "light";
    }
}
