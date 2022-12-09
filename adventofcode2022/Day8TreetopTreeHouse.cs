using System.Diagnostics;

namespace AdventOfCode2022 {
    public class Day8TreetopTreeHouse : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day8Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();

        private List<string> visibleTrees = new();
        private string[] testGroup = new [] { "30373", "25512", "65332", "33549", "35390" };
        private List<int> scenicView = new ();

        private readonly List<string> matrixResult = new();
        private string[] lines;

        public void OnProcessData(string result) {
            Now = DateTime.Now;
            lines = result.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            //lines = testGroup;
            var iterations = 0;
            for(int i = 0; i < lines.Length; i++) {
                for(int j = 0; j < lines[i].Length; j++) {
                    string entry = $"{i},{j}";
                    int treeHeight = int.Parse(lines[i][j].ToString());

                    if(i == 0 || i == lines.Length - 1 || j == 0 || j == lines[i].Length - 1) {
                        if (!visibleTrees.Contains(entry)) {
                            visibleTrees.Add(entry);
                        }
                    }

                    int visibDistance = 1;
                    for(int left = j-1; left >= 0; left--) {
                        iterations++;
                        var nextTreeHeight = int.Parse(lines[i][left].ToString());
                        if(nextTreeHeight >= treeHeight ) {
                            break;
                        }
                        if(left == 0) {
                            visibDistance--;
                            if (!visibleTrees.Contains(entry)) {
                                visibleTrees.Add(entry);
                            }
                        }
                        visibDistance++;
                    }

                    int previousVisibDistance = visibDistance;
                    visibDistance = 1;
                    for(int right = j+1; right < lines[i].Length; right++) {
                        iterations++;
                        var nextTreeHeight = int.Parse(lines[i][right].ToString());
                        if(nextTreeHeight >= treeHeight ) {
                            break;
                        }
                        if(right == lines[i].Length-1) {
                            visibDistance--;
                            if (!visibleTrees.Contains(entry)) {
                                visibleTrees.Add(entry);
                            }
                        }
                        visibDistance++;
                    }

                    previousVisibDistance *= visibDistance;
                    visibDistance = 1;
                    for(int up = i-1; up >= 0; up--) {
                        iterations++;
                        var nextTreeHeight = int.Parse(lines[up][j].ToString());
                        if(nextTreeHeight >= treeHeight ) {
                            break;
                        }
                        if(up == 0) {
                            visibDistance--;
                            if (!visibleTrees.Contains(entry)) {
                                visibleTrees.Add(entry);
                            }
                        }
                        visibDistance++;
                    }

                    previousVisibDistance *= visibDistance;
                    visibDistance = 1;
                    for(int down = i+1; down < lines.Length; down++) {
                        iterations++;
                        var nextTreeHeight = int.Parse(lines[down][j].ToString());
                        if(nextTreeHeight >= treeHeight ) {
                            break;
                        }
                        if(down == lines.Length - 1) {
                            visibDistance--;
                            if (!visibleTrees.Contains(entry)) {
                                visibleTrees.Add(entry);
                            }
                        }
                        visibDistance++;
                    }
                    var scenincScore = previousVisibDistance * visibDistance;
                    //Console.WriteLine($"{i}-{j} with height {lines[i][j]} has scenic score of {scenincScore}");
                    scenicView.Add(scenincScore);
                }
            }

            scenicView.Sort();
            // TestMethod();
            //
            // Console.WriteLine($"The process took {iterations} iterations");
            Console.WriteLine($"Day 8 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");

            Results.Add(visibleTrees.Count.ToString());
            Results.Add(scenicView[^1].ToString());
        }

        private void TestMethod() {
            char[,] charArray = new char [lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[0].Length; j++) {
                    charArray[i, j] = lines[i][j];
                }
            }

            for (int i = 0; i < lines.Length; i++) {
                string line = string.Empty;
                for (int j = 0; j < lines[0].Length; j++) {
                    string entry = $"{i},{j}";
                    if (!visibleTrees.Contains(entry)) {
                        charArray[i, j] = '_';
                    }

                    line += charArray[i, j].ToString();
                }

                matrixResult.Add(line);
            }
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 8 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 8 Part {i + 1} Result - Result is {Results[i]}");
            }

            for(int i = 0; i < matrixResult.Count; i++) {
                Console.WriteLine(matrixResult[i]);
            }
        }
    }
}
