using System;
using System.Collections.Generic;
using System.Data;
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

        for(var i = 0; i < reports.Count; i++) {
            bool first = true;
            for(var j = 0; j < reports[i].Count - 1; j++) {
                var diff = IsAscendent(reports[i]) ? reports[i][j+1] - reports[i][j] : reports[i][j] - reports[i][j+1];
                if(diff > 3 || diff <= 0) {
                    if(first) {
                        first = false;
                        var tempReport = reports[i];
                        tempReport.RemoveAt(j);
                        var result = GetResult(tempReport);

                        if(result) {
                            matches++;
                            Console.WriteLine(i + 1);
                            break;
                        }
                        break;
                    }
                }

                if(j == reports[i].Count - 2) {
                    Console.WriteLine(i + 1);
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
            var diffTemp = IsAscendent(tempReport) ? tempReport[k+1] - tempReport[k] : tempReport[k] - tempReport[k+1];
            if(diffTemp > 3 || diffTemp <= 0) {
                return false;
            }

            if(k == tempReport.Count - 2) {
                return true;
            }
        }
        return false;
    }

    private bool IsAscendent(List<int> report){
        int asc = 0;
        int desc = 0;
        for(var j = 0; j < report.Count - 1; j++) {
            if(report[j + 1] < report[j]) {
                desc++;
            }
            else {
                asc++;
            }
        }

        if(desc > asc) {
            return false;
        }
        return true;
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
