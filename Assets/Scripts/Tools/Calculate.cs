using UnityEngine;

namespace Tools
{
    public class Calculate
    {
        public static Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXZ = distance;
            distanceXZ.y = 0;

            float sy = distance.y;
            float sxz = distanceXZ.magnitude;

            float vy = sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
            float vxz = sxz / time;

            Vector3 result = distanceXZ.normalized;
            result *= vxz;
            result.y = vy;

            return result;
        }
    }
}
