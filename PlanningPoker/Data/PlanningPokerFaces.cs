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
        Coffee
    }

    public partial class Constants
    {
        public IDictionary<double, PlanningPokerFace> PlanningPokerFaces { get; set; }

        public Constants()
        {
            PlanningPokerFaces = new Dictionary<double, PlanningPokerFace>()
            {
                { 0, PlanningPokerFace.Zero },
                { 0.5, PlanningPokerFace.OneHalf },
                { 1, PlanningPokerFace.One },
                { 2, PlanningPokerFace.Two },
                { 3, PlanningPokerFace.Three },
                { 5, PlanningPokerFace.Five },
                { 8, PlanningPokerFace.Eight },
                { 13, PlanningPokerFace.Thirteen },
                { 20, PlanningPokerFace.Twenty },
                { 40, PlanningPokerFace.Fourty },
                { 100, PlanningPokerFace.OneHundred },
                { -1, PlanningPokerFace.Unsure },
                { -2, PlanningPokerFace.Coffee }
            };
        }
    }


}
