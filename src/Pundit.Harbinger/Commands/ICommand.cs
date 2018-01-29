using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Pundit.Harbinger.Commands
{
    public interface ICommand
    {
        long Id { get; }
    }
}
