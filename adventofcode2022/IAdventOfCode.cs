public interface IAdventOfCode {
    string Url { get; }
    DateTime Now { get;}
    List<string> Results { get; }
    void OnProcessData(string result);
    void PrintResults();
}
