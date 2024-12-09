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
        Regex masRegex = new(@"M.S");
        List<string> matrixLines = dataSource.Split("\n").ToList();

        for(int i = 0; i < matrixLines.Count() - 2; i++) {
            MatchCollection matches1 = masRegex.Matches(matrixLines[i]);
            MatchCollection matches2 = masRegex.Matches(matrixLines[i + 2]);
        }
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
