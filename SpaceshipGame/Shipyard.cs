using ShimMath.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceshipGame
{
    public class Shipyard
    {
        public Shipyard()
        {

        }
        public static Spaceship BuildSpaceship(SpaceshipBlueprint spaceshipBlueprint)
        {
            return new Spaceship();
        }
    }
}
