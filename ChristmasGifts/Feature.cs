
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
using System.Xml;

public static class Feature
{
    public static bool Local = false;

    public static string NewLine => Local ? "\r\n" : "\n";
}
