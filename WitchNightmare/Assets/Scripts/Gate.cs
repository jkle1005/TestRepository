using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private  Field  field;

    public  Field       out_field;
    public  Vector3Int  out_position;

    private void ActiveGate()
    {
        FieldManager.Instance.OpenField(out_field);
        Player.Instance.SetPosition(out_position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "hitBox_player")
        {
            ActiveGate();
        }
    }
}
