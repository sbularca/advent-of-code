using System.Drawing;

namespace AdventOfCode2022 {
    public class Day7NoSpaceLeftOnDevice : IAdventOfCode {
        const int totalHdd = 70000000;
        const int minSize = 30000000;
        public string Url => "https://sebastianbularca.com/temp/data/day7Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public bool ShouldExecute { get; set; } = false;
        public void OnProcessData(string result) {
            Now = DateTime.Now;
            string[] dataStream = result.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            // we have 4 scenarios - cd, dir xxxx, 000000 filename.ext with 1 exception cd ..
            List<FolderTree> folderTree = new ();
            FolderTree currentFolder = null!;
            int depth = 0;

            for(int i = 0; i < dataStream.Length; i++) {
                string[] data = dataStream[i].Split(" ");

                if(data[1] == "ls") {
                    continue;
                }

                if(data[1] == "cd") {
                    if(data[2] == "..") {
                        depth --;
                        currentFolder = currentFolder!.parentFolder!;
                        //Console.WriteLine($"Changing to folder {currentFolder}, depth is {depth}");
                        continue;
                    }
                    depth++;
                    if (currentFolder == null) {
                        data[2] = "topFolder";
                        currentFolder = new FolderTree {
                            name = "topFolder",
                            depth = depth
                        };
                        folderTree.Add(currentFolder);
                        continue;
                    }

                    FolderTree folder = currentFolder.subfolders.FirstOrDefault(f => f.name == data[2]) ?? throw new InvalidOperationException();
                    //Console.WriteLine($"Changing to folder {data[2]}, depth is {depth}");
                    FolderTree prevFolder = currentFolder;
                    folder.depth = depth;
                    folder.parentFolder = prevFolder;
                    currentFolder = folder;
                    continue;
                }

                if(data[0] == "dir") {
                    var newFolderTree = new FolderTree {
                        name = data[1]
                    };
                    folderTree.Add(newFolderTree);
                    //Console.WriteLine($"Added folder {data[1]}");
                    currentFolder?.subfolders.Add(newFolderTree);
                    continue;
                }

                if(int.TryParse(data[0], out int size)) {
                    currentFolder?.filesSize.Add(size);
                }
            }

            int sum = 0;
            // sort folders by depth to make size calculation faster by calculating first the deepest size
            FolderTree.DepthComparer depthComparer = new ();
            folderTree.Sort(depthComparer);

            // calculate size of files and subfolders in each folder
            for(int i = 0; i < folderTree.Count; i++) {
                int size = 0;
                for(int j = 0; j < folderTree[i].filesSize.Count; j++) {
                    size += folderTree[i].filesSize[j];
                }

                for(int j = 0; j < folderTree[i].subfolders.Count; j++) {
                    size += folderTree[i].subfolders[j].size;
                }

                folderTree[i].size = size;
                //Console.WriteLine($"Folder {folderTree[i].name} with depth {folderTree[i].depth} has size {size}");

                //part 1
                if(size <= 100000) {
                    sum+=size;
                }
            }
            Results.Add(sum.ToString());

            //part 2
            FolderTree.SizeComparer sizeComparer = new ();
            folderTree.Sort(sizeComparer);

            int dif = totalHdd - folderTree[^1].size;
            //Console.WriteLine($"Total data size is {folderTree[^1].size}. Min additional free space needed is - {minSize - dif}");
            for(int i = 0; i < folderTree.Count; i++) {
                if(folderTree[i].size > (minSize - dif)) {
                    Results.Add(folderTree[i].size.ToString());
                    break;
                }
            }
            Console.WriteLine($"Day 7 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 7 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 7 Part {i+1} Result is {Results[i]}");
            }
        }

        private class FolderTree {
            public string name = string.Empty;
            public int depth;
            public int size;
            public FolderTree? parentFolder;
            public readonly List<FolderTree> subfolders = new ();
            public readonly List<int> filesSize = new ();

            public class DepthComparer : Comparer<FolderTree> {
                public override int Compare(FolderTree? x, FolderTree? y) {
                    if (x?.depth <= y?.depth) {
                        return 1;
                    }
                    return -1;
                }
            }

            public class SizeComparer : Comparer<FolderTree> {
                public override int Compare(FolderTree? x, FolderTree? y) {
                    if (x?.size >= y?.size) {
                        return 1;
                    }
                    return -1;
                }
            }
        }
    }
}
