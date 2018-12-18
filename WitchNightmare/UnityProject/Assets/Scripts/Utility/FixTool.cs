using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTool : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        Vector3 pos = transform.position;
        pos = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
        transform.position = pos;
    }
#endif
}
