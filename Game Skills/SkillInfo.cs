﻿using System;

namespace Minesweeper
{
    public class SkillInfo : Attribute
    {
        public SkillInfo(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public int Cost { get; }
    }
}