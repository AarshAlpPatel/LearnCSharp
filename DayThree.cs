using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Program {
    public class DayThree {
        public static void runner() {
            List<string> input = new List<string>();
            using (StreamReader sr = File.OpenText(Directory.GetCurrentDirectory() + "/input/day3.txt")) {
                string? inputs = String.Empty;
                while ((inputs = sr.ReadLine()) != null) {
                    input.Add(inputs);
                }
            }

            long sum = 0;
            long instructionSum = 0;    

            foreach (string s in input) {
                var doRegex = new Regex("do\\(\\)");
                var dontRegex = new Regex("don't\\(\\)");
                var multRegex = new Regex("mul\\([0-9]+,[0-9]+\\)");

                MatchCollection multMatches = multRegex.Matches(s);
                MatchCollection dontMatches = dontRegex.Matches(s);
                MatchCollection doMatches = doRegex.Matches(s);

                // Prepending -1 and 0 to Dont and Do lists respectively to have proper init state
                // which allows all initial mult pairs seen before any do or dont to be considered valid
                List<int> dontIndices = dontMatches.Select(s => s.Index).Prepend<int>(-1).ToList<int>();
                List<int> doIndices = doMatches.Select(s => s.Index).Prepend<int>(0).ToList<int>();

                List<int> multIndices = multMatches.Select(s => s.Index).ToList<int>();
                List<string> multStrings = multMatches.Select(s => s.Value).ToList<string>();

                string newS = s;
                for (int i = 0; i < multIndices.Count; i++) {
                    int dontIndex = 0;
                    int doIndex = 0;

                    while (((dontIndex + 1) < dontIndices.Count) && (dontIndices[dontIndex + 1] < multIndices[i])) {
                        dontIndex++;
                    }
                    while (((doIndex + 1) < doIndices.Count) && (doIndices[doIndex + 1] < multIndices[i])) {
                        doIndex++;
                    }

                    if ((dontIndex < dontIndices.Count) && (doIndex < doIndices.Count) && doIndices[doIndex] > dontIndices[dontIndex]) {
                        //valid
                        newS = newS.Substring(0, multIndices[i]) + newS.Substring(multIndices[i], multStrings[i].Length).ToUpper() + newS.Substring(multIndices[i] + multStrings[i].Length);
                        instructionSum += multiplyString(multStrings[i]);
                    }
                }
                sum += getMultiplicationSum(s);
            }
            

            
            Console.WriteLine($"Total sum of garbled input: {sum}");
            Console.WriteLine($"Total sum of input with instructions: {instructionSum}");
            
            int actualInstructionSum = 0;
            foreach (string s in input) {
                actualInstructionSum += newSolution(s);
            }
            Console.WriteLine($"Actual sum of input with instructions is: {actualInstructionSum}"); 
        }

        public static int newSolution(String s) {
            string newS = "do()" + s + "don't()";
            var doDontRegex = new Regex("do\\(\\)(.*?)don't\\(\\)");
            MatchCollection validSections = doDontRegex.Matches(newS);
            
            int sum = 0;
            foreach (Match match in validSections) {
               sum += getMultiplicationSum(match.Value); 
            }

            return sum;
        }

        public static int multiplyString(String s) {
            var regex2 = new Regex("[0-9]+");
            MatchCollection numMatches = regex2.Matches(s);
            int num1 = Convert.ToInt32(numMatches.ToArray()[0].Value);    

            int num2 = Convert.ToInt32(numMatches.ToArray()[1].Value);

            return (num1 * num2);    
        }

        public static int getMultiplicationSum(String s) {
            
            int sum = 0;
            //RegEx - mul\([0-9]+,[0-9]+\)
            var regex = new Regex("mul\\([0-9]+,[0-9]+\\)");
            MatchCollection matches = regex.Matches(s);

            foreach (Match match in matches) {
                string multiplicationString = match.Value;
                sum += multiplyString(multiplicationString);
            }

            return sum;
        }
    }
}