// See https://aka.ms/new-console-template for more information

List<AOC22.Day> days = new();
bool test = false;

//days.Add(new AOC22.Day1(test));
//days.Add(new AOC22.Day2(test));
//days.Add(new AOC22.Day3(test));
//days.Add(new AOC22.Day4(test));
days.Add(new AOC22.Day5(test));

foreach (var day in days)
{
    Console.WriteLine(day.name);
    day.Part1();
    day.Part2();
}