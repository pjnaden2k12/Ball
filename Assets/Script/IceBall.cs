using UnityEngine;

public class IceBall : Ball
{
    public float freezeChance = 0.05f; 

    protected override void OnHitBox(Box box)
    {
        box.ReduceHP(10);

        if (Random.value < freezeChance)
        {
            Debug.Log("ÂY CU ĐỨNG HÌNH MẤT 5S");
            box.ApplyFreeze(3); 
        }
    }
}