namespace AttemptsGO.Views;

public partial class LobbyPage : ContentPage
{
	LobbyViewModel vm;
	public LobbyPage(LobbyViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = vm = viewModel;
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        P1NumberEntry.Text = "";
		P2NumberEntry.Text = "";

		await vm.RandomizeName("1");
		vm.Randomize("1");
		await vm.RandomizeName("2");
        vm.Randomize("2");

        base.OnNavigatedTo(args);
    }
}
