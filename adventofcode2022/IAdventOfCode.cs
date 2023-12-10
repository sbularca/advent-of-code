public interface IAdventOfCode {
    string Url { get; }
    DateTime Now { get;}
    List<string> Results { get; }
    bool ShouldExecute { get; set; }
    void OnProcessData(string result);
    void PrintResults();
}
