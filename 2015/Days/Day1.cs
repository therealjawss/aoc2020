using System.IO;
using System; 
public class Day1 {

    public static void Run(){
        var input = File.ReadAllText("1.txt");
        Console.WriteLine(input);
        int ctr = 0;
        for(int i = 0; i<input.Length; i++) {
            if (input[i] == '(') ctr++;
            else ctr--;
            if (ctr == -1){
                Console.WriteLine(i+1);
                break;
            }
        }
        Console.WriteLine(ctr);
    }
}