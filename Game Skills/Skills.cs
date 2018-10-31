using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public enum Skills
    {
        [SkillInfo("None", 0)]
        None,
        [SkillInfo("Solve single cell", 1000)]
        SolveSingleCell,
        [SkillInfo("Solve neighbors", 5000)]
        SolveNeighbors
    }
}
