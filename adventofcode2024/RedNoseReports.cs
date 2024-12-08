public class RedNoseReports : IAdventOfCode {
    private List<List<int>> reports = new List<List<int>>();
    private int matches = 0;

    public bool ShouldRun { get; set; }
    public void ExecuteInstance(string dataSource) {
        // Parse input
        List<string> values = dataSource.Split("\n").ToList();
        foreach(string line in values) {
            if(string.IsNullOrWhiteSpace(line)) {
                continue;
            }
            List<int> valueList = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            reports.Add(valueList);
        }
        var time = DateTime.Now;
        // Process each report
        for(int i = 0; i < reports.Count; i++) {
            var report = reports[i];
            if(report.Count < 2) {
                matches++;
                continue;
            }

            bool isAsc = IsAscendent(report);
            bool fixedLine = false;
            bool firstErrorEncountered = false;

            // Check differences once
            for(int j = 0; j < report.Count - 1; j++) {
                int diff = isAsc ? report[j + 1] - report[j] : report[j] - report[j + 1];

                // Conditions: diff must be > 0 and <= 3
                if(diff <= 0 || diff > 3) {
                    if(!firstErrorEncountered) {
                        firstErrorEncountered = true;
                        if(TryFixReport(report, j - 1, isAsc) ||
                            TryFixReport(report, j, isAsc) ||
                            TryFixReport(report, j + 1, isAsc)) {
                            matches++;
                            fixedLine = true;
                        }
                        else {
                            Console.WriteLine(i + 1);
                        }

                        break;
                    }

                    Console.WriteLine(i + 1);
                    break;
                }

                if(j == report.Count - 2 && !fixedLine && !firstErrorEncountered) {
                    matches++;
                }
            }
        }
        Console.WriteLine($"Time: {(DateTime.Now - time).TotalMilliseconds}");
    }

    private bool TryFixReport(List<int> original, int removeIndex, bool isAsc) {
        if(removeIndex < 0 || removeIndex >= original.Count) return false;

        var tempReport = new List<int>(original);
        tempReport.RemoveAt(removeIndex);
        return GetResult(tempReport, isAsc);
    }

    private bool GetResult(List<int> tempReport, bool isAsc) {
        for(int k = 0; k < tempReport.Count - 1; k++) {
            int diff = isAsc ? tempReport[k + 1] - tempReport[k] : tempReport[k] - tempReport[k + 1];
            if(diff <= 0 || diff > 3) {
                return false;
            }
        }
        return true;
    }

    private bool IsAscendent(List<int> report) {
        int asc = 0;
        int desc = 0;
        for(int j = 0; j < report.Count - 1; j++) {
            if(report[j + 1] < report[j]) {
                desc++;
            }
            else {
                asc++;
            }
        }
        // If more ascending pairs than descending, consider ascending
        return asc >= desc;
    }

    public void PrintResults() {
        Console.WriteLine(GetType().Name);
        Console.WriteLine($"Matches: {matches}");
        Console.WriteLine("\n");
    }
}
