using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;


public static class UtilsMethods
{
    private static GameObject descendant = null;
    public static bool ContainsLayer(LayerMask mask, int layer)
    {
         return (mask.value & 1 << layer) > 0;
    }

    public static float HeightToForce(float height, float gravity)
    {
        return Mathf.Sqrt(height * -2 * gravity);
    }

    public static void ResetAllAnimatorTriggers(Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
                animator.ResetTrigger(trigger.name);
        }
    }
    
    public static Vector3 GetDirecton(Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }
    
    public static Vector3 GetInitialThrowVelocity(Vector3 posInitial, Vector3 posFinal, float angle, float gravity)
    {
        Vector3 l_DeltaPos = posFinal - posInitial;
        Vector3 l_XZDelta = l_DeltaPos;
        l_XZDelta.y = 0f;
        Vector3 l_ThrowDir = Quaternion.LookRotation(l_XZDelta) * Quaternion.AngleAxis(-angle, Vector3.right) * Vector3.forward;

        float l_Time = Mathf.Sqrt((l_ThrowDir.y * l_DeltaPos.x / l_ThrowDir.x - l_DeltaPos.y) / -gravity * 2);
        float l_Speed = l_DeltaPos.x / l_ThrowDir.x / l_Time;

        if (float.IsNaN(l_Speed))
        {
            Debug.Log("Impossible Trajectory");
            return Vector3.zero;
        }
        return l_Speed * l_ThrowDir;
    }
    
    public static GameObject ReturnRepeatChild(GameObject parent, string childName)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.name == childName)
            {
                descendant = child.gameObject;
                break;
            }
            else
            {
                ReturnRepeatChild(child.gameObject, childName);
            }
        }
        return descendant;
    }

    public static T[] ReturnRepeatChildren<T>(GameObject parent, string childName) where T: Component
    {
        return parent.GetComponentsInChildren<T>().Where(t => t.name == childName).ToArray();
    }

    public static GameObject ReturnRepeatParent(GameObject child, string parentName)
    {
        GameObject parent = child.transform.parent.gameObject;
        if (parent.name == parentName) { return parent; }
        else { return ReturnRepeatParent(parent, parentName); }
    }
    
    public static Vector3 GetMiddlePosition(Vector3 posA, Vector3 posB)
    {
        Vector3 middlePos = posB - posA;
        middlePos = posA + middlePos / 2.0f;
        return middlePos;
    }
 
    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
    
    public enum RotAxis {Pitch, Yaw, Roll}
    public static Vector3 RotateVectorInAxis(Vector3 initVector, RotAxis rotDir, float angle)
    {
        Vector3 l_Rhs = rotDir switch
        {
            RotAxis.Pitch => Vector3.up,
            RotAxis.Yaw => Vector3.right,
            RotAxis.Roll => Vector3.forward,
            _ => Vector3.up
        };
        var l_Axis = Vector3.Cross(initVector, l_Rhs);
        if (l_Axis == Vector3.zero) l_Axis = Vector3.right;
        return Quaternion.AngleAxis(angle, l_Axis) * initVector;
    }
}
