// See https://aka.ms/new-console-template for more information

List<AOC22.Day> days = new();
bool test = truee;

//days.Add(new AOC22.Day1(test));
//days.Add(new AOC22.Day2(test));
//days.Add(new AOC22.Day3(test));
//days.Add(new AOC22.Day4(test));
//days.Add(new AOC22.Day5(test));
//days.Add(new AOC22.Day6(test));
//days.Add(new AOC22.Day7(test));
//days.Add(new AOC22.Day8(test));
//days.Add(new AOC22.Day9(test));
//days.Add(new AOC22.Day10(test));
//days.Add(new AOC22.Day11(test));
//days.Add(new AOC22.Day12(test));
//days.Add(new AOC22.Day13(test));
days.Add(new AOC22.Day14(test));

foreach (var day in days)
{
    Console.WriteLine(day.name);
    day.Part1();
    day.Part2();
}