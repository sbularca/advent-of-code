using System.Text.RegularExpressions;

namespace AdventOfCode2022 {
    public class Day5SupplyStacks: IAdventOfCode {
        public string Url =>  "https://sebastianbularca.com/temp/data/day5Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public bool ShouldExecute { get; set; } = false;
        public void OnProcessData(string result) {
            Now = DateTime.Now;
            string[] allData = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            List<string> stacks = new();
            List<List<int>> instructions = new();

            //create the initial matrix
            for(int i = 0; i < 8; i++) {
                string stackString = allData[i].Replace("]", " ").Replace("[", " ").Remove(0, 1);
                for(int j = 1; j < stackString.Length-3; j++) {
                    stackString = stackString.Remove(j, 3);
                }
                stackString = stackString.Remove(stackString.Length - 1);
                stacks.Add(stackString);
            }

            //generate the instructions list
            for(int i = 9; i < allData.Length; i++) {
                var numbers = new Regex(@"\d+").Matches(allData[i])
                    .Select(m => Int32.Parse(m.Value)).ToList();
                instructions.Add(numbers);
            }

            // flip the matrix for easy addition
            List<List<char>> flippedStack = new();
            for(int i = 0; i < stacks[0].Length; i++) {
                flippedStack.Add(new List<char>{'-', '-', '-', '-', '-', '-', '-', '-'});
            }
            for(int i = stacks.Count - 1; i >= 0; i--) {
                for(int j = 0; j < stacks[i].Length; j++) {
                    flippedStack[j][stacks.Count - 1 - i] = stacks[i][j];
                }
            }

            //trim spaces
            for(int i = 0; i < flippedStack.Count; i++) {
                var tempList = flippedStack[i];
                for(int j = tempList.Count-1; j >= 0 ; j--) {
                    if(tempList[j] == ' ') {
                        flippedStack[i].RemoveAt(j);
                    }
                }
            }

            //convert to strings to use easier
            List<string> towerList = new();
            for(int i = 0; i < flippedStack.Count; i++) {
                string line = string.Empty;
                for(int j = 0; j < flippedStack[i].Count; j++) {
                    line += flippedStack[i][j];
                }
                //Console.WriteLine(line);
                towerList.Add(line);
            }

            // actual crates arrangement cycles for both solutions
            List<List<int>> instructions1 = new(instructions);
            List<string> towerList1 = new(towerList);
            string topCratesSingle = ArangeCrates(instructions, towerList);
            string topCratesMultiple = ArangeCrates(instructions1, towerList1, false);

            Results.Add(topCratesSingle);
            Results.Add(topCratesMultiple);

            Console.WriteLine($"Day 5 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 5 results\n");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 5 Part {i+1} Result - Total contained pairs - {Results[i]}");
            }
        }

        private string ArangeCrates(List<List<int>> list, List<string> towerList, bool singleCrates = true) {
            for (int i = 0; i < list.Count; i++) {
                var from = towerList[list[i][1] - 1];
                var to = towerList[list[i][2] - 1];
                var toMove = from.Substring(from.Length - list[i][0]);
                towerList[list[i][1] - 1] = from.Substring(0, from.Length - toMove.Length);
                if (singleCrates) {
                    toMove = ReverseString(toMove);
                }
                towerList[list[i][2] - 1] = to + toMove;
                //Console.WriteLine($"Will move {toMove} to {to} and create {towerList[list[i][2] - 1]} \n");
            }

            string topCrates = string.Empty;
            for(int i = 0; i < towerList.Count; i++) {
                topCrates += towerList[i][towerList[i].Length - 1];
            }

            return topCrates;
        }

        private static string ReverseString(string s ) {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
