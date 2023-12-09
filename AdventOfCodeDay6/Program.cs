﻿/*
 * Advent Of Code 2023
 * Day 6
 * Part 1 & 2
 * AdventOfCodeDay6.csproj
 * By DaWeba02 / Markus Weber
 *
 * Created on 09 Dec 2023
 *
 * Instructions see: https://adventofcode.com/2023/day/5
 */

internal class Program
{
    private static (int time, int distance)[] races;
    private static (long time, long distance) race;

    public static void Main()
    {
        string[] lines = File.ReadLines(@"./input.txt").ToArray();

        ParseInput(lines);

        int p1 = 1;

        foreach ((int time, int distance) in races)
            p1 *= CountWaysToWin(time, distance);

        int p2 = CountWaysToWin(race.time, race.distance);

        string result = $"Part 1: {p1} | Part 2: {p2}";
        Console.WriteLine(result);
    }

    private static int CountWaysToWin(long time, long distance)
    {
        int waysToWin = 0;

        for (int holdTime = 1; holdTime < time; holdTime++)
        {
            long dist = holdTime * (time - holdTime);

            if (dist > distance)
                waysToWin++;
        }

        return waysToWin;
    }

    private static void ParseInput(string[] lines)
    {
        string[] timesStr = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] distancesStr = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        races = new (int, int)[timesStr.Length - 1];

        string timeStr = "";
        string distanceStr = "";

        for (int i = 1; i < timesStr.Length; i++)
        {
            races[i - 1] = (int.Parse(timesStr[i]), int.Parse(distancesStr[i]));

            timeStr += timesStr[i];
            distanceStr += distancesStr[i];
        }

        race = (long.Parse(timeStr), long.Parse(distanceStr));
    }
}