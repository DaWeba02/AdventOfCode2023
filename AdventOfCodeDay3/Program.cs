/*
 * Advent Of Code 2023
 * Day 3
 * Part 1 & 2
 * AdventOfCodeDay3.csproj
 * By DaWeba02 / Markus Weber
 *
 * Created on 06 Dec 2023
 *
 * Instructions see: https://adventofcode.com/2023/day/3
 */

using System.Text;
using System.Text.RegularExpressions;

// Get input from files
var input = await File.ReadAllLinesAsync(path: ".\\input.txt", encoding: Encoding.UTF8);

var result1 = Part1(input);
var result2 = Part2(input);

Console.WriteLine($"Part 1 Sum: {result1}");
Console.WriteLine($"Part 2 Sum: {result2}");

Console.ReadKey();

int Part1(string[] input)
{
    var symbols = Parse(input.ToList(), new Regex(@"[^.0-9]"));
    var nums = Parse(input.ToList(), new Regex(@"\d+"));

    return nums
        .Where(x => symbols.Any(y => Adjavent(y, x)))
        .Select(x => x.Int)
        .Sum();
}

int Part2(string[] input)
{
    var gears = Parse(input.ToList(), new Regex(@"\*"));
    var numbers = Parse(input.ToList(), new Regex(@"\d+"));

    return gears
    .Select(g => new
    {
        Gear = g,
        Neighbours = numbers.Where(n => Adjavent(n, g)).Select(n => n.Int)
    })
    .Where(x => x.Neighbours.Count() == 2)
    .Select(x => x.Neighbours.First() * x.Neighbours.Last())
    .Sum();
}

bool Adjavent(Part p1, Part p2)
    => Math.Abs(p2.irow - p1.irow) <= 1
    && p1.icol <= p2.icol + p2.text.Length
    && p2.icol <= p1.icol + p1.text.Length;

IList<Part> Parse(List<string> rows, Regex regex)
{
    return Enumerable.Range(0, rows.Count)
                       .SelectMany(x => regex.Matches(rows[x]).Cast<Match>(),
                                   (x, y) => new Part(y.Value, x, y.Index))
                       .ToList();
}

record Part(string text, int irow, int icol)
{
    public int Int => int.Parse(text);
}