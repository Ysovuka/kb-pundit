using System;

namespace Pundit.Harbinger.Commands
{
    public class CommandEventArgs : EventArgs
    {
        public CommandEventArgs(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
    }
}
