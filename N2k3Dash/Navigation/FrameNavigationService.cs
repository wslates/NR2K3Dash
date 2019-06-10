using N2k3Dash.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using N2k3Dash.Model;

class FrameNavigationService : IFrameNavigationService, INotifyPropertyChanged
{
    #region Fields
    private readonly Dictionary<string, Uri> _pagesByKey;
    private int _current = 0;
    private readonly List<string> _pages;
    private LowLevelKeyboardListener _listener;
    private string _currentPageKey;
    
    #endregion
    #region Properties                                              
    public string CurrentPageKey
    {
        get
        {
            return _currentPageKey;
        }

        private set
        {
            if (_currentPageKey == value)
            {
                return;
            }

            _currentPageKey = value;
            OnPropertyChanged("CurrentPageKey");
        }
    }
    public object Parameter { get; private set; }
    #endregion
    #region Ctors and Methods
    public FrameNavigationService()
    {
        _pagesByKey = new Dictionary<string, Uri>();
        _pages = new List<string>();
        _listener = new LowLevelKeyboardListener();
        _listener.OnKeyPressed += OnKeyPressed;
        _listener.HookKeyboard();
    }
    public void GoBack()
    {
        if (_current == 0)
        {
            _current = _pages.Count - 1;
        } else
        {
            _current--;
        }

        NavigateTo(_pages[_current]);
    }

    public void GoForward()
    {
        if (_current == _pages.Count - 1)
        {
            _current = 0;
        } else
        {
            _current++;
        }

        NavigateTo(_pages[_current]);
    }

    public void NavigateTo(string pageKey)
    {
        NavigateTo(pageKey, null);
    }

    public void NavigateTo(string pageKey, object parameter)
    {
        lock (_pagesByKey)
        {
            if (!_pagesByKey.ContainsKey(pageKey))
            {
                throw new ArgumentException(string.Format("No such page: {0} ", pageKey), "pageKey");
            }

            var frame = GetDescendantFromName(Application.Current.MainWindow, "currentDisplay") as Frame;

            if (frame != null)
            {
                frame.Source = _pagesByKey[pageKey];
            }
            Parameter = parameter;
            CurrentPageKey = pageKey;
        }
    }

    public void Configure(string key, Uri pageType)
    {
        lock (_pagesByKey)
        {
            if (_pagesByKey.ContainsKey(key))
            {
                _pagesByKey[key] = pageType;
            }
            else
            {
                _pagesByKey.Add(key, pageType);
                _pages.Add(key);


            }
        }
    }

    public void Unhook()
    {
        _listener.UnHookKeyboard();
    }

    private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
    {
        var count = VisualTreeHelper.GetChildrenCount(parent);

        if (count < 1)
        {
            return null;
        }

        for (var i = 0; i < count; i++)
        {
            var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
            if (frameworkElement != null)
            {
                if (frameworkElement.Name == name)
                {
                    return frameworkElement;
                }

                frameworkElement = GetDescendantFromName(frameworkElement, name);
                if (frameworkElement != null)
                {
                    return frameworkElement;
                }
            }
        }
        return null;
    }
    private void OnKeyPressed(object sender, KeyPressedArgs e)
    {
        if (e.KeyPressed.Equals(System.Windows.Input.Key.OemPeriod))
        {
            GoForward();

        }
        else if (e.KeyPressed.Equals(System.Windows.Input.Key.OemComma))
        {
            GoBack();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
