using System;

namespace Minesweeper.Skins
{
    public class SkinInfo : Attribute
    {
        public SkinInfo(int id, string name, int cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }

        public int Id { get; }
        public string Name { get; }
        public int Cost { get; }
    }
}