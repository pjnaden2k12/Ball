using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject prefab;

    public int Width;

    public int Height;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int i = 0; i < Height; i++)
            {
                Vector2 pos = new Vector2(x, i);
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
