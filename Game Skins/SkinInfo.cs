using System;

namespace Minesweeper
{
    public class SkinInfo : Attribute
    {
        public SkinInfo(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public int Cost { get; }
    }
}