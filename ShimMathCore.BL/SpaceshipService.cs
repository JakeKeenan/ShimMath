using System;
using System.Collections.Generic;
using System.Text;
using ShimMath.DTO;
using ShimMathCore.Repository;

namespace ShimMathCore.BL
{
    public class SpaceshipService
    {
        private SpaceshipRepo SpaceshipRepo;
        public SpaceshipService(SpaceshipRepo spaceshipRepo)
        {
            SpaceshipRepo = spaceshipRepo;
        }
        public ReturnStatus AddSpaceshipBlueprint(SpaceshipBlueprint newSpaceshipBlueprint)
        {
            ReturnStatus retVal = new ReturnStatus();
            
            return retVal;
        }
        public ReturnStatus EditSpaceshipCode(string SpaceshipName, string newSourceCode)
        {
            ReturnStatus retVal = new ReturnStatus();

            return retVal;
        }
        public ReturnStatus StageSpaceshipBlueprint(string SpaceshipName)
        {
            ReturnStatus retVal = new ReturnStatus();

            return retVal;
        }
        public List<SpaceshipBlueprint> GetStagedSpaceshipBlueprints()
        {
            return new List<SpaceshipBlueprint>();
        }
        public List<SpaceshipBlueprint> GetUnstagedSpaceshipBlueprints()
        {
            return new List<SpaceshipBlueprint>();
        }
    }
}
