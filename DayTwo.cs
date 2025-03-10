using System.Net;

namespace Program {
    public class DayTwo {
        public static void runner() {
            List<string> reports = readFile();
            
            int validReports = 0;
            int validReportsOffByOne = 0;
            foreach (string report in reports) {
                List<int> levels = report.Split(' ').Select(s => Convert.ToInt32(s)).ToList<int>();
                
                if (isValid(levels)) {
                    validReports++;
                }
                if (isSafeAfterRemovingOne(levels)) {
                    validReportsOffByOne++;
                }
            }

            Console.WriteLine("The number of safe reports are: " + validReports);
            Console.WriteLine("The number of safe reports off by one are: " + validReportsOffByOne);
        }

        public static List<string> readFile() {
            List<string> reports = new List<string>();
            using (StreamReader sr = File.OpenText(Directory.GetCurrentDirectory() + "/input/day2.txt")) {
                string? s = String.Empty;
                while ((s = sr.ReadLine()) != null) {
                    reports.Add(s);
                }
            }
            return reports;
        }
        public static bool isSafeAfterRemovingOne(List<int> levels) {
            int i = 0;
            bool isSafe = false;
            for (i = 0; i < levels.Count; i++) {
                List<int> copyLevels = new List<int>(levels);
                copyLevels.RemoveAt(i);
                isSafe = isSafe || isValid(copyLevels);
                if (isSafe) break;
            }
        
            return isSafe;
        }
        public static bool isValid(List<int> levels) {
            bool isValid = true;
            bool isDecreasing = false;
            bool isIncreasing = false;

            for (int i = 1; i < levels.Count; i++) {
                if (Math.Abs(levels[i] - levels[i-1]) > 3 || Math.Abs(levels[i] - levels[i-1]) < 1) {
                    isValid = false;
                    break;
                }
                if (levels[i] > levels[i-1]) {
                    isIncreasing = true;
                } else if (levels[i] < levels[i-1]) {
                    isDecreasing = true;
                } else {
                    isValid = false;
                }
            }

            if (isIncreasing && isDecreasing) {
                isValid = false;
            }
            return isValid;
        }
    }
}