using UnityEngine;

namespace KHJ.Core
{
    public class Coord
    {
        public int x, y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coord(float x, float y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        public Coord(Vector3 vec)
        {
            this.x = (int)vec.x;
            this.y = (int)vec.z;
        }
    }
}
