using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

namespace Zocat
{
    public static class CurvyTools
    {
        public static List<float> GetAngleList(List<Transform> list)
        {
            var listFloat = new float[list.Count];
            for (var i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    var angle = MathTools.CenterAngle(list[0], list.LastElement(), list[1]);
                    listFloat[0] = angle;
                }
                else if (i == list.Count - 1)
                {
                    var angle = MathTools.CenterAngle(list.LastElement(), list.GetFromEnd(1), list[0]);
                    listFloat[list.Count - 1] = angle;
                }
                else
                {
                    var angle = MathTools.CenterAngle(list[i], list[i - 1], list[i + 1]);
                    listFloat[i] = angle;
                }
            }

            return listFloat.ToList();
        }

        public static List<Transform> GetCPTransformList(CurvySpline curvySpline)
        {
            var tmp = new List<Transform>();
            tmp.Clear();
            foreach (var item in curvySpline.ControlPointsList)
            {
                tmp.Add(item.transform);
            }

            return tmp;
        }

        public static List<float> GetAngleList(CurvySpline curvySpline)
        {
            return GetAngleList(GetCPTransformList(curvySpline));
        }
        /*--------------------------------------------------------------------------------------*/

        public static CurvySplineSegment PositionToSegment(CurvySpline curvy, Vector3 pos)
        {
            var carTf = curvy.GetNearestPointTF(pos);
            return curvy.TFToSegment(carTf);
        }

        public static float SegmentIndexToTf(CurvySpline curvy, int index)
        {
            return curvy.ControlPointsList[index].TF;
        }

        public static void PutForward(Transform transform, CurvySpline curvy, float _plusDistance = 10, float _plusY = 1)
        {
            var tf = curvy.GetNearestPointTF(transform.position);
            var currentDistance = curvy.TFToDistance(tf);
            var targetDistance = (currentDistance + _plusDistance) % curvy.Length;
            var newTF = curvy.DistanceToTF(targetDistance);
            var newPos = curvy.Interpolate(newTF);
            var rot = Quaternion.LookRotation(curvy.GetTangent(newTF), Vector3.up);

            transform.position = newPos + new Vector3(0, _plusY, 0);
            transform.rotation = rot;
        }

        public static void NearestPosToAbsolute(Transform transform, CurvySpline curvy, SplineController splineController)
        {
            var tf = curvy.GetNearestPointTF(transform.position);
            var absoluteDistance = curvy.TFToDistance(tf);
            splineController.Position = absoluteDistance;
        }

        public static void PutToTfPoint(Transform transform, CurvySpline curvy, float _tfPoint)
        {
            var pos = curvy.Interpolate(_tfPoint);
            var rot = Quaternion.LookRotation(curvy.GetTangent(_tfPoint), Vector3.up);
            transform.position = pos + new Vector3(0, 1, 0);
            transform.rotation = rot;
        }

        public static Vector3 GetTfAngle(this CurvySpline curvy, float tf)
        {
            var rot = Quaternion.LookRotation(curvy.GetTangent(tf), Vector3.up);
            return rot.eulerAngles;
        }

        public static PosRot GetPosRot(this CurvySpline curvy, float tf)
        {
            var pos = curvy.Interpolate(tf);
            var rot = Quaternion.LookRotation(curvy.GetTangent(tf), Vector3.up).eulerAngles;
            return new PosRot(pos, rot);
        }

        public static Vector3 GetRandomPosFromSpline(this CurvySpline curvySpline)
        {
            var tf = Random.Range(0f, 1f);
            return curvySpline.Interpolate(tf);
        }

        /*--------------------------------------------------------------------------------------*/

        // public static void SetCurvySpline(List<Waypoint> waypoints, ref CurvySpline curvySpline)
        // {
        //     curvySpline.Clear(false);
        //     foreach (var item in waypoints)
        //     {
        //         curvySpline.Add(item.transform.position, Space.World);
        //     }
        // }
        //
        //
        // public static Waypoint GetNearestWaypoint(List<Waypoint> waypoints, Vector3 position)
        // {
        //     Waypoint nearestWaypoint = null;
        //     float nearestDistance = Mathf.Infinity;
        //
        //     foreach (Waypoint waypoint in waypoints)
        //     {
        //         float distance = Vector3.Distance(waypoint.Position, position);
        //         if (distance < nearestDistance)
        //         {
        //             nearestDistance = distance;
        //             nearestWaypoint = waypoint;
        //         }
        //     }
        //
        //     return nearestWaypoint;
        // }
    }
}