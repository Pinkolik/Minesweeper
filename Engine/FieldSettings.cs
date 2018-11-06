using System;

namespace Minesweeper.Engine
{
    [Serializable]
    public class FieldSettings : IEquatable<FieldSettings>
    {
        public FieldSettings(int columns, int rows, int numberOfMines)
        {
            Columns = columns;
            Rows = rows;
            NumberOfMines = numberOfMines;
        }

        public int Columns { get; }
        public int Rows { get; }
        public int NumberOfMines { get; }

        public bool Equals(FieldSettings other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Columns == other.Columns && Rows == other.Rows && NumberOfMines == other.NumberOfMines;
        }

        public static bool operator ==(FieldSettings left, FieldSettings right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FieldSettings left, FieldSettings right)
        {
            return !left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FieldSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Columns;
                hashCode = (hashCode * 397) ^ Rows;
                hashCode = (hashCode * 397) ^ NumberOfMines;
                return hashCode;
            }
        }
    }
}