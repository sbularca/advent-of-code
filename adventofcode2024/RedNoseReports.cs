using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RedNoseReports : IAdventOfCode {
    private List<List<int>> reports = new List<List<int>>();
    public void ExecuteInstance(string dataSource) {
        var values = dataSource.Split("\n").ToList();
        foreach(var value in values) {
            var valueList = value.Split(' ').Select(int.Parse).ToList();
            reports.Add(valueList);
        }
        for(int i = 0; i < reports.Count; i++) {
            var tempReport = reports[i];
            for(var j = 0; j < tempReport.Count; j++) {
                if(tempReport[1] > tempReport[0]) {
                    reports[i] = InvertList(tempReport);
                }
            }
        }
    }

    private List<int> InvertList(List<int> report) {
        return report.AsEnumerable().Reverse().ToList();
    }

    public void PrintResults() {
        Console.WriteLine(GetType().Name);
        Console.WriteLine("\n");
    }

}
