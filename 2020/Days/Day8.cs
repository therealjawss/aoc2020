using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day8 : Christmas
    {
        public override int Day => 8;

        public static void Run()
        {
            Console.WriteLine("hello");
            var day = new Day8();
            day.GetInput(file: "test.txt", pattern: "\n");
            //day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
            //day.PostL1Answer();
            Task.Delay(60000);
            Console.WriteLine(day.Level2(day.Input));
            //day.PostL2Answer();
        }
        public long Accumulator { get; set; }
        public Dictionary<int, Instruction> Instructions = new Dictionary<int, Instruction>();

        public void ParseOperations()
        {
            int ctr = 0;
            foreach (var input in Input)
            {
                var line = input.Split(" ");

                Instructions.Add(ctr, parseLine(line));
                ctr++;
            }
        }

        private Instruction parseLine(string[] line)
        {
            int arg = Int32.Parse(line[1]);
            switch (line[0])
            {
                case "nop":
                    return new Instruction(Operation.nop, arg);
                case "acc":
                    return new Instruction(Operation.acc, arg);
                case "jmp": return new Instruction(Operation.jmp, arg);
                default:
                    throw new Exception();
            }
        }

        public override string Level1(string[] input)
        {
            ParseOperations();
            long accumulator = 0;
            int index = 0;
            Dictionary<int, Instruction> instructions = new Dictionary<int, Instruction>();
            int ctr = 0;
            if (ItLooped(Instructions, ref accumulator))
            {
                Console.Write("it looped");
            }

            return accumulator.ToString();
        }

        public bool ItLooped(Dictionary<int, Instruction> instructions, ref long accumulator)
        {
            int ctr = 0;
            int index = 0;
            Dictionary<int, Instruction> processed = new Dictionary<int, Instruction>();
            do
            {
                try
                {
                    processed.Add(index, Instructions[index]);

                }
                catch (Exception)
                {
                    return true;
                }
                process(Instructions, ref accumulator, ref index);
                ctr++;
            } while (processed.Count < instructions.Count && index < Instructions.Count);

            return false;

        }

        private void process(Dictionary<int, Instruction> instructions, ref long accumulator, ref int index)
        {
            var current = instructions[index];
            switch (current.operation)
            {
                case Operation.nop:
                    index++;
                    break;
                case Operation.acc:
                    accumulator += current.argument;
                    index++;
                    break;
                case Operation.jmp:
                    index += current.argument;
                    break;
                default:
                    break;

            }
        }

        public override string Level2(string[] input)
        {
            long accumulator = 0;
            Dictionary<int, Instruction> processedInstructions = new Dictionary<int, Instruction>();
            var jmpnop = Instructions.Where(x => x.Value.operation == Operation.jmp || x.Value.operation == Operation.nop);
            foreach (var op in jmpnop)
            {
                ChangeIt(Instructions, op.Key);
                accumulator = 0;
                if (ItLooped(Instructions, ref accumulator))
                {
                    ChangeIt(Instructions, op.Key);
                }
                else
                {
                    break;
                }
            }

            return accumulator.ToString();
        }

        public void ChangeIt(Dictionary<int, Instruction> instructions, int idx)
        {
            var instruction = instructions[idx];
            var ins = new Instruction(instruction.operation == Operation.jmp ? Operation.nop : instruction.operation == Operation.nop ? Operation.jmp : instruction.operation, instruction.argument);
            instructions[idx] = ins;
        }

        private Instruction ChangeIt(Dictionary<int, Instruction> instructions, int idx, int indexToSwitch)
        {
            var instruction = instructions[idx];
            if (idx == indexToSwitch)
            {


                var ins = new Instruction(instruction.operation == Operation.jmp ? Operation.nop : instruction.operation == Operation.nop ? Operation.jmp : instruction.operation, instruction.argument);
                instructions[idx] = ins;
                return ins;
            }
            else return instruction;
        }
    }

}
public record Instruction(Operation operation, int argument);
public enum Operation
{
    nop,
    acc,
    jmp
}
