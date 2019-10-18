using System.Collections.Generic;

namespace PlanningPoker.Data
{
    public enum PlanningPokerFace
    {
        Zero,
        OneHalf,
        One,
        Two,
        Three,
        Five,
        Eight,
        Thirteen,
        Twenty,
        Fourty,
        OneHundred,
        Unsure,
        Coffee,
        Infinity
    }

    public static partial class Constants
    {
        public static IDictionary<PlanningPokerFace, PlanningPokerCard> PlanningPokerFaces
        {
            get => new Dictionary<PlanningPokerFace, PlanningPokerCard>()
            {
                { PlanningPokerFace.Zero,       new PlanningPokerCard(0) },
                { PlanningPokerFace.OneHalf,    new PlanningPokerCard("&frac12;", 0.5) },
                { PlanningPokerFace.One,        new PlanningPokerCard(1) },
                { PlanningPokerFace.Two,        new PlanningPokerCard(2) },
                { PlanningPokerFace.Three,      new PlanningPokerCard(3) },
                { PlanningPokerFace.Five,       new PlanningPokerCard(5) },
                { PlanningPokerFace.Eight,      new PlanningPokerCard(8) },
                { PlanningPokerFace.Thirteen,   new PlanningPokerCard(13) },
                { PlanningPokerFace.Twenty,     new PlanningPokerCard(20) },
                { PlanningPokerFace.Fourty,     new PlanningPokerCard(40) },
                { PlanningPokerFace.OneHundred, new PlanningPokerCard(100) },
                { PlanningPokerFace.Unsure,     new PlanningPokerCard("?", -1) },
                { PlanningPokerFace.Coffee,     new PlanningPokerCard("&#x2615;", -2) },
                { PlanningPokerFace.Infinity,   new PlanningPokerCard("&infin;", double.PositiveInfinity) } 
            };
        }

    }


}
