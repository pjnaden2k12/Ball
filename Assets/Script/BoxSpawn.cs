using System.Collections.Generic;
using UnityEngine;

public class BoxSpawn : MonoBehaviour
{
    public Box prefabbox;

    public int startY = 5;
    public int endY = 11;
    public int maxX = 9;
    public int turnCount = 0;

    private void Start()
    {
        float spawnChance = 0.4f;

        for (int x = 0; x < maxX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                if (Random.value < spawnChance)
                {
                    Vector2 pos = new Vector2(x, y);
                    Box newBox = Instantiate(prefabbox, pos, Quaternion.identity);
                    newBox.gameObject.SetActive(true);
                }
            }
        }
    }

    public bool DoesAnyBoxExist()
    {
        return FindObjectsOfType<Box>().Length > 0;
    }

    public bool DoesAnyBoxReachBottom()
    {
        foreach (Box box in FindObjectsOfType<Box>())
        {
            if (box.transform.position.y <= 0.05f)
            {
                return true;
            }
        }
        return false;
    }

    public void MoveBoxesDownWithFreeze()
    {
        turnCount++;
        List<Box> boxes = new List<Box>(FindObjectsOfType<Box>());
        boxes.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));

        HashSet<Vector2> frozenPositions = new HashSet<Vector2>();
        foreach (Box box in boxes)
        {
            Vector2 currentPos = box.transform.position;

            if (box.IsFrozen() || frozenPositions.Contains(currentPos + Vector2.down))
            {
                frozenPositions.Add(currentPos);
                continue;
            }

            box.transform.position += Vector3.down;
        }

        SpawnTopRow();
    }

    private void SpawnTopRow()
    {
        float spawnChance = 0.3f;
        int topY = endY - 1;

        for (int x = 0; x < maxX; x++)
        {
            if (Random.value < spawnChance)
            {
                Vector2 pos = new Vector2(x, topY);
                Box newBox = Instantiate(prefabbox, pos, Quaternion.identity);
                if (Random.value < 0.1f)
                {
                    newBox.isMutation = true;
                }

                int baseHP = 100;
                int bonusHP = Mathf.RoundToInt(turnCount * Random.Range(10f, 30f));
                int rawHP = baseHP + bonusHP;
                int finalHP = Mathf.CeilToInt(rawHP / 10f) * 10;
                newBox.hp = finalHP;

                newBox.gameObject.SetActive(true);
            }
        }
    }
}
