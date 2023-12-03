/*
 * Advent Of Code 2023
 * Day 1
 * Part 1
 * AdventOfCodeDay1.csproj
 * Part1.cs
 * By DaWeba02 / Markus Weber
 *
 * Created on 03 Dec 2023
 *
 * Instruction: ... On each line, the calibration value can be found by combining the first digit and the last digit (in that order) to form a single two-digit number ...
 *
 * Description: Gets the first digit and the last digit of the lines and sums them all up. If there is only one digit, e.g. 7, it counts as 77.
 */

using System.Text;

// Get input from files
var input = await File.ReadAllLinesAsync(path: ".\\input.txt", encoding: Encoding.UTF8);

// Init sum var
var sum = 0;

// Iterate through all lines and get first + last digit
// Lastly, build up number + count to sum
foreach (var inputLine in input)
{
    var firstDigit = inputLine.FirstOrDefault(x => char.IsDigit(x));
    var lastDigit = inputLine.LastOrDefault(x => char.IsDigit(x));

    sum += int.Parse($"{firstDigit}{lastDigit}", System.Globalization.NumberStyles.Integer);
}

// Print sum
Console.WriteLine($"Sum: {sum}");
Console.ReadKey();