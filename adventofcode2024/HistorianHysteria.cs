using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

public class HistorianHysteria : IAdventOfCode {
    private List<int> leftList = new List<int>();
    private List<int> rightList = new List<int>();
    private int resultOne = 0;
    private int resultTwo = 0;

    public void ExecuteInstance(string dataSource) {
        var lines = dataSource.Split('\n');
        foreach(var line in lines) {
            try {
            var numbers = line.Split("   ").Select(int.Parse).ToArray();
            leftList.Add(numbers[0]);
            rightList.Add(numbers[1]);
            } catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        leftList.Sort();
        rightList.Sort();

        for(int i = 0; i < leftList.Count; i++) {
            resultOne += Math.Abs(leftList[i] - rightList[i]);
        }

        foreach(int eL in leftList){
            int counter = 0;
            foreach(int eR in rightList){
                if (eL == eR){
                    counter++;
                }
            }
            var score = eL * counter;
            resultTwo += score;
        }
    }

    public void PrintResults() {
        Console.WriteLine($"Total Phase 1 = {resultOne}");
        Console.WriteLine($"Total Phase 2 = {resultTwo}");
    }
}
