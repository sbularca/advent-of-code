namespace AdventOfCode2022 {
    public class Day6TuningTrouble : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day6Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();

        public void OnProcessData(string result) {
            Now = DateTime.Now;
            string dataStream = result;
            var message1Length = 4;
            var message2Length = 14;
            GetMarker(dataStream, message1Length);
            GetMarker(dataStream, message2Length);

            Console.WriteLine($"Day 6 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        private void GetMarker(string dataStream, int messageLength) {
            for (int i = 0; i < dataStream.Length; i++) {
                if (i > dataStream.Length - 1 - messageLength) {
                    break;
                }

                var a = 1;
                string tempString = dataStream[i].ToString();
                while (a < messageLength) {
                    tempString += dataStream[i + a];
                    a++;
                }

                if (tempString.Select(c => c).Distinct().Count() == tempString.Length) {
                    Results.Add((i + messageLength).ToString());
                    break;
                }
            }
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 6 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 6 Part {i+1} Result - Index is {Results[i]}");
            }
        }
    }
}
