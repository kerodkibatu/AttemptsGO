namespace AttemptsGO.ViewModels;
public partial class LobbyViewModel : BaseViewModel
{
    [ObservableProperty]
    string p1Name;
    [ObservableProperty]
    string p2Name;
    [ObservableProperty]
    string p1Number;
    [ObservableProperty]
    string p2Number;
    [RelayCommand]
    async Task Start()
    {
        if (P1Number.Distinct().Count() != 4)
        {
            await Shell.Current.DisplayAlert("Input Invalid!", "Player 1's input is invalid!", "OK!");
            return;
        }
        if (P2Number.Distinct().Count() != 4)
        {
            await Shell.Current.DisplayAlert("Input Invalid!", "Player 2's input is invalid!", "OK!");
            return;
        }
        await Shell.Current.DisplayAlert("NEW MATCH!", $"{P1Name} vs {P2Name}", "START!");
        await Shell.Current.GoToAsync("GamePage", parameters: new Dictionary<string, object>()
        {
            {"P1Name",P1Name},
            {"P2Name",P2Name},
            {"P1Number",P1Number},
            {"P2Number",P2Number},
        });
    }
    [RelayCommand]
    public async Task RandomizeName(string playerId)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("Names.txt");
        using var reader = new StreamReader(stream);
        var contents = reader.ReadToEnd();
        string[] possible = contents.Split('\n');
        string value = possible.OrderBy(c => Random.Shared.Next()).ElementAt(0).Trim();
        if (playerId == "1")
            P1Name = value;
        else
            P2Name = value;
    }
    [RelayCommand]
    public void Randomize(string playerId)
    {
        string possible = "123456789";
        string value = new string([..possible.OrderBy(c => Random.Shared.Next())])[..4];
        if(playerId == "1")
            P1Number = value;
        else
            P2Number = value;
    }
}