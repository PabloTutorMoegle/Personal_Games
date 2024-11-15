using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 8) * controlPoints[0].position +
                8 * Mathf.Pow(1 - t, 7) * t * controlPoints[1].position +
                28 * Mathf.Pow(1 - t, 6) * Mathf.Pow(t, 2) * controlPoints[2].position +
                56 * Mathf.Pow(1 - t, 5) * Mathf.Pow(t, 3) * controlPoints[3].position +
                70 * Mathf.Pow(1 - t, 4) * Mathf.Pow(t, 4) * controlPoints[4].position +
                56 * Mathf.Pow(1 - t, 3) * Mathf.Pow(t, 5) * controlPoints[5].position +
                28 * Mathf.Pow(1 - t, 2) * Mathf.Pow(t, 6) * controlPoints[6].position +
                8 * (1 - t) * Mathf.Pow(t, 7) * controlPoints[7].position +
                Mathf.Pow(t, 8) * controlPoints[8].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
            new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
            new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
    }
}
