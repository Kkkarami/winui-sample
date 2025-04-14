using System.Collections.ObjectModel;
using Handbook.Entities;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;



namespace Handbook.Pages;


public sealed partial class HanbookPage : Page
{
    public ObservableCollection<Post> Posts { get; } = new ObservableCollection<Post>();
    public HanbookPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        const string sharedImagePath = "ms-appx:///Assets/NotFound.png";
        var postsToAdd = new[]
        {
            new Post("HDD", "Andrii Fed", "That post about HDD", sharedImagePath),
            new Post("SSD", "Andrii Fed", "That post about SSD", sharedImagePath),
            new Post("CD", "Andrii Fed", "That post about CD", sharedImagePath),
            new Post("USB", "Andrii Fed", "That post about USB", sharedImagePath),
            new Post("Floppy", "Andrii Fed", "That post about Floppy", sharedImagePath)
        };
        foreach (var post in postsToAdd)
        {
            Posts.Add(post);
        }
        if (e.Parameter is Post newPost)
        {
            Posts.Add(newPost);
        }


    }
}
