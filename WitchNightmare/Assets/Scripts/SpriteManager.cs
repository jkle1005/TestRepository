using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private static SpriteManager instance;
    public static SpriteManager Instance
    {
        get
        {
            if (SpriteManager.instance == null)
                SpriteManager.instance = FindObjectOfType<SpriteManager>();
            return SpriteManager.instance;
        }
    }





    private void Awake()
    {
        
    }
}
