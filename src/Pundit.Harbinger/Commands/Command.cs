using System;

namespace Pundit.Harbinger.Commands
{
    public abstract class Command : ICommand
    {
        public Command(long id, Version version)
        {
            Id = id;
            Version = version;
        }

        public long Id { get; private set; }
        public Version Version { get; private set; }
    }
}
