/*
 * Advent Of Code 2023
 * Day 5
 * Part 1 & 2
 * AdventOfCodeDay5.csproj
 * By DaWeba02 / Markus Weber
 *
 * Created on 08 Dec 2023
 *
 * Instructions see: https://adventofcode.com/2023/day/5
 */

internal class Program
{
    record Entry(LongRange Source, LongRange Dest)
    {
        public static Entry Parse(string s) =>
            new(ParseInt64s(s));

        private Entry(long[] v) : this(
            Source: LongRange.FromMinLength(v[1], v[2]),
            Dest: LongRange.FromMinLength(v[0], v[2]))
        {
        }

        public long Transform(long value) =>
            value + Dest.Min - Source.Min;

        public LongRange Transform(LongRange range) =>
            Dest.Intersect((Transform(range.Min), Transform(range.Max)));
    }

    record Map(Entry[] Entries)
    {
        public static Map Parse(string s) =>
            new(s.Trim().Split('\n')[1..].Select(Entry.Parse).ToArray());

        public IEnumerable<long> Transform(long value) =>
            Entries.Where(m => m.Source.Match(value)).Select(m => m.Transform(value));

        public IEnumerable<LongRange> Transform(LongRange range) =>
            Entries.Where(m => m.Source.Match(range)).Select(m => m.Transform(range));
    }

    private static void Main()
    {
        var ss = File.ReadAllText(".\\input.txt").Split("\r\n\r\n");
        var (seeds, maps) = Parse(ss);
        Console.WriteLine(Part1(seeds, maps));
        Console.WriteLine(Part2(seeds, maps));
    }

    private static long Part1(long[] seeds, Map[] maps) =>
        seeds.Min(seed => Min(seed, maps));

    private static long Part2(long[] seeds, Map[] maps) =>
        maps.Aggregate(
                seeds.Chunk(2).Select(LongRange.FromMinLength), Transform)
            .Min(range => range.Min);

    private static long Min(long seed, Map[] maps) =>
        maps.Aggregate(seed, Min);

    private static long Min(long value, Map map) =>
        map.Transform(value).Any() ? map.Transform(value).Min() : value;

    private static IEnumerable<LongRange> Transform(IEnumerable<LongRange> ranges, Map map) =>
        ranges.SelectMany(range => Transform(range, map));

    private static IEnumerable<LongRange> Transform(LongRange range, Map map) =>
        map.Transform(range).Any() ? map.Transform(range) : new[] { range };

    private static (long[], Map[]) Parse(string[] ss) =>
        (ParseInt64s(ss[0].Split(": ")[1]), ss[1..].Select(Map.Parse).ToArray());

    private static long[] ParseInt64s(string s) =>
        s.Split(' ').Select(long.Parse).ToArray();
}

public struct LongRange : IEquatable<LongRange>
{
    public LongRange(long min, long max)
    {
        Min = min;
        Max = max;
    }

    public long Min { get; }
    public long Max { get; }
    public long Length => Max - Min + 1;

    public override readonly bool Equals(object obj) =>
        obj is LongRange other && Equals(other);

    public readonly bool Equals(LongRange other) =>
        Min == other.Min && Max == other.Max;

    public override readonly int GetHashCode() =>
        HashCode.Combine(Min, Max);

    public override readonly string ToString() =>
        $"{Min},{Max}";

    public readonly void Deconstruct(out long min, out long max)
    {
        min = Min;
        max = Max;
    }

    public readonly bool Match(long value) =>
        value >= Min && value <= Max;

    public readonly bool Match(LongRange other) =>
        other.Min <= Max && other.Max >= Min;

    public static bool Match(LongRange left, LongRange right) =>
        left.Match(right);

    public readonly LongRange Union(LongRange other) =>
        new(Math.Min(Min, other.Min), Math.Max(Max, other.Max));

    public static LongRange Union(LongRange left, LongRange right) =>
        left.Union(right);

    public readonly LongRange Intersect(LongRange other) =>
        new(Math.Max(Min, other.Min), Math.Min(Max, other.Max));

    public static LongRange Intersect(LongRange left, LongRange right) =>
        left.Intersect(right);

    public static implicit operator (long min, long max)(LongRange value) =>
        (value.Min, value.Max);

    public static implicit operator LongRange((long min, long max) value) =>
        new(value.min, value.max);

    public static LongRange FromMinLength(params long[] values) =>
        new(values[0], values[0] + values[1] - 1);

    public static bool operator ==(LongRange left, LongRange right) =>
        left.Equals(right);

    public static bool operator !=(LongRange left, LongRange right) =>
        !left.Equals(right);
}