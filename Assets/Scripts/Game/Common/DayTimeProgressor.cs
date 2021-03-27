using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sends events to progress the day.
/// </summary>
public class DayTimeProgressor : MonoBehaviour
{
    private Color32 day = new Color32(255, 255, 255, 255);
    private Color32 night = new Color32(225, 201, 255, 255);

    float sendTimer = 0.0f;

    private void ChangeDaylightColor(Color32 color)
    {
        if (BoltNetwork.IsServer)
        {
            DayTimeChange evnt = DayTimeChange.Create();
            evnt.color = color;
            evnt.Send();
        }
    }

    private void Update()
    {
        if(BoltNetwork.IsServer)
        {
            if(Time.realtimeSinceStartup > sendTimer) {

                // 300 seconds night->day and 300 seconds day->night
                float x = Time.realtimeSinceStartup;
                float t = (Mathf.Sin((x / 95.4f) - (Mathf.PI / 2.0f)) / 2.0f) + .5f;
                Color32 intermediate = Color32.Lerp(day, night, t);

                ChangeDaylightColor(intermediate);
                sendTimer = Time.realtimeSinceStartup + 1.0f;
            }
        }
    }
}
