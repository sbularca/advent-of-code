using System.Net;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022 {
    public class Day1CalorieCounting : IAdventOfCode {
        private const string url = "https://sebastianbularca.com/temp/data/day1Input.txt";

        public void Start() {
            Task task = Task.Run(async () => await GetInputDataFromUrlAsync(OnProcessResult));
            while(!task.IsCompleted) {
                Task.Yield();
            }
        }

        private void OnProcessResult(string result) {
            List<int> sums = new ();
            var elvesBackpacks = result.Split(new [] {"\n\n"}, StringSplitOptions.None);
            for(int i = 0; i < elvesBackpacks.Length; i++) {
                string[] calories = elvesBackpacks[i].Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
                int sum = 0;
                for(int j = 0; j < calories.Length; j++) {
                    sum += int.Parse(calories[j]);
                }
                sums.Add(sum);
            }
            sums.Sort();
            Console.WriteLine($"The elf with the biggest muscles is carrying: {sums[^1]} calories. Such wonder!");
        }

        private async Task GetInputDataFromUrlAsync(Action<string> callback) {
            HttpClient client = new ();
            try {
                string file = await client.GetStringAsync(url);
                callback(file);
            }
            catch(HttpRequestException e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
