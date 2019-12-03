using System;
using UnityEngine;

namespace GPWS {
    class VesselAttributes: MonoBehaviour {
        public static double RadarAltitude(Vessel v) {
            if (v != null) {
                return v.radarAltitude;
            } else {
                return double.PositiveInfinity;
            }
        }

        public static double VerticalSpeed(Vessel v) {
            if (v != null) {
                return v.verticalSpeed;
            } else {
                return 0;
            }
        }

        private static double SignedDotProduct(Vector3 v1, Vector3 v2, Vector3 upVector) {
            if (Vector3.Dot(Vector3.Cross(v1, v2), upVector) < 0) {
                return -Vector3.Angle(v1, v2);
            } else {
                return Vector3.Angle(v1, v2);
            }
        }

        private static double NormalAngle(Vector3 v1, Vector3 v2, Vector3 upVector) {
            return SignedDotProduct(Vector3.Cross(upVector, v1), Vector3.Cross(upVector, v2), upVector);
        }

        public static double AoA(Vessel v) {
            Transform rt = v.ReferenceTransform;

            return -1.0 * NormalAngle(v.GetSrfVelocity(), rt.up, rt.right);
        }

        public static double BankAngle(Vessel v) {
            Vector3d surfVector = (v.CoMD - v.mainBody.position).normalized;
            Vector3d surfParallelVector = Vector3d.Cross(surfVector, v.ReferenceTransform.up).normalized;
            double angle = Vector3d.Angle(surfParallelVector, v.ReferenceTransform.right) * Math.Sign(Vector3d.Dot(surfParallelVector, v.ReferenceTransform.forward));
            double bankAngle = Math.Abs(angle);

            return bankAngle;
        }
    }
}
