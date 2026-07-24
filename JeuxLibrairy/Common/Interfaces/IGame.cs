using JeuxLibrairy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLibrairy.Common.Interfaces
{
    public interface IGame
    {
        GameState State { get; }
        Player? Winner { get; }
        Player TurnTo {  get; }
    }
}
