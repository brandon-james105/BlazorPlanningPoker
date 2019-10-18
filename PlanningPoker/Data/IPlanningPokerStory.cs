namespace PlanningPoker.Data
{
    public interface IPlanningPokerStory
    {
        public string Id { get; set; }
        double Points { get; set; }
        string Text { get; set; }
    }
}