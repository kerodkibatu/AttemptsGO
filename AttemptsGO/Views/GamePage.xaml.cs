namespace AttemptsGO.Views;

public partial class GamePage : ContentPage
{
	public GamePage(GameViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
