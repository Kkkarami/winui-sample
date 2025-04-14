using System;
using System.Threading.Tasks;
using Handbook.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;



namespace Handbook.Pages;


public sealed partial class AddTextPage : Page
{
    private StorageFile selectedImageFile = null;
    public AddTextPage()
    {
        this.InitializeComponent();
    }

    private async void PickImageButton_Click(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker();
        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".png");
        picker.FileTypeFilter.Add(".jpeg");
        var hWnd = WindowNative.GetWindowHandle(App.MainWindow);
        InitializeWithWindow.Initialize(picker, hWnd);

        StorageFile selectedImageFile = await picker.PickSingleFileAsync();
        if (selectedImageFile != null)
        {
            ImagePathText.Text = selectedImageFile.Name;
        }

    }
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(HanbookPage));
    }
    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        ClearErrors();

        try
        {
            Post newPost = new Post(
                title: TitleTextBox.Text,
                author: AuthorTextBox.Text,
                description: DescriptionTextBox.Text
            );

            newPost.ImagePath = await SaveImageAync() ?? "ms-appx:///Assets/NotFound.png";
            Frame.Navigate(typeof(HanbookPage), newPost);

        }
        catch (ValidationException ex)
        {
            if (ex.Errors.ContainsKey("Title"))
            {
                ShowError(TitleError, string.Join("\n", ex.Errors["Title"]));
            }
            if (ex.Errors.ContainsKey("Author"))
            {
                ShowError(AuthorError, string.Join("\n", ex.Errors["Author"]));
            }
        }




    }

    private async Task<string> SaveImageAync()
    {
        if (selectedImageFile == null)
        {
            return null;
        }
        var localFolder = ApplicationData.Current.LocalFolder;
        var newFile = await localFolder.CreateFileAsync(selectedImageFile.Name, CreationCollisionOption.ReplaceExisting);
        await selectedImageFile.CopyAndReplaceAsync(newFile);
        return newFile.Name;
    }

    private void ShowError(TextBlock errorBlock, string message)
    {
        errorBlock.Text = message;
        errorBlock.Visibility = Visibility.Visible;
    }
    private void ClearErrors()
    {
        TitleError.Visibility = Visibility.Collapsed;
        AuthorError.Visibility = Visibility.Collapsed;
    }
}
