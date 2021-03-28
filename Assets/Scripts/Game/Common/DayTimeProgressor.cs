using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sends events to progress the day.
/// </summary>
public class DayTimeProgressor : MonoBehaviour
{
    const float transitionDuration = 10.0f;

    private enum TIME { DAY, DUSK, NIGHT };
    private TIME pastPhase = TIME.DUSK;
    private TIME currentPhase = TIME.NIGHT;
    private float timer = 0.0f;

    private float GetPhaseDuration(TIME phase)
    {
        switch(phase) {
            case TIME.DAY:
                return 60 + transitionDuration;
            case TIME.DUSK :
                return 30 + transitionDuration;
            case TIME.NIGHT:
                return 60 + transitionDuration;
        };

        return 0;
    }

    private TIME GetNextPhase(TIME phase)
    {
        switch (phase)
        {
            case TIME.DAY:
                return TIME.DUSK;
            case TIME.DUSK:
                return TIME.NIGHT;
            case TIME.NIGHT:
                return TIME.DAY;
        };

        return TIME.DAY;
    }

    private Color32 GetPhaseColor(TIME phase)
    {
        switch (phase)
        {
            case TIME.DAY:
                return new Color32(255, 255, 255, 255);
            case TIME.DUSK:
                return new Color32(255, 227, 182, 255);
            case TIME.NIGHT:
                return new Color32(1, 1, 1, 255);
        };

        return new Color32(255, 255, 255, 255);
    }

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
            if(Time.realtimeSinceStartup > timer) {
                pastPhase = currentPhase;
                currentPhase = GetNextPhase(currentPhase);
                StartCoroutine("InterpolateTo", GetPhaseColor(currentPhase));
                timer = Time.realtimeSinceStartup + GetPhaseDuration(currentPhase);
            }
        }
    }

    /// <summary>
    /// Interpolates current Daylight color into another 
    /// over 10 seconds.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private IEnumerator InterpolateTo(Color32 color)
    {
        for (int i = 0; i < Mathf.CeilToInt(transitionDuration); i++)
        {
            Color32 original = GetPhaseColor(pastPhase);
            Color32 intermediate = Color32.Lerp(original, color, i / transitionDuration);
            ChangeDaylightColor(intermediate);

            yield return new WaitForSecondsRealtime(1);
        }

        ChangeDaylightColor(color);
    }
}
