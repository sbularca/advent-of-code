
using System.Diagnostics;
using System.Numerics;

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
            //string[] lines = testLines1;
            string[] lines = testLines2;
            List<Vector2> tailPos = new ();
            Vector2 prevHead = Vector2.Zero;
            int lenght = 10;
            List<Vector2> rope = new List<Vector2>(lenght);
            for(int i = 0; i < lenght; i++) {
                rope.Add(Vector2.Zero);
            }

            for (int i = 0; i < lines.Length; i++) {
                var data = lines[i].Split(' ');
                var dir = data[0];
                Vector2 direction = dir switch {
                    "R" => new Vector2(0, 1),
                    "L" => new Vector2(0, -1),
                    "U" => new Vector2(-1, 0),
                    "D" => new Vector2(1,0),
                    _ => throw new ArgumentOutOfRangeException()
                };
                Console.WriteLine($"Going {dir} {data[1]}");
                for(int j = 0; j < rope.Count - 1; j++) {
                    Vector2 dist = rope[j] + direction * float.Parse(data[1]);
                    while(rope[j] != dist) {
                        prevHead = rope[j];
                        rope[j] += direction;
                        if(Vector2.Distance(rope[j], rope[j+1]) > Vector2.Distance(Vector2.One, Vector2.Zero)) {
                            rope[j+1] += (prevHead - rope[j+1]);
                            if(j+1 == rope.Count -1 && !tailPos.Contains(rope[j+1])) {
                                tailPos.Add(rope[j+1]);
                            }
                        }
                        Console.WriteLine($"x = {rope[j].X}, y = {rope[j].Y}");
                        Console.WriteLine($"x = {rope[j+1].X}, y = {rope[j+1].Y}\n");
                    }
                }

                //Console.WriteLine($"Step - {dir} {dist}");
            //
            //     double currentMagnitude = 0;
            //     switch (dir) {
            //         case "R":
            //             dist =  head.Y + dist;
            //             while ( head.Y < dist) {
            //                 head.Y++;
            //                 currentMagnitude = Math.mag Math.Abs(Math.Sqrt(Math.Pow((head.X - tail.X), 2) + Math.Pow((head.Y - tail.Y), 2)));
            //                 if(currentMagnitude > 1.42f) {
            //                     tail.Y++;
            //                     if(head.X - tail.X > 0) {
            //                         tail.X++;
            //                     }
            //                     else if(head.X - tail.X < 0){
            //                         tail.X--;
            //                     }
            //                     if(!tailPos.Contains(new (tail.X, tail.Y))) {
            //                         tailPos.Add(new(tail.X, tail.Y));
            //                     }
            //                 }
            //                 Console.WriteLine($"H - {head.X}, {head.Y}; T - {tail.X}, {tail.Y}");
            //             }
            //             break;
            //         case "L":
            //             dist =  head.Y - dist;
            //             while ( head.Y > dist) {
            //                 head.Y--;
            //                 currentMagnitude = Math.Abs(Math.Sqrt(Math.Pow((head.X - tail.X), 2) + Math.Pow((head.Y - tail.Y), 2)));
            //                 if(currentMagnitude > 1.42f) {
            //                     tail.Y--;
            //                     if(head.X - tail.X > 0) {
            //                         tail.X++;
            //                     }
            //                     else if(head.X - tail.X < 0){
            //                         tail.X--;
            //                     }
            //                     if(!tailPos.Contains(new Vector2(tail.X, tail.Y))) {
            //                         tailPos.Add(new Vector2(tail.X, tail.Y));
            //                     }
            //                 }
            //                 Console.WriteLine($"H - {head.X}, {head.Y}; T - {tail.X}, {tail.Y}");
            //             }
            //             break;
            //         case "D":
            //             dist =  head.X + dist;
            //             while ( head.X < dist) {
            //                 head.X++;
            //                 currentMagnitude = Math.Abs(Math.Sqrt(Math.Pow((head.X - tail.X), 2) + Math.Pow((head.Y - tail.Y), 2)));
            //                 if(currentMagnitude > 1.42f) {
            //                     tail.X++;
            //                     if(head.Y - tail.Y > 0) {
            //                         tail.Y++;
            //                     }
            //                     else if(head.Y - tail.Y < 0){
            //                         tail.Y--;
            //                     }
            //                     if(!tailPos.Contains(new Vector2(tail.X, tail.Y))) {
            //                         tailPos.Add(new Vector2(tail.X, tail.Y));
            //                     }
            //                 }
            //                 Console.WriteLine($"H - {head.X}, {head.Y}; T - {tail.X}, {tail.Y}");
            //             }
            //             break;
            //         case "U":
            //             dist =  head.X - dist;
            //             while ( head.X > dist) {
            //                 head.X--;
            //                 currentMagnitude = Math.Abs(Math.Sqrt(Math.Pow((head.X - tail.X), 2) + Math.Pow((head.Y - tail.Y), 2)));
            //                 if(currentMagnitude > 1.42f) {
            //                     tail.X--;
            //                     if(head.Y - tail.Y > 0) {
            //                         tail.Y++;
            //                     }
            //                     else if(head.Y - tail.Y < 0){
            //                         tail.Y--;
            //                     }
            //                     if(!tailPos.Contains(new Vector2(tail.X, tail.Y))) {
            //                         tailPos.Add(new Vector2(tail.X, tail.Y));
            //                     }
            //                 }
            //                 Console.WriteLine($"H - {head.X}, {head.Y}; T - {tail.X}, {tail.Y}");
            //             }
            //             break;
            //     }
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
