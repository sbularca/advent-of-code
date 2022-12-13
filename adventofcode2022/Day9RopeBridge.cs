using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace AdventOfCode2022 {
    public class Day9RopeBridge : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day9Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();

        private string[] testLines1 = {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };

        private string[] testLines2 = {
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20"
        };

        public void OnProcessData(string result) {
            Now = DateTime.Now;

            //string[] lines = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            string[] lines = testLines1;
            //string[] lines = testLines2;
            List<(int x, int y)> tailPos = new ();
            ((int x, int y) H, (int x, int y)T) pos = ((0, 0), (0, 0));
            for (int i = 0; i < lines.Length; i++) {
                var data = lines[i].Split(' ');
                var dir = data[0];
                var dist = int.Parse(data[1]);
                var ropeSize = 10;
                Console.WriteLine($"Step - {dir} {dist}");
                double currentMagnitude = 0;
                switch (dir) {
                    case "R":
                        dist =  pos.H.y + dist;
                        while ( pos.H.y < dist) {
                            pos.H.y++;
                            currentMagnitude = Math.Abs(Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2)));
                            if(currentMagnitude > 1.42f) {
                                pos.T.y++;
                                if(pos.H.x - pos.T.x > 0) {
                                    pos.T.x++;
                                }
                                else if(pos.H.x - pos.T.x < 0){
                                    pos.T.x--;
                                }
                                if(!tailPos.Contains((pos.T.x, pos.T.y))) {
                                    tailPos.Add((pos.T.x, pos.T.y));
                                }
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }
                        break;
                    case "L":
                        dist =  pos.H.y - dist;
                        while ( pos.H.y > dist) {
                            pos.H.y--;
                            currentMagnitude = Math.Abs(Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2)));
                            if(currentMagnitude > 1.42f) {
                                pos.T.y--;
                                if(pos.H.x - pos.T.x > 0) {
                                    pos.T.x++;
                                }
                                else if(pos.H.x - pos.T.x < 0){
                                    pos.T.x--;
                                }
                                if(!tailPos.Contains((pos.T.x, pos.T.y))) {
                                    tailPos.Add((pos.T.x, pos.T.y));
                                }
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }

                        break;
                    case "D":
                        dist =  pos.H.x + dist;
                        while ( pos.H.x < dist) {
                            pos.H.x++;
                            currentMagnitude = Math.Abs(Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2)));
                            if(currentMagnitude > 1.42f) {
                                pos.T.x++;
                                if(pos.H.y - pos.T.y > 0) {
                                    pos.T.y++;
                                }
                                else if(pos.H.y - pos.T.y < 0){
                                    pos.T.y--;
                                }
                                if(!tailPos.Contains((pos.T.x, pos.T.y))) {
                                    tailPos.Add((pos.T.x, pos.T.y));
                                }
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }
                        break;
                    case "U":
                        dist =  pos.H.x - dist;
                        while ( pos.H.x > dist) {
                            pos.H.x--;
                            currentMagnitude = Math.Abs(Math.Sqrt(Math.Pow((pos.H.x - pos.T.x), 2) + Math.Pow((pos.H.y - pos.T.y), 2)));
                            if(currentMagnitude > 1.42f) {
                                pos.T.x--;
                                if(pos.H.y - pos.T.y > 0) {
                                    pos.T.y++;
                                }
                                else if(pos.H.y - pos.T.y < 0){
                                    pos.T.y--;
                                }
                                if(!tailPos.Contains((pos.T.x, pos.T.y))) {
                                    tailPos.Add((pos.T.x, pos.T.y));
                                }
                            }
                            Console.WriteLine($"H - {pos.H.x}, {pos.H.y}; T - {pos.T.x}, {pos.T.y}");
                        }
                        break;
                }
            }

            Console.WriteLine($"Day 9 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");

            Results.Add((tailPos.Count+1).ToString());
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 9 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 9 Part {i + 1} Result - Result is {Results[i]}");
            }
        }
    }
}
