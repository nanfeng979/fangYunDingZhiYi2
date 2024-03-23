using UnityEngine;

namespace Y9g
{
    public sealed class UsualCalculate
    {
        public static Vector3 Vector3ChangeX(Vector3 vector3, float x)
        {
            return new Vector3(x, vector3.y, vector3.z);
        }

        public static Vector3 Vector3ChangeY(Vector3 vector3, float y)
        {
            return new Vector3(vector3.x, y, vector3.z);
        }

        public static Vector3 Vector3ChangeZ(Vector3 vector3, float z)
        {
            return new Vector3(vector3.x, vector3.y, z);
        }

        public static bool Vector3Max(Vector3 a, Vector3 b)
        {
            return a.x >= b.x && a.y >= b.y && a.z >= b.z;
        }
    }
}