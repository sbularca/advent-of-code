public interface IAdventOfCode {
    public bool ShouldRun { get; set; }
    void ExecuteInstance(string dataSource);
    void PrintResults();
}

public class AdventOfCode {
    public static async Task Main(String[] args) {
        var instances = new List<IAdventOfCode>();

        var tasks = new List<Task>();
        FindAllIAdvents(ref instances);

        for(int i = 0; i < instances.Count; i++) {
            IAdventOfCode aocInstance = instances[i];
            var dataSource = GetInputData(aocInstance.GetType().Name);
            if(aocInstance.ShouldRun) {
                tasks.Add(Task.Run(() => aocInstance.ExecuteInstance(dataSource)));
            }
        }

        await Task.WhenAll(tasks);

        for(int i = 0; i < instances.Count; i++) {
            if(instances[i].ShouldRun) {
                instances[i].PrintResults();
            }
        }
    }

    private static void FindAllIAdvents(ref List<IAdventOfCode> instances) {
        IEnumerable<Type> adventsOfCode = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(IAdventOfCode)));
        foreach(Type type in adventsOfCode) {
            IAdventOfCode aoc = (IAdventOfCode)Activator.CreateInstance(type)!;
            instances.Add(aoc);
        }
    }

    private static string GetInputData(string fileName) {
        return File.ReadAllText($"data/{fileName}.dat").TrimEnd('\r', '\n');
    }
}
