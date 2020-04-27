using ShimMath.DTO;
using ShimMathCore.BL;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using SpaceshipGame.Commands;

namespace SpaceshipGame
{
    public class EnvironmentRunner
    {
        private SpaceshipService spaceshipSvc;
        private List<Spaceship> Fleet { get; set; }
        private List<Thread> Captains { get; set; }
        public EnvironmentRunner(SpaceshipService spaceshipService)
        {
            spaceshipSvc = spaceshipService;
            
        }
        public void Load()
        {
            List<SpaceshipBlueprint> spaceshipBlueprints = spaceshipSvc.GetStagedSpaceshipBlueprints();
            foreach (SpaceshipBlueprint spaceshipBlueprint in spaceshipBlueprints)
            {
                Fleet.Add(Shipyard.BuildSpaceship(spaceshipBlueprint));
            }
            foreach (Spaceship spaceship in Fleet)
            {
                //make a thread for the spaceship, give the thread a spaceship object to modify at a rate.
                //spaceship.DecideCommand(new IdleCommand().Execute(spaceship));
                Captains.Add(new Thread(() => { }));
            }
        }
        public void Run()
        {
            foreach (Spaceship spaceship in Fleet)
            {
                //spaceship.RunCommand();

            }
        }
    }
}
