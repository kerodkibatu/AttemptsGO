namespace AttemptsGO.ViewModels;

public partial class GameViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    string p1Name;
    [ObservableProperty]
    string p2Name;
    [ObservableProperty]
    string p1Number;
    [ObservableProperty]
    string p2Number;

    [ObservableProperty]
    string p1Entry = "";
    [ObservableProperty]
    string p2Entry = "";

    [ObservableProperty]
    ObservableCollection<Attempt> p1Attempts = [];
    [ObservableProperty]
    ObservableCollection<Attempt> p2Attempts = [];

    //Winning
    [ObservableProperty]
    ObservableCollection<Attempt> winnerHistory = [];
    [ObservableProperty]
    string winnerAnnounce = "";
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("History"))
            return;
        P1Name = (string)query["P1Name"];
        P2Name = (string)query["P2Name"];
        P1Number = (string)query["P1Number"];
        P2Number = (string)query["P2Number"];
        
        Reset();
    }
    void Reset()
    {
        P1Attempts.Clear();
        P2Attempts.Clear();
        P1Entry = "";
        P2Entry = "";
    }
    [RelayCommand]
    void AddP1Entry(string digit)
    {
        if (P1Entry.Length >= 4 || P1Entry.Contains(digit))
            return;
        P1Entry += digit;
    }
    [RelayCommand]
    void P1Erase()
    {
        if (P1Entry.Length >= 1)
            P1Entry = new([..P1Entry.SkipLast(1)]);
    }
    [RelayCommand]
    async Task GuessP1()
    {
        if (P1Entry.Length < 4)
            return;
        var result = Attempt.Check(P1Entry, P2Number);
        var att = new Attempt(P1Entry, result.no, result.pos);
        P1Attempts.Add(att);
        P1Entry = "";
        if (att.Pos == 4)
            await Win(1);
    }
    [RelayCommand]
    void AddP2Entry(string digit)
    {
        if (P2Entry.Length >= 4 || P2Entry.Contains(digit))
            return;
        P2Entry += digit;
    }
    [RelayCommand]
    void P2Erase()
    {
        if (P2Entry.Length >= 1)
            P2Entry = new([.. P2Entry.SkipLast(1)]);
    }
    [RelayCommand]
    async Task GuessP2()
    {
        if (P2Entry.Length < 4)
            return;
        var result = Attempt.Check(P2Entry, P1Number);
        var att = new Attempt(P2Entry, result.no, result.pos);
        P2Attempts.Add(att);
        P2Entry = "";
        if (att.Pos == 4)
            await Win(2);
    }
    async Task Win(int winner)
    {
        var winnerName = winner == 1 ? P1Name : P2Name;
        var history = (winner == 1 ? P1Attempts : P2Attempts).ToArray();

        string Message = $"{winnerName} Won the Match in {history.Length} Attempts!";

        WinnerAnnounce = $"{winnerName} WON!";
        WinnerHistory.Clear();
        foreach (var item in history)
        {
            WinnerHistory.Add(item);
        }
        await Shell.Current.DisplayAlert("Victory Royale",Message,"Continue!");
        await Shell.Current.GoToAsync("HistoryPage", parameters: new Dictionary<string, object>()
        {
            {"History",true},
        });
    }
    [RelayCommand]
    async Task NewGame()
    {
        Reset();
        await Shell.Current.GoToAsync("LobbyPage");
    }
}
public class Attempt(string att, int no, int pos)
{
    public string Att { get; set; } = att;
    public int No { get; set; } = no;
    public int Pos { get; set; } = pos;
    public static (int no,int pos) Check(string guess,string actual)
    {
        int no = 0;
        int pos = 0;
        for (int i = 0; i < 4; i++)
        {
            var a = actual[i];
            var g = guess[i];
            if (guess.Distinct().Contains(a))
                no++;
            if (a == g)
                pos++;
        }
        return (no, pos);
    }
}
