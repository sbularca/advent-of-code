using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace AdventOfCode2022 {
    public class Day9RopeBridge : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day9Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();

        private string[] testLines = {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };

        public void OnProcessData(string result) {
            Now = DateTime.Now;

            //string[] lines = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            string[] lines = testLines;
            ((int x, int y) H, (int x, int y)T) pos = ((0, 0), (0, 0));
            for (int i = 0; i < lines.Length; i++) {
                var data = lines[i].Split(' ');
                var dir = data[0];
                var dist = int.Parse(data[1]);
                Console.WriteLine($"Step - {dir} {dist}");
                double currentMagnitude = 0;
                switch (dir) {
                    case "R":
                        dist = pos.H.y + dist;
                        while (pos.H.y < dist) {
                            pos.H.y++;
                            currentMagnitude = Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2));
                            if(currentMagnitude > 1.42f) {
                                pos.T.y++;
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }

                        break;
                    case "L":
                        dist =  pos.H.y - dist;
                        while ( pos.H.y > dist) {
                            pos.H.y--;
                            currentMagnitude = Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2));
                            if(currentMagnitude > 1.42f) {
                                pos.T.y--;
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }

                        break;
                    case "D":
                        dist =  pos.H.x + dist;
                        while ( pos.H.x < dist) {
                            pos.H.x++;
                            currentMagnitude = Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2));
                            if(currentMagnitude > 1.42f) {
                                pos.T.x++;
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }
                        break;
                    case "U":
                        dist =  pos.H.x - dist;
                        while ( pos.H.x > dist) {
                            pos.H.x--;
                            currentMagnitude = Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2));
                            if(currentMagnitude > 1.42f) {
                                pos.T.x--;
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }
                        break;
                }
            }

            Console.WriteLine($"Day 9 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }


        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 9 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 9 Part {i + 1} Result - Result is {Results[i]}");
            }
        }
    }
}
