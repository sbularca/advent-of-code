using System.Text.RegularExpressions;

public class MulItOver : IAdventOfCode {
    public bool ShouldRun { get; set; } = true;
    private int finalResult = 0;
    public void ExecuteInstance(string dataSource) {
        Regex dontRegex = new Regex(@"don't()");
        Regex doRegex = new Regex(@"do(?!n't)()");

        var dontMatches = dontRegex.Matches(dataSource);
        var doMatches = doRegex.Matches(dataSource);
        foreach(Match match in doMatches) {
            Console.WriteLine($"Found do's at index {match.Index}");
        }

        foreach(Match match in dontMatches) {
            Console.WriteLine($"Found dont's at index {match.Index}");
        }

        var dontIndex = dontMatches[0].Index;
        var doIndex = doMatches[0].Index;

        var index = 0;
        while(doIndex < dontIndex) {
            doIndex = doMatches[index].Index;
            index++;
            Console.WriteLine($"index: {index}");
        }

        Console.WriteLine($"doIndex: {doIndex}");

        var firstSet = CalculateMatches(dataSource[..dontIndex], out MatchCollection newMatches);

        for(int i = doIndex; i < dontMatches.Count; i++) {
            int nextDoIndex = 0;
            for(int j = doIndex; j < doMatches.Count; j++) {
                if(doMatches[j].Index > dontMatches[i].Index) {
                    break;
                }
            }

            int nextSet = 0;
            if(i < dontMatches.Count - 1) {
                nextSet = CalculateMatches(dataSource[nextDoIndex..dontMatches[i].Index], out MatchCollection nextMatches);
            }
            else {
                nextSet = CalculateMatches(dataSource[nextDoIndex..], out MatchCollection nextMatches);
            }

            finalResult += nextSet;
        }

        finalResult += firstSet;

        CalculateMatches(dataSource, out MatchCollection matches);
    }
    private int CalculateMatches(string dataSource, out MatchCollection matches) {
        var result = 0;
        Regex mullRegex = new Regex(@"mul\s*\(\s*(\d+)\s*,\s*(\d+)\s*\)");
        matches = mullRegex.Matches(dataSource);
        foreach(Match match in matches) {
            var index = match.Index;
            int a = int.Parse(match.Groups[1].Value);
            int b = int.Parse(match.Groups[2].Value);
            result += a * b;
        }
        return result;
    }

    private void SearchInstancesOfString(string dataSource, string pattern) {
        int startIndex = 0;
        List<int> occurrences = new List<int>();
        while((startIndex = dataSource.IndexOf(pattern, startIndex)) != -1) {
            occurrences.Add(startIndex);
            startIndex += pattern.Length;
        }

        Console.WriteLine($"Found {occurrences.Count} occurrences for -{pattern}");
    }

    public void PrintResults() {
        Console.WriteLine(GetType().Name);
        Console.WriteLine($"Result: {finalResult}");
        Console.WriteLine("\n");
    }
}
