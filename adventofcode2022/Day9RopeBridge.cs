using System.ComponentModel;
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
            (int x, int y) pos = (0, 0);
            for (int i = 0; i < lines.Length; i++) {
                var data = lines[i].Split(' ');
                var dir = data[0];
                var dist = int.Parse(data[1]);
                Console.WriteLine($"Step - {dir} {dist}");
                
                switch (dir) {
                    case "R":
                        dist = pos.y + dist;
                        while (pos.y < dist) {
                            pos.y++;
                        }
                        break;
                    case "L":
                        dist = pos.y - dist;
                        while (pos.y > dist) {
                            pos.y--;
                        }
                        break;
                    case "D":
                        dist = pos.x + dist;
                        while (pos.x < dist) {
                            pos.x++;
                        }
                        break;
                    case "U":
                        dist = pos.x - dist;
                        while (pos.x > dist) {
                            pos.x--;
                        }
                        break;
                }

                Console.WriteLine($"{pos.x}, {pos.y}");
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
