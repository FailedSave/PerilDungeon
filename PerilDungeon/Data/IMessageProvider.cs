using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Data
{
    interface IMessageProvider
    {
        public List<string> Messages { get; }
        public void AddMessage(string message);
        public IEnumerable<string> GetMostRecentMessages(int count);

        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}
