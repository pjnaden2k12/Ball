using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int hp = 100;
    public Sprite normalSprite;
    public Sprite frozenSprite;
    public Sprite mutationSprite;
    public bool isMutation = false;

    private int frozenTurns = 0;
    private SpriteRenderer spriteRenderer;
    private TextMesh hpText;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpText = GetComponentInChildren<TextMesh>();
        UpdateVisual();
    }

    public void ReduceHP(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            if (isMutation)
            {
                BallShot ballShot = FindObjectOfType<BallShot>();
                if (ballShot != null)
                {
                    ballShot.GainBall();
                }
            }
            FindObjectOfType<Game>().AddScore(10);
            Destroy(gameObject);
        }
        else
        {
            UpdateVisual();
        }
    }

    public void ApplyFreeze(int turns)
    {
        frozenTurns = Mathf.Max(frozenTurns, turns);
        UpdateVisual();
    }

    public void OnEndTurn()
    {
        if (frozenTurns > 0)
        {
            frozenTurns--;
            UpdateVisual();
        }
    }

    public bool IsFrozen()
    {
        return frozenTurns > 0;
    }

    private void UpdateVisual()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        if (isMutation)
        {
            spriteRenderer.sprite = mutationSprite;
        }
        else if (frozenTurns == 3 || frozenTurns == 2)
        {
            spriteRenderer.sprite = frozenSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }

        if (hpText != null)
        {
            hpText.text = hp.ToString();
        }
    }
}