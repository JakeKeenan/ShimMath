using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceshipGame.Commands
{
    public abstract class Command
    {
        public CommandResults CommandResults{ get;  set;}
        public abstract CommandStatus Execute(Spaceship spaceship);
    }
}
