public interface IAdventOfCode {
    string Url { get; }
    DateTime Now { get;}
    List<int> Results { get; }
    void OnProcessData(string result);
    void PrintResults();
}
