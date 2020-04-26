using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SpaceshipGame.Commands
{
    public class ThrustCommand : Command
    {
        private double duration;
        private double elapsedTime;
        public ThrustCommand(double duration)
        {
            this.duration = duration;
            elapsedTime = 0;
        }

        public override CommandStatus Execute(Spaceship spaceship)
        {
            CommandStatus retVal = new CommandStatus()
            {
                Finished = false,
                ReturnStatus = new ReturnStatus()
                {
                    IsSuccessful = true,
                },
            };
            if(spaceship == null)
            {
                retVal.ReturnStatus.IsSuccessful = false;
                retVal.ReturnStatus.ErrorMessage = "Spaceship is null";
            }
            spaceship.Velocity += spaceship.Acceleration;

            return retVal;
        }
    }
}
