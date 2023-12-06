/*
 * Advent Of Code 2023
 * Day 2
 * Part 1 & 2
 * AdventOfCodeDay2.csproj
 * By DaWeba02 / Markus Weber
 *
 * Created on 06 Dec 2023
 *
 * Instructions see: https://adventofcode.com/2023/day/2
 */

using System.Text;
using System.Text.RegularExpressions;

var input = await File.ReadAllLinesAsync(path: ".\\input.txt", encoding: Encoding.UTF8);

var result1 = Part1(input);
var result2 = Part2(input);

Console.WriteLine($"Part 1 Sum: {result1}");
Console.WriteLine($"Part 2 Sum: {result2}");

Console.ReadKey();

int Part1(string[] input)
{
    var sum = 0;

    foreach (var inputLine in input)
    {
        var game = ParseGame(inputLine);

        if (game.red <= 12 && game.green <= 13 && game.blue <= 14)
        {
            sum += game.id;
        }
    }

    return sum;
}

int Part2(string[] input)
{
    var sum = 0;

    foreach (var inputLine in input)
    {
        var game = ParseGame(inputLine);

        sum += game.red * game.green * game.blue;
    }

    return sum;
}

const string GAME_ID_REGEX = @"Game (\d+)";
const string RED_REGEX = @"(\d+) red";
const string GREEN_REGEX = @"(\d+) green";
const string BLUE_REGEX = @"(\d+) blue";

Game ParseGame(string line)
    => new Game(
        ParseIntValues(line, GAME_ID_REGEX).First(),
        ParseIntValues(line, RED_REGEX).Max(),
        ParseIntValues(line, GREEN_REGEX).Max(),
        ParseIntValues(line, BLUE_REGEX).Max()
         );

IList<int> ParseIntValues(string line, string regex)
{
    return Regex.Matches(line, regex)
        .Select(x => int.Parse(x.Groups[1].Value))
        .ToList();
}

record Game(int id, int red, int green, int blue);