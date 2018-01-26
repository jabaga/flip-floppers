using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper {

    public static float DegreesBetweenObjects(GameObject obj1, GameObject obj2)
    {
        return DegreesBetweenPoints(obj1.transform.position.x, obj1.transform.position.y, obj2.transform.position.x, obj2.transform.position.y);
    }

    public static float DegreesBetweenPoints(float x1, float y1, float x2, float y2)
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(y1 - y2, x1 - x2);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        AngleDeg += 180;

        return AngleDeg;
    }
}
