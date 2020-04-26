using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceshipGame.Commands
{
    public class IdleCommand : Command
    {
        public override CommandStatus Execute(Spaceship spaceship)
        {
            return new CommandStatus();
        }
    }
}
