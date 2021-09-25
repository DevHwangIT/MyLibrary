using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.Mathematic
{
    public class BezierCurveBase : MonoBehaviour
    {
        //Fuction Test
        
        public static Vector3 Bezier(Vector3 point1, Vector3 point2, Vector3 point3, double mu)
        {
            double mum1, mum12, mu2;
            Vector3 pos = Vector3.zero;

            mu2 = mu * mu;
            mum1 = 1 - mu;
            mum12 = mum1 * mum1;

            pos.x = (float)((point1.x * mum12) +
                            (2 * point2.x * mum1 * mu) +
                            (point3.x * mu2));
            pos.y = (float)((point1.y * mum12) +
                            (2 * point2.y * mum1 * mu) +
                            (point3.y * mu2));
            pos.z = (float)((point1.z * mum12) +
                            (2 * point2.z * mum1 * mu) +
                            (point3.z * mu2));

            return pos;
        }
        
        public static Vector3 Bezier(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4,
            double mu)
        {
            double mum1, mum13, mu3;
            Vector3 pos = Vector3.zero;

            mum1 = 1 - mu;
            mum13 = mum1 * mum1 * mum1;
            mu3 = mu * mu * mu;

            pos.x = (float)((mum13 * point1.x) +
                            (3 * mu * mum1 * mum1 * point2.x) +
                            (3 * mu * mu * mum1 * point3.x) +
                            (mu3 * point4.x));
            pos.y = (float)((mum13 * point1.y) +
                            (3 * mu * mum1 * mum1 * point2.y) +
                            (3 * mu * mu * mum1 * point3.y) +
                            (mu3 * point4.y));
            pos.z = (float)((mum13 * point1.z) +
                            (3 * mu * mum1 * mum1 * point2.z) +
                            (3 * mu * mu * mum1 * point3.z) +
                            (mu3 * point4.z));

            return pos;
        }
        
        //------------------------------------------------------------------------
        
        [Range(0.01f, 1f)]
        public float gizmoRadius = 0.3f;

        [Range(0f, 1f)]
        public float progression = 0f;

        protected bool HasNull<T>(params T[] elements) where T : class
        {
            for (int i = 0; i < elements.Length; i++)
                if (elements[i] == null) return true;
            return false;
        }

        protected Vector3 Lerp(Transform a, Transform b, float t)
        {
            return Vector3.Lerp(a.position, b.position, t);
        }

        protected void DrawGizmoSphere(float radius, Transform point)
        {
            Gizmos.DrawWireSphere(point.position, radius);
        }

        protected void DrawGizmoSphere(float radius, in Vector3 point)
        {
            Gizmos.DrawWireSphere(point, radius);
        }

        protected void DrawGizmoSpheres(float radius, params Vector3[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawWireSphere(points[i], radius);
            }
        }

        protected void DrawGizmoSpheres(float radius, params Transform[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawWireSphere(points[i].position, radius);
            }
        }

        protected void DrawGizmoLine(Transform a, Transform b)
        {
            Gizmos.DrawLine(a.position, b.position);
        }

        protected void DrawGizmoLine(in Vector3 a, in Vector3 b)
        {
            Gizmos.DrawLine(a, b);
        }

        protected void DrawGizmoLines(params Vector3[] points)
        {
            int len = points.Length;
            if (len < 2) return;

            for (int i = 0; i < len - 1; i++)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }
        }

        protected void DrawGizmoLines(params Transform[] points)
        {
            int len = points.Length;
            if (len < 2) return;

            for (int i = 0; i < len - 1; i++)
            {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
        }
    }
}