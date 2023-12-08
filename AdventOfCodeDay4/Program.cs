/*
 * Advent Of Code 2023
 * Day 4
 * Part 1 & 2
 * AdventOfCodeDay4.csproj
 * By DaWeba02 / Markus Weber
 *
 * Created on 06 Dec 2023
 *
 * Instructions see: https://adventofcode.com/2023/day/4
 */

using System.Text;
using System.Text.RegularExpressions;

var input = await File.ReadAllLinesAsync(path: ".\\input.txt", encoding: Encoding.UTF8);

var result1 = Part1(input);
var result2 = Part2(input);

Console.WriteLine($"Part 1 Sum: {result1}");
Console.WriteLine($"Part 2 Sum: {result2}");

Console.ReadKey();

double Part1(string[] input)
{
    return input
        .Select(x => new { Line = x, Card = ParseCard(x) })
        .Where(x => x.Card.matches > 0)
        .Select(x => Math.Pow(2, x.Card.matches - 1))
        .Sum();
}

double Part2(string[] input)
{
    var cards = input
        .Select(x => ParseCard(x))
        .ToArray();

    var counts = cards
        .Select(_ => 1)
        .ToArray();

    for (var i = 0; i < cards.Length; i++)
    {
        var (card, count) = (cards[i], counts[i]);

        for (var j = 0; j < card.matches; j++)
        {
            counts[i + j + 1] += count;
        }
    }

    return counts.Sum();
}

Card ParseCard(string line)
{
    var parts = line.Split(':', '|');

    var l = Regex.Matches(parts[1], @"\d+")
        .Select(x => x.Value);
    var r = Regex.Matches(parts[2], @"\d+")
        .Select(x => x.Value);

    return new Card(l.Intersect(r).Count());
}

record Card(int matches);