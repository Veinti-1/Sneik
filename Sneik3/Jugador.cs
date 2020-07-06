using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneik3
{
    public class Jugador : IComparable<Jugador>
    {
        public string name;
        public int score = 0;

        public int CompareTo(Jugador other)
        {
            return other.score.CompareTo(score);
        }
    }
}
