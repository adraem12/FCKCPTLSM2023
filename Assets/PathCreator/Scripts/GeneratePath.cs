using PathCreation;
using UnityEngine;
using UnityEditor;
namespace PathCreation.Examples
{
    [RequireComponent(typeof(PathCreator))]
    public class GeneratePath : MonoBehaviour
    {
        public bool closedLoop = true;
        public Transform[] waypoints;
        public void GeneratePathVoid()
        {
            if (waypoints.Length > 0)
            {
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
            }
        }
    }
}