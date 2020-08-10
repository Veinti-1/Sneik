using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneik3
{
    class Segmento : IEquatable<Segmento>
    {
        public int x { get; set; }
        public int y { get; set; }
        public int xA { get; set; }
        public int yA { get; set; }

        public bool Equals(Segmento other)
        {
            if (x == other.x && y == other.y)
            {
                return true;
            }
            return false;
        }
    }
}
