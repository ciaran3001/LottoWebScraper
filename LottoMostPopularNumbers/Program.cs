using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace LottoMostPopularNumbers
{
    class Program
    {
        public static int position = 1;

        static List<int> PosOne = new List<int>();
        static List<int> PosTwo = new List<int>();
        static List<int> PosThree = new List<int>();
        static List<int> PosFour = new List<int>();
        static List<int> PosFive = new List<int>();
        static List<int> PosSix = new List<int>();

        static List<int> BonusNumbers = new List<int>();

        static List<int>[] ResultSets = new List<int>[8];
        

        static void Main(string[] args)
        {
            ResultSets[1] = PosOne;
            ResultSets[2] = PosTwo;
            ResultSets[3] = PosThree;
            ResultSets[4] = PosFour;
            ResultSets[5] = PosFive;
            ResultSets[6] = PosSix;
            ResultSets[7] = BonusNumbers;


            // Console.WriteLine("Looking for lotto file at C:/Lotto.html");
            //https://www.lottery.ie/draw-games/results/view?game=daily-million&draws=0

            var url = "https://www.lottery.ie/draw-games/results/view?game=daily-million&draws=0";
            var web = new HtmlWeb();
            var LottoDoc = web.Load(url);

            Console.WriteLine("Extracting numbers");
            HtmlNodeCollection nodes = LottoDoc.DocumentNode.ChildNodes;
           // var PickNums = LottoDoc.DocumentNode.Descendants().Where(node => node.Attributes["name"].Value == "picknumber");
            var PickNums = LottoDoc.DocumentNode.SelectNodes("//*[@name=\"picknumber\"]");

            Console.WriteLine(PickNums.Count() + " Numbers found.");
            ExtractNumbers(PickNums);
            Console.WriteLine("Extracted numbers");


            Console.WriteLine("First position results: " + PosOne.Count());
            Console.WriteLine("Second position results: " + PosTwo.Count());
            Console.WriteLine("Third position results: " + PosThree.Count());
            Console.WriteLine("Fourth position results: " + PosFour.Count());
            Console.WriteLine("Fifth position results: " + PosFive.Count());
            Console.WriteLine("Sixth position results: " + PosSix.Count());

            Console.WriteLine("Bonus position results: " + BonusNumbers.Count());

            Console.WriteLine("**Mode results are: " + GetMode(PosOne) + " " + GetMode(PosTwo) + " " + GetMode(PosThree) + " " + GetMode(PosFour) + " " + GetMode(PosFive) + " " + GetMode(PosSix) + "   " + GetMode(BonusNumbers));
            Console.ReadLine();
        }

        public static int? GetMode(List<int> results)
        {
            int? _tmp =
             results
             .GroupBy(x => x)
             .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
             .Select(x => (int?)x.Key)
             .FirstOrDefault();
            return _tmp;
        }
        static void ExtractNumbers(IEnumerable<HtmlNode> node)
        {
            foreach (HtmlNode n in node)
            {
                    if(position > 7)
                    {
                        position = 1;
                    }

                    ResultSets[position].Add(Int32.Parse(n.Attributes["value"].Value));
                    Console.WriteLine("Added " + n.Attributes["value"].Value);
                    position++;
              
            }
        }
    }    
}
