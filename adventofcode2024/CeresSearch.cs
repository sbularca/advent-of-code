using System.Text.RegularExpressions;

public class CeresSearch : IAdventOfCode {
    private const string textToSearch = "XMAS";
    private int counterPhase1 = 0;
    private int counterPhase2;
    public bool ShouldRun { get; set; } = true;
    public void ExecuteInstance(string dataSource) {
        //ExecutePhase1(dataSource);
        ExecutePhase2(dataSource);
    }

    private void ExecutePhase2(string dataSource) {
        counterPhase2 = 0;
        List<Regex> regexes = new List<Regex> {
            new(@"(?=M.S)"),
            new(@"(?=S.M)"),
            new(@"(?=S.S)"),
            new(@"(?=M.M)")
            };
        List<string> matrixLines = dataSource.Split("\n").ToList();
        for(int i = 0; i < matrixLines.Count() - 2; i++) {
            foreach(Regex regex in regexes) {
                MatchCollection matches = regex.Matches(matrixLines[i]);
                Console.WriteLine(matches.Count);
                foreach(Match match in matches) {
                    counterPhase2 += VerifyPattern(matrixLines, i, match);
                }
            }
        }
    }

    private int VerifyPattern(List<string> line, int i, Match match) {
        Regex reg1 = new(@"M.S.A.M.S");
        Regex reg2 = new(@"S.M.A.S.M");
        Regex reg3 = new(@"M.M.A.S.S");
        Regex reg4 = new(@"S.S.A.M.M");

        string line1 = line[i];
        string line2 = line[i + 1];
        string line3 = line[i + 2];
        string pattern = $"{line1[match.Index]}{line1[match.Index + 1]}{line1[match.Index + 2]}{line2[match.Index]}{line2[match.Index + 1]}{line2[match.Index + 2]}{line3[match.Index]}{line3[match.Index + 1]}{line3[match.Index + 2]}";
        Console.WriteLine(pattern);

        var match1 = reg1.Matches(pattern);
        var match2 = reg2.Matches(pattern);
        var match3 = reg3.Matches(pattern);
        var match4 = reg4.Matches(pattern);

        Console.WriteLine(pattern);

        return match1.Count + match2.Count + match3.Count + match4.Count;
    }

    private void ExecutePhase1(string dataSource) {
        counterPhase1 = 0;
        Regex xmasRegex = new(@"XMAS");
        List<string> matrixLines = dataSource.Split("\n").ToList();
        List<string> invertedLines = matrixLines.Select(x => new string(x.Reverse().ToArray())).ToList();

        List<string> matrixColumns = InvertMatrix(matrixLines);
        List<string> reversedColumns = matrixColumns.Select(x => new string(x.Reverse().ToArray())).ToList();

        List<string> diagonalLines = GetAllDiagonals(matrixLines);

        List<string> allLines = new();
        allLines.AddRange(matrixLines);
        allLines.AddRange(invertedLines);
        allLines.AddRange(matrixColumns);
        allLines.AddRange(reversedColumns);
        allLines.AddRange(diagonalLines);

        foreach(string line in allLines) {
            MatchCollection matches = xmasRegex.Matches(line);
            counterPhase1 += matches.Count;
        }
    }

    public void PrintResults() {
        Console.WriteLine($"Found {counterPhase1} instances of {textToSearch}");
        Console.WriteLine($"Found {counterPhase2} instances of M.S");
    }

    private List<string> InvertMatrix(List<string> matrixLines) {
        List<string> matrixColumns = new();

        for(int i = 0; i < matrixLines[0].Count(); i++) {
            string s = "";
            for(int j = 0; j < matrixLines.Count(); j++) {
                s += matrixLines[j][i];
            }
            matrixColumns.Add(s);
        }

        return matrixColumns;
    }

    public static List<string> GetAllDiagonals(List<string> grid) {
        List<string> diagonals = new();

        int rows = grid.Count;
        if(rows == 0) {
            return diagonals;
        }
        int cols = grid[0].Length;

        // Extract Left-to-Right diagonals (↘)
        // Diagonals are defined by (row - col) = constant
        // Minimum (row - col) could be -(cols-1), maximum could be (rows-1)
        for(int diff = -(cols - 1); diff <= rows - 1; diff++) {
            List<char> diagonalChars = new();
            for(int r = 0; r < rows; r++) {
                int c = r - diff;
                if(c >= 0 && c < cols) {
                    diagonalChars.Add(grid[r][c]);
                }
            }
            if(diagonalChars.Count > 0) {
                string diag = new(diagonalChars.ToArray());
                diagonals.Add(diag);
            }
        }

        // Extract Right-to-Left diagonals (↙)
        // Diagonals are defined by (row + col) = constant
        // Minimum (row + col) is 0, maximum (row + col) is (rows-1)+(cols-1)
        for(int sum = 0; sum <= rows - 1 + (cols - 1); sum++) {
            List<char> diagonalChars = new();
            for(int r = 0; r < rows; r++) {
                int c = sum - r;
                if(c >= 0 && c < cols) {
                    diagonalChars.Add(grid[r][c]);
                }
            }
            if(diagonalChars.Count > 0) {
                string diag = new(diagonalChars.ToArray());
                diagonals.Add(diag);
            }
        }

        // Now generate the reversed versions of all the collected diagonals
        List<string> reversedDiagonals = new();
        foreach(string d in diagonals) {
            char[] arr = d.ToCharArray();
            Array.Reverse(arr);
            reversedDiagonals.Add(new string(arr));
        }

        // Combine everything (original diagonals + reversed ones)
        diagonals.AddRange(reversedDiagonals);

        return diagonals;
    }

}
