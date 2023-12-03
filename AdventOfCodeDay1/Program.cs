/*
 * Advent Of Code 2023
 * Day 1
 * Part 1 & 2
 * AdventOfCodeDay1.csproj
 * By DaWeba02 / Markus Weber
 *
 * Created on 03 Dec 2023
 */

using System.Text;

// Get input from files
var input = await File.ReadAllLinesAsync(path: ".\\input.txt", encoding: Encoding.UTF8);

var result1 = Part1(input);
var result2 = Part2(input);

Console.WriteLine($"Part 1 Sum: {result1}");
Console.WriteLine($"Part 2 Sum: {result2}");

Console.ReadKey();

/*
 * Instruction: ... On each line, the calibration value can be found by combining the first digit and the last digit (in that order) to form a single two-digit number ...
 * Description: Gets the first digit and the last digit of the lines and sums them all up. If there is only one digit, e.g. 7, it counts as 77.
 */
int Part1(string[] input)
{
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

    return sum;
}

/*
 * Instruction: ... It looks like some of the digits are actually spelled out with letters: one, two, three, four, five, six, seven, eight, and nine also count as valid "digits". ...
 * Description: Gets the first digit and the last digit of the lines and sums them all up. If there is only one digit, e.g. 7, it counts as 77. Also words like 'two' -> 2 count as digits.
 */
int Part2(string[] input)
{
    // Init sum var
    var sum = 0;

    // Init digit arr
    string[] digits =
        {
            "0",    "1",   "2",   "3",     "4",    "5",    "6",   "7",     "8",     "9",
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
        };

    // Iterate through all lines and get first + last digit
    // Lastly, build up number + count to sum
    // Also account for digits as 'words'
    foreach (var inputLine in input)
    {
        var lastDigit = digits
            .Select((t, v) => (v, inputLine.LastIndexOf(t)))
            .OrderByDescending(t => t.Item2)
            .FirstOrDefault().v % 10;

        var firstDigit = digits
            .Select((t, v) => (v, inputLine.Contains(t) ? inputLine.IndexOf(t) : inputLine.Length))
            .OrderBy(t => t.Item2)
            .FirstOrDefault().v % 10;

        sum += int.Parse($"{firstDigit}{lastDigit}", System.Globalization.NumberStyles.Integer);
    }

    return sum;
}