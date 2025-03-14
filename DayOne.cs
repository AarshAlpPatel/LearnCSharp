namespace Program {
    public class DayOne {
       public static void runner() {    
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();

            readFile(firstList, secondList);
            
            int totalDistance = getDistanceScore(firstList, secondList);
            Console.WriteLine("The total distance between both lists is: " + totalDistance);

            int simScore = getSimilarityScore(firstList, secondList);
            Console.WriteLine("The similarity score between the two lists is: " + simScore);
       }


        public static void readFile(List<int> firstList, List<int> secondList) {
            string newPath = Directory.GetCurrentDirectory() + "/input/day1.txt";

            using (StreamReader sr = File.OpenText(newPath)) {
                string? s = String.Empty;
                while ((s = sr.ReadLine()) != null) {
                    string[] locationId = s.Split("   ");
                    firstList.Add(Convert.ToInt32(locationId[0]));
                    secondList.Add(Convert.ToInt32(locationId[1]));
                }
            }

            firstList.Sort();
            secondList.Sort();
        }
       public static int getSimilarityScore(List<int> firstList, List<int> secondList) {
                
            Dictionary<int, int> secondListMap = new Dictionary<int, int>();
            foreach (int num in secondList) {
                if (secondListMap.TryGetValue(num, out int count)) {
                }
                secondListMap[num] = count + 1;
            }

            int similarityScore = 0;
            foreach (int num in firstList) {
                if (secondListMap.TryGetValue(num, out int count)) {
                    similarityScore += (num * count);
                }
            }

            return similarityScore;
       }   

        public static int getDistanceScore(List<int> firstList, List<int> secondList) {
            int i = 0;
            int totalDistance = 0;
            while (i < firstList.Count) {
                totalDistance += Math.Abs(firstList[i] - secondList[i]);
                i++;
            }

            return totalDistance;
        }
    }
}