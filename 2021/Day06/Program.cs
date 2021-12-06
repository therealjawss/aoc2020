using ChristmasGifts;

var d = new Day6();
await d.GetInput(file: "test.txt", pattern: ",");
//await d.GetInput(pattern: ",");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await d.PostSecondAnswer();


public class Day6 : Christmas
{

    string result = "todo";
    public Day6() : base("6", "2021") { }

    public override string First()
    {
        var initialState = Input.Select(x => int.Parse(x)).ToList();
        var days = 18;

        var fishList = initialState.Select(x => x).ToList();
        for(int i= 0; i < days; i++)
        {
            var newfishList = fishList.Select(x => x==0 ? 6 : fishList.Contains(x) ? x-1 : x).ToList();
            var newFish = fishList.Count(x => x==0);
            for(int j=0; j < newFish; j++)
            {
                newfishList.Add(8);
            }
            fishList = newfishList;
        }
        
        return $"{fishList.Count()}";
    }
    public override string Second()
    {
        var fishList = Input.Select(x => short.Parse(x)).ToList();
        var days = 256;


        for (int i = 0; i < days; i++)
        {
            var fishArray = fishList.ToArray();
            var fishLookup = fishList.Select(x => x).ToHashSet();
            var newFishList = new short[fishList.Count];
            var bornFish = new List<short>();
            var goldfishMemory = new HashSet<short>();
            for (int j = 0; j<fishList.Count; j++)
            {
                var fish = fishArray[j];
                if (fish == 0)
                {
                    bornFish.Add(8);
                    newFishList[j] = 6;
                }
                else if (goldfishMemory.Contains(fishList[j])|| fishLookup.Contains(fishList[j]))
                {
                    goldfishMemory.Add(fishList[j]);
                    newFishList[j] =(short)(fish-1);
                }
                else newFishList[j] = fish;
            }

            fishList = newFishList.ToList().Concat(bornFish).ToList();
        }

        return $"{fishList.Count()}";
    }

}
