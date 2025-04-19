using UnityEngine;

public class FreezeEffect : MonoBehaviour
{
    private int freezeTurns = 0;

    
    public void ApplyFreeze(int turns)
    {
        freezeTurns = Mathf.Max(freezeTurns, turns);
    }

    
    public void OnEndTurn()
    {
        if (freezeTurns > 0)
            freezeTurns--;
    }

    
    public bool IsFrozen()
    {
        return freezeTurns > 0;
    }
}
