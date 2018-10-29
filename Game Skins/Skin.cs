using System;
using System.Drawing;

namespace Minesweeper
{
    [Serializable]
    public abstract class Skin : IEquatable<Skin>
    {
        public virtual string SkinName { get; }
        public virtual Bitmap Field { get; }
        public virtual Bitmap Mine { get; }
        public virtual Bitmap Tile { get; }
        public virtual Bitmap Flag { get; }
        public virtual Bitmap DefaultFace { get; }
        public virtual Bitmap LostFace { get; }
        public virtual Bitmap WonFace { get; }
        public virtual string WinMessage { get; }
        public virtual string LostMessage { get; }
        public virtual Color TextBrushColor { get; }

        public bool Equals(Skin other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(SkinName, other.SkinName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Skin) obj);
        }

        public override int GetHashCode()
        {
            return SkinName != null ? SkinName.GetHashCode() : 0;
        }
    }
}