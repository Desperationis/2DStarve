using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In charge of swapping between the different behaviors of a mob.
/// </summary>
[System.Serializable]
[DisallowMultipleComponent]
public class AISwapper : MonoBehaviour
{
    [SerializeField]
    private MobController mobController;

    [SerializeField]
    private MobAIData AIData;

    [SerializeField]
    private List<AIBehavior> AIs = new List<AIBehavior>();

    [SerializeField]
    [ReadOnly]
    private AIBehavior currentAI = null;

    public void Start()
    {
        // Self-destruct if this is a client
        if(BoltNetwork.IsClient)
        {
            foreach(AIBehavior AI in AIs)
            {
                Destroy(AI);
            }

            Destroy(this);
        }


        // Initialize AIBehaviors.
        if (AIs.Count == 0)
        {
            Debug.LogWarningFormat("AISwapper.cs: There are no AI behaviors on {0}\'s!", name);
        }

        else
        {
            foreach (AIBehavior AI in AIs)
            {
                AI.SetDependencies(mobController, AIData);
                AI.Init();
            }
            currentAI = GetCurrentAI();
        }
    }

    void FixedUpdate()
    {
        AIBehavior newAi = GetCurrentAI();
        if (newAi != currentAI)
        {
            SwapCurrentAI(newAi);
        }

        if(currentAI != null)
        {
            currentAI.UpdateAI();
        }
    }

    public AIBehavior GetCurrentAI()
    {
        // Check for new AI's based on importance.
        foreach (AIBehavior AI in AIs)
        {
            if (AI.CheckRequirement())
            {
                if (currentAI == null) return AI;

                else if (currentAI.GetIdentifier() != AI.GetIdentifier())
                {
                    return AI;
                }

                break;
            }
        }

        return currentAI;
    }

    public void SwapCurrentAI(AIBehavior AI)
    {
        if (AI == null)
        {
            Debug.LogWarning("AISwapper.cs: Swapping out the current AI is not possible with a null value.");
        }
        else
        {
            currentAI.OnSwitchLeave();
            currentAI = AI;
            currentAI.OnSwitchEnter();
        }
    }
}
