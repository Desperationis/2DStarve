using UnityEngine;

/// <summary>
/// Script for manually controlling physics. If physics is not
/// manually updated by the next FixedUpdate(), this script 
/// will automatically update physics. 
/// </summary>
public class PhysicsUpdater : MonoBehaviour
{
    private static bool simulatedPrior = false;

    private void Awake()
    {
        Physics2D.autoSimulation = false;
    }

    private void FixedUpdate()
    {
        if (!simulatedPrior)
        {
            Physics2D.Simulate(Time.fixedDeltaTime);
        }

        simulatedPrior = false;
    }

    public static void Simulate()
    {
        Physics2D.Simulate(Time.fixedDeltaTime);
        simulatedPrior = true;
    }
}
