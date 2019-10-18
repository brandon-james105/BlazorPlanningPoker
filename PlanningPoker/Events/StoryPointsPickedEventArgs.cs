using System;

namespace PlanningPoker.Events
{
    internal class StoryPointsPickedEventArgs : EventArgs
    {
        public string SessionId { get; set; }
        public int StoryIndex { get; set; }
        public double StoryPoints { get; set; }

        public StoryPointsPickedEventArgs(string id, int storyIndex, double storyPoints)
        {
            SessionId = id;
            StoryIndex = storyIndex;
            StoryPoints = storyPoints;
        }
    }
}