namespace AdventOfCode2022 {
    public static class AdventOfCode {
        public static async Task Main(String[] args) {
            List<IAdventOfCode> instances = new();
            List<Task> tasks = new ();

            // Find automatically all interface implementations
            FindAllIAdvents(instances);

            // Execute tasks in parallel
            for(int i = 0; i < instances.Count; i++) {
                IAdventOfCode aocInstance = instances[i];
                if (!instances[i].ShouldExecute) {
                    continue;
                }
                tasks.Add(GetInputDataFromUrlAsync(aocInstance.OnProcessData, aocInstance.Url));
            }
            await Task.WhenAll(tasks);

            //print results
            for(int i = 0; i < instances.Count; i++) {
                if(!instances[i].ShouldExecute) {
                    continue;
                }
                instances[i].PrintResults();
            }
        }

        private static void FindAllIAdvents(List<IAdventOfCode> instances) {
            IEnumerable<Type> adventsOfCode = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IAdventOfCode)));
            foreach (Type type in adventsOfCode) {
                IAdventOfCode aoc = (IAdventOfCode)Activator.CreateInstance(type)!;
                instances.Add(aoc);
            }
        }

        private static async Task GetInputDataFromUrlAsync(Action<string> callback, string url) {
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
