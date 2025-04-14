
using Microsoft.UI.Xaml;



namespace Handbook;


public partial class App : Application
{
    public static Window MainWindow { get; set; } = new MainWindow();
    public App()
    {
        this.InitializeComponent();
    }


    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        //MainWindow = new MainWindow();
        MainWindow.Activate();
        MainWindow.ExtendsContentIntoTitleBar = true;

    }


}
