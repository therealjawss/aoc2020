using FluentAssertions;

namespace Tests;

public class Day09Tests
{

    [Fact]
    public void CanReadInput()
    {
        var input = "2333133121414131402";
        var infoList = Info.Parse(input).ToList();

        infoList.Should().NotBeEmpty();
        // You can add assertions here to validate the infoList
    }

    [Fact]
    public void CanReadOdd()
    {
        var input = "90909";
        var output = CaptureConsoleOutput(() =>
        {
            var infoList = Info.Parse(input).ToList();
            infoList.Should().NotBeEmpty();
            Console.WriteLine("hello");
            infoList.Print();
        });
    }


    [Theory]
    [InlineData("12345", "022111222")]
    [InlineData("2333133121414131402", "0099811188827773336446555566")]
    public void CanCompress(string input, string expected)
    {
        var infoList = Info.Parse(input).ToList();
        var compressed = infoList.Compress();
        compressed.Should().NotContain("."); 
        compressed.Should().Be(expected);
        // You can add assertions here to validate the compressed
    }

    [Fact]
    public void CanGetChecksum()
    {
        var input = "0099811188827773336446555566";
        var checksum = input.Checksum();

        checksum.Should().Be(1928);
    }

    private string CaptureConsoleOutput(Action action)
    {
        var stringWriter = new StringWriter();
        var originalOutput = Console.Out;
        Console.SetOut(stringWriter);

        try
        {
            action();
        }
        finally
        {
            Console.SetOut(originalOutput);
        }

        Console.WriteLine(stringWriter.ToString());
        return stringWriter.ToString();

    }
}
