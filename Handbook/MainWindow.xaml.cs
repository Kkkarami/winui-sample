using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Handbook;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();


    }

    private void MainNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem navigationViewItem)
        {
            string pageName = navigationViewItem.Tag.ToString()!;
            Type pageType = Type.GetType($"Handbook.Pages.{pageName}")!;
            ContentFrame.Navigate(pageType);

            sender.Header = navigationViewItem.Content.ToString();

        }
    }
}
