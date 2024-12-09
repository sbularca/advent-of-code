using System.Text.RegularExpressions;

public class MulItOver : IAdventOfCode {
    public bool ShouldRun { get; set; }
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

        var dontIndex = dontMatches.Count > 0 ? dontMatches[0].Index : -1;
        var doIndex = doMatches.Count > 0 ? doMatches[0].Index : -1;

        var set = dontIndex > 0 ? CalculateMatches(dataSource[..dontIndex], out var m) : 0;

        Console.WriteLine($"Set: {set}");

        if(doIndex == -1) {
            finalResult += set;
            return;
        }
        var lastIndex = -1;
        if(doIndex > 0 && dontIndex > 0) {
            lastIndex = doMatches[^1].Index > dontMatches[^1].Index ? doMatches[^1].Index : dontMatches[^1].Index;
        }else if(doIndex > 0) {
            lastIndex = doMatches[^1].Index;
        } else {
            lastIndex = dontMatches[^1].Index;
        }

        while(true) {
            if (doIndex < dontIndex) {
                doIndex = doMatches.First(x => x.Index > dontIndex).Index;
            }

            if (lastIndex < doIndex ) {
                doIndex = doMatches.First(x => x.Index > dontIndex).Index;
            }

            if (dontIndex < doIndex) {
                try {
                    dontIndex = dontMatches.First(x => x.Index > doIndex).Index;
                }catch {
                    dontIndex = lastIndex;
                }
            }

            if (lastIndex < dontIndex ) {
                dontIndex = dontMatches.First(x => x.Index > doIndex).Index;
            }

            if(dontIndex > doIndex) {
                set += CalculateMatches(dataSource[doIndex..dontIndex], out var newMatches);
            }

            if(dontIndex > 0 && doIndex > dontMatches[^1].Index) {
                set += CalculateMatches(dataSource[doIndex..], out var newMatches);
                break;
            }
            // find next do indexes and index those

            if(doIndex == lastIndex || dontIndex == lastIndex) {
                break;
            }
        }

        finalResult += set;

        //CalculateMatches(dataSource, out MatchCollection matches);
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

    public void PrintResults() {
        Console.WriteLine(GetType().Name);
        Console.WriteLine($"Result: {finalResult}");
        Console.WriteLine("\n");
    }
}
