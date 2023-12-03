public static int[] IndexesOf(this string line, string value)
{
    return line.Select((ch, i) => line.Substring(i, value.Length + 1).StartsWith(value)  ? i : -1).Where(i => i != -1).ToArray();
}
