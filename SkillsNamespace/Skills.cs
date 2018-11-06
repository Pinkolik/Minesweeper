namespace Minesweeper.SkillsNamespace
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
