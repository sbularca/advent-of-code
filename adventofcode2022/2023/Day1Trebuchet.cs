using System.Diagnostics;
using System.Text.RegularExpressions;

namespace adventofcode2022{
    public class Day1Trebuchet : IAdventOfCode {
        public string Url => "http://sebastianbularca.com/temp/data/2023/day1Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public bool ShouldExecute { get; set; } = true;

        private int sum;
        public void OnProcessData(string result) {
            Now = DateTime.Now;

            var lettersToNumbers = new List<string>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            string[] lines = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);

            var listCopy = lines;
            for (int i = 0; i < listCopy.Length; i++) {
                var currentLine = listCopy[i];
                for (int j = 0; j < lettersToNumbers.Count; j++) {
                    if(!currentLine.Contains(lettersToNumbers[j])) {
                        continue;
                    }

                    currentLine = currentLine.Replace(lettersToNumbers[j], (j+1).ToString());
                }
                lines[i] = currentLine;
            }

            foreach(string line in lines) {
                Console.WriteLine(line);
                string numbers = string.Concat(line.Where(char.IsDigit));
                if (numbers.Contains("0")){
                    numbers = numbers.Replace("0", "");
                }
                Console.WriteLine(numbers);
                if(numbers.Length == 1) {
                    numbers = $"{numbers[0]}{numbers[0]}";
                }

                if (numbers.Length >= 2){
                    numbers = $"{numbers[0]}{numbers[numbers.Length - 1]}";
                }

                var value = int.Parse(numbers);
                sum += value;
                Console.WriteLine(value);
            }
            Results.Add(sum.ToString());
            Console.WriteLine($"Day X OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");

        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day X results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day X Part {i + 1} Result - Result is {Results[i]}");
            }
        }
    }
}
