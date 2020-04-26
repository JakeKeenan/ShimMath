using SpaceshipGame.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceshipGame
{
    public class Spaceship
    {
        public double Velocity { get; set; }
        public double Acceleration { get; set; }
        public Spaceship()
        {

        }
        
        public ReturnStatus FollowCommand(Command command)
        {
            ReturnStatus retVal = new ReturnStatus();
            CommandStatus commandStatus = command.Execute(this);
            if (commandStatus.Finished)
            {
                
                retVal.IsSuccessful = true;
            }
            else if(commandStatus.ReturnStatus.IsSuccessful)
            {
                
            }
            return retVal;
        }

        public Command DecideCommand(CommandResults commandResults)
        {
            return new IdleCommand();
        }

        
    }
}
