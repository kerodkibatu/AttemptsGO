namespace AttemptsGO.Views;

public partial class HistoryPopup
{
	public HistoryPopup(GameViewModel gvm)
	{
		InitializeComponent();
		BindingContext = gvm;
	}
}