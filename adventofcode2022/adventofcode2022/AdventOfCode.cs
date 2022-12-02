using System;

namespace AdventOfCode2022 {
    public interface IAdventOfCode {
        void Start();
    }

    public static class AdventOfCode {
        public static void Main(String[] args) {
            Console.WriteLine("Executing Day 1");
            Day1CalorieCounting day1 = new ();
            day1.Start();
        }
    }
}
