using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public class RedNoseReports : IAdventOfCode {
    private List<List<int>> reports = new List<List<int>>();
    private int matches = 0;

    public void ExecuteInstance(string dataSource) {
        var values = dataSource.Split("\n").ToList();
        foreach(var value in values) {
            var valueList = value.Split(' ').Select(int.Parse).ToList();
            reports.Add(valueList);
        }

        for(int i = 0; i < reports.Count; i++) {
            var tempReport = reports[i];
            int asc = 0;
            int desc = 0;
            for(var j = 0; j < tempReport.Count - 1; j++) {
                if(tempReport[j+1] < tempReport[j]) {
                    desc++;
                } else {
                    asc++;
                }
            }

            if(desc >= asc) {
                reports[i] = InvertList(tempReport);
            }
        }

        for(var i = 0; i < reports.Count; i++) {
            bool first = true;
            for(var j = 0; j < reports[i].Count - 1; j++) {
                var diff = reports[i][j+1] - reports[i][j];
                if(diff > 3 || diff <= 0) {
                    if(first) {
                        first = false;
                        var tempReport = reports[i];
                        tempReport.RemoveAt(j+1);
                        var result = GetResult(tempReport);

                        if(result) {
                            Console.WriteLine(i);
                            matches++;
                        }
                        break;
                    }
                }

                if(j == reports[i].Count - 2) {
                    Console.WriteLine(i);
                    matches++;
                }
            }
        }
    }

    private bool GetResult(List<int> tempReport) {
        if(tempReport.Count < 2) {
            return false;
        }

        for(var k = 0; k < tempReport.Count - 1; k++) {
            var diffTemp = tempReport[k+1] - tempReport[k];
            if(diffTemp > 3 || diffTemp <= 0) {
                return false;
            }

            if(k == tempReport.Count - 2) {
                return true;
            }
        }
        return false;
    }

    private List<int> InvertList(List<int> report) {
        return report.AsEnumerable().Reverse().ToList();
    }

    public void PrintResults() {
        Console.WriteLine(GetType().Name);
        Console.WriteLine($"Matches: {matches}");
        Console.WriteLine("\n");
    }

}
