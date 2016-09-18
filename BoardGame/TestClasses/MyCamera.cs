using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BoardGame.TestClasses
{
    class MyCamera
    {
        Vector3D camPoint;
        Vector3D lookPoint;
        Vector3D upVector;
        Vector3D lastVector;

        public MyCamera(double camX, double camY, double camZ, double lookX, double lookY, double lookZ, double upX, double upY, double upZ)
        {
            camPoint = new Vector3D(camX, camY, camZ);
            lookPoint = new Vector3D(lookX, lookY, lookZ);
            upVector = new Vector3D(upX, upY, upZ);
            FixLastVector();
        }

        public void FixLastVector()
        {
            lastVector = lookPoint - camPoint;
            lastVector.Normalize();
        }

        public string PointToStr(int what)
        {
            switch (what)
            {
                case 0: return VectToStr(camPoint);
                case 1: return VectToStr(lookPoint);
                case 2: return VectToStr(upVector);
                case 3: FixLastVector(); return VectToStr(lastVector);
            }
            return String.Empty;
        }

        #region Vector3D helpers
        public string VectToStr(Vector3D v)
        {
            return String.Format("{0} {1} {2}", v.X.ToString("F"), v.Y.ToString("F"), v.Z.ToString("F")).Replace(",", ".");
        }

        private Vector3D Multiply(double[,] M, Vector3D P)
        {
            Vector3D output = new Vector3D();
            output.X = M[0, 0] * P.X + M[1, 0] * P.Y + M[2, 0] * P.Z;
            output.Y = M[0, 1] * P.X + M[1, 1] * P.Y + M[2, 1] * P.Z;
            output.Z = M[0, 2] * P.X + M[1, 2] * P.Y + M[2, 2] * P.Z;
            return output;
        }

        private Vector3D MyRotate(double angle, Vector3D axis, Vector3D? center, Vector3D source)
        {
            //Rotating point source with angle degrees around the axis that crosses point center
            //We only have rotation matrix for an axis that crosses the origin, so we need to translate the point, too
            //http://en.wikipedia.org/wiki/Rotation_matrix#Axis_of_a_rotation
            //http://en.wikipedia.org/wiki/Flight_dynamics
            Vector3D output = new Vector3D(source.X, source.Y, source.Z);

            if (center != null)
            {
                output.X -= center.Value.X;
                output.Y -= center.Value.Y;
                output.Z -= center.Value.Z;
            }

            double C = Math.Cos(angle);
            double S = Math.Sin(angle);
            double x2 = axis.X * axis.X;
            double y2 = axis.Y * axis.Y;
            double z2 = axis.Z * axis.Z;
            double xy = axis.X * axis.Y;
            double yz = axis.Y * axis.Z;
            double xz = axis.Z * axis.Z;

            double[,] M = new double[3, 3];
            M[0, 0] = x2 + (1 - x2) * C; M[1, 0] = xy * (1 - C) - axis.Z * S; M[2, 0] = xz * (1 - C) + axis.Y * S;
            M[0, 1] = xy * (1 - C) + axis.Z * S; M[1, 1] = y2 + (1 - y2) * C; M[2, 1] = yz * (1 - C) - axis.X * S;
            M[0, 2] = xz * (1 - C) - axis.Y * S; M[1, 2] = yz * (1 - C) + axis.X * S; M[2, 2] = z2 + (1 - z2) * C;

            output = Multiply(M, output);

            if (center != null)
            {
                output.X += center.Value.X;
                output.Y += center.Value.Y;
                output.Z += center.Value.Z;
            }

            return output;
        }
        #endregion

        #region Flight mode
        public void Roll(double angle)
        {
            //Rotate UpPoint around axis CamPoint->LookPoint
            Vector3D U = lookPoint - camPoint;
            U.Normalize();

            upVector = MyRotate(angle, U, null, upVector);
            upVector.Normalize();

            FixLastVector();
        }

        public void Yaw(double angle)
        {
            //Rotate LookPoint around UpPoint
            Vector3D U = new Vector3D(upVector.X, upVector.Y, upVector.Z);
            lookPoint = MyRotate(angle, U, camPoint, lookPoint);

            FixLastVector();
        }

        public void Pitch(double angle)
        {
            //Rotate LookPoint around the normalvector of UpPoint (normalvector: CamPoint->LookPoint cross UpPoint) 

            Vector3D U = camPoint - lookPoint;
            Vector3D V = Vector3D.CrossProduct(U, upVector);
            V.Normalize();

            lookPoint = MyRotate(angle, V, camPoint, lookPoint);
            upVector = MyRotate(angle, V, null, upVector);
            upVector.Normalize();

            FixLastVector();
        }

        public void Move(double distance)
        {
            //Translate CamPoint along the CamPoint->LookPoint axis
            Vector3D U = new Vector3D(lastVector.X, lastVector.Y, lastVector.Z);
            U *= distance;

            camPoint.X += U.X;
            camPoint.Y += U.Y;
            camPoint.Z += U.Z;

            lookPoint.X += U.X;
            lookPoint.Y += U.Y;
            lookPoint.Z += U.Z;

            FixLastVector();
        }

        public void Strafe(double distance)
        {
            //Translate CamPoint along the normalvector of the CamPoint->LookPoint and UpVector vectors
            Vector3D U = Vector3D.CrossProduct(lastVector, upVector);
            U.Normalize();
            U *= distance;

            camPoint.X += U.X;
            camPoint.Y += U.Y;
            camPoint.Z += U.Z;

            lookPoint.X += U.X;
            lookPoint.Y += U.Y;
            lookPoint.Z += U.Z;
        }

        public void Fly(double distance)
        {
            //Translate CamPoint along the UpPoint vector
            Vector3D U = new Vector3D(upVector.X, upVector.Y, upVector.Z);
            U *= distance;

            camPoint.X += U.X;
            camPoint.Y += U.Y;
            camPoint.Z += U.Z;

            FixLastVector();
        }
        #endregion
    }
}
