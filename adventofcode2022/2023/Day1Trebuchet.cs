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
            string[] linesCopy;
            Array.Copy(lines, linesCopy = new string[lines.Length], lines.Length);

            for (int i = 0; i < 2; i++) {
                sum = 0;
                if (i == 1) {
                    EvaluateTextualNumbers(ref lines, lettersToNumbers);
                }
                foreach(string line in lines) {
                    //Console.WriteLine(line);
                    string numbers = string.Concat(line.Where(char.IsDigit));
                    if (numbers.Contains("0")){
                        numbers = numbers.Replace("0", "");
                    }

                    if (numbers.Length == 0) {
                        continue;
                    }

                    //Console.WriteLine(numbers);
                    if(numbers.Length == 1) {
                        numbers = $"{numbers[0]}{numbers[0]}";
                    }

                    if (numbers.Length >= 2){
                        numbers = $"{numbers[0]}{numbers[^1]}";
                    }

                    var value = int.Parse(numbers);
                    sum += value;
                    Console.WriteLine(linesCopy[Array.IndexOf(lines, line)]);
                    Console.WriteLine(value);
                }
                Results.Add(sum.ToString());
                Console.WriteLine($"Day 1 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
            }

        }
        private void EvaluateTextualNumbers(ref string[] lines, List<string> lettersToNumbers) {
            var listCopy = lines;
            for (int i = 0; i < listCopy.Length; i++) {
                var currentLine = listCopy[i];
                var numbers = new List<string>();
                for (int j = 0; j < lettersToNumbers.Count; j++) {
                    if(!currentLine.Contains(lettersToNumbers[j])) {
                        continue;
                    }
                    numbers.Add(lettersToNumbers[j]);
                    Console.WriteLine(lettersToNumbers[j]);
                }

                if (numbers.Count == 0) {
                    continue;
                }

                string lowIndex = numbers[0];
                string highIndex = numbers[0];
                for (int j = 0; j < numbers.Count-1; j++) {
                    if(currentLine.IndexOf(lowIndex) > currentLine.IndexOf(numbers[j + 1])) {
                        lowIndex = numbers[j+1];
                    }
                    if(currentLine.IndexOf(highIndex) < currentLine.IndexOf(numbers[j + 1])) {
                        highIndex = numbers[j+1];
                    }
                }

                Console.WriteLine("low index = " + lowIndex);
                Console.WriteLine("high index = " + highIndex);

                var lowIndexIndex = currentLine.IndexOf(lowIndex);
                currentLine = currentLine.Replace(lowIndex, (lettersToNumbers.IndexOf(lowIndex) + 1).ToString());
                if (!currentLine.Contains(highIndex)) {
                    currentLine = currentLine.Insert( lowIndexIndex + 1, (lettersToNumbers.IndexOf(highIndex) + 1).ToString());
                }else {
                    currentLine = currentLine.Replace(highIndex, (lettersToNumbers.IndexOf(highIndex) + 1).ToString());
                }
                lines[i] = currentLine;
                Console.WriteLine(currentLine);
            }
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day X results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 1 Part {i + 1} Result - Result is {Results[i]}");
            }
        }
    }
}
