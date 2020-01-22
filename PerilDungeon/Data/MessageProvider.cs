using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Data
{
    public class MessageProvider : IMessageProvider
    {
        public MessageProvider()
        {
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }

        public IEnumerable<string> GetMostRecentMessages(int count)
        {
            if (count >= Messages.Count)
            {
                return Messages;
            }
            else
            {
                return Messages.TakeLast(count);
            }
        }

        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
