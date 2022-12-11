using System.ComponentModel;
using System.Runtime.Intrinsics.X86;

namespace AdventOfCode2022 {
    public class Day9RopeBridge : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day9Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();

        private string[,] testGrid = new string[5, 6] {
            { ".", ".", ".", ".", ".", "." },
            { ".", ".", ".", ".", ".", "." },
            { ".", ".", ".", ".", ".", "." },
            { ".", ".", ".", ".", ".", "." },
            { ".", ".", ".", ".", ".", "." }
        };

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

        private List<(string dir, int value)> kvDirSetList = new();
        private List<(int x, int y)> headRecordedPos = new();
        private List<(int x, int y)> tailRecordedPos = new();
        private string[,] grid;

        public void OnProcessData(string result) {
            Now = DateTime.Now;

            //string[] lines = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            string[] lines = testLines;
            (int x, int y) startingPos = (0, 0);
            grid = GenerateStartingGrid(lines, out startingPos);
            ((int l, int r) horiz, (int u, int d) vert) direction = ((-1, 1), (-1, 1));
            ((int x, int y) H, (int x, int y) T) rope = ((startingPos.x, startingPos.y), (startingPos.x, startingPos.y));

            for(int i = 0; i < kvDirSetList.Count; i++) {
                int endPos = 0;
                switch(kvDirSetList[i].dir) {
                    case "R":
                        endPos = rope.H.y + kvDirSetList[i].value;
                        for(int j = rope.H.y; j <= endPos; j++) {
                            rope.H = (rope.H.x, j);
                            if(!headRecordedPos.Contains(rope.H)) {
                                Console.WriteLine($"{rope.H.x}, {rope.H.y}");
                                headRecordedPos.Add(rope.H);
                            }
                        }
                        rope.H = (rope.H.x, endPos);
                        break;
                    case "L":
                        endPos = rope.H.y - kvDirSetList[i].value;
                        for(int j = rope.H.y; j <= endPos; j++) {
                            rope.H = (rope.H.x, j);
                            if(!headRecordedPos.Contains(rope.H)) {
                                Console.WriteLine($"{rope.H.x}, {rope.H.y}");
                                headRecordedPos.Add(rope.H);
                            }
                        }
                        rope.H = (rope.H.x, endPos);
                        break;
                    case "U":
                        endPos = rope.H.x - kvDirSetList[i].value;
                        for(int j = rope.H.x; j <= endPos; j++) {
                            rope.H = (j, rope.H.y);
                            if(!headRecordedPos.Contains(rope.H)) {
                                Console.WriteLine($"{rope.H.x}, {rope.H.y}");
                                headRecordedPos.Add(rope.H);
                            }
                        }
                        rope.H = (endPos, rope.H.y);
                        break;
                    case "D":
                        endPos = rope.H.x + kvDirSetList[i].value;
                        for(int j = rope.H.x; j <= endPos; j++) {
                            rope.H = (j, rope.H.y);
                            if(!headRecordedPos.Contains(rope.H)) {
                                Console.WriteLine($"{rope.H.x}, {rope.H.y}");
                                headRecordedPos.Add(rope.H);
                            }
                        }
                        rope.H = (endPos, rope.H.y);
                        break;
                }
            }


            Console.WriteLine($"Day 9 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        private void Draw(List<(int x, int y)> coords) {
            Console.WriteLine(coords.Count);
            for(int i = 0; i < coords.Count; i++) {
                Console.WriteLine($"{coords[i].x}, {coords[i].y}");
                grid[coords[i].x, coords[i].y] = "X";
            }
            for(int i = 0; i < grid.GetLength(0); i++) {
                string line = string.Empty;
                for(int j = 0; j < grid.GetLength(1); j++) {
                    line += grid[i, j];
                }
                Console.WriteLine(line);
            }
        }

        private string[,] GenerateStartingGrid(string[] lines, out (int line, int col) startPosition) {
            int line1 = 0;
            int column = 0;
            int width = 0;
            int height = 0;
            bool firstHoriz = false;
            bool firstVert = false;
            int horiz = 0;
            int vert = 0;

            for(int i = 0; i < lines.Length; i++) {
                string direction = lines[i][0].ToString();
                int value = int.Parse(lines[i].Split(" ")[1]);
                kvDirSetList.Add((direction, value));
                switch(lines[i][0]) {
                    case 'R':
                        if(!firstHoriz) {
                            line1 = 1;
                            firstHoriz = true;
                        }
                        line1 += value;
                        Console.WriteLine($"R {Math.Abs(line1)}");
                        break;
                    case 'L':
                        if(!firstHoriz) {
                            line1 = -1;
                            firstHoriz = true;
                        }
                        line1 -= value;
                        Console.WriteLine($"L {Math.Abs(line1)}");
                        break;
                    case 'D':
                        if(!firstVert) {
                            column = -1;
                            firstVert = true;
                        }
                        column -= value;
                        Console.WriteLine($"D {Math.Abs(column)}");
                        break;
                    case 'U':
                        if(!firstVert) {
                            column = 1;
                            firstVert = true;
                        }
                        column += value;
                        Console.WriteLine($"U {Math.Abs(column)}");
                        break;
                }
                if(line1 < 0) {
                    horiz = (horiz < line1) ? horiz : line1;
                }
                if(column > 0) {
                    vert = (vert > column) ? vert : column;
                }

                width = (Math.Abs(line1) > width) ? Math.Abs(line1) : width;
                height = (Math.Abs(column) > height) ? Math.Abs(column) : height;
            }
            startPosition.col = Math.Abs(horiz);
            startPosition.line = Math.Abs(vert) - 1 ;

            Console.WriteLine($"The grid size is {height} x {width}. Coords are {startPosition.line}, {startPosition.col}");

            string[,] result = new string[height, width];
            for(int i = 0; i < height; i++) {
                string line = string.Empty;
                for(int j = 0; j < width; j++) {
                    result[i, j] = ".";
                    line += result[i, j];
                }
                Console.WriteLine(line);
            }
            return result;
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 9 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 9 Part {i + 1} Result - Result is {Results[i]}");
            }

            Draw(headRecordedPos);
        }
    }
}
