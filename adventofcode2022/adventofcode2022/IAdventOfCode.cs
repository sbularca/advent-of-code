public interface IAdventOfCode {
    string Url { get; }
    DateTime Now { get;}
    void OnProcessData(string result);
}
