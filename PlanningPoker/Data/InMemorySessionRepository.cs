using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public class InMemorySessionRepository : IRepository<IPlanningPokerSession>
    {
        private IDictionary<string, IPlanningPokerSession> Sessions { get; set; }

        public InMemorySessionRepository()
        {
            Sessions = new Dictionary<string, IPlanningPokerSession>();
        }

        public IPlanningPokerSession Create(IPlanningPokerSession item)
        {
            if (string.IsNullOrEmpty(item.SessionId))
            {
                item.SessionId = Guid.NewGuid().ToString();
            }

            Sessions.Add(item.SessionId, item);

            return Sessions[item.SessionId];
        }

        public bool Delete(string id)
        {
            var success = false;

            if (Sessions.ContainsKey(id))
            {
                success = Sessions.Remove(id);
            }

            return success;
        }

        public IPlanningPokerSession Find(string id)
        {
            return Sessions[id];
        }

        public IEnumerable<IPlanningPokerSession> List()
        {
            return Sessions.Values;
        }

        public IPlanningPokerSession Update(IPlanningPokerSession item)
        {
            if (Sessions.ContainsKey(item.SessionId))
            {
                Sessions[item.SessionId] = item;
            }

            return Sessions[item.SessionId];
        }
    }
}
