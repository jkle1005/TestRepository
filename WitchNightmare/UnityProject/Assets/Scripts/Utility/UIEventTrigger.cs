using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Witch;

public class UIEventTrigger : MonoBehaviour 
{
    public void MouseEnter()
    {
        string[] id = Parser.Instance.ParseDataSingleString(gameObject.name);
        Debug.Log("UIEventTrigger::MouseEnter():: " + id[0]);

        switch (id[0])
        {

        }
    }

    public void MouseExit()
    {
        string[] id = Parser.Instance.ParseDataSingleString(gameObject.name);
        Debug.Log("UIEventTrigger::MouseExit():: " + id[0]);

        switch (id[0])
        {

        }
    }

    public void ClickDown()
    {
        string[] id = Parser.Instance.ParseDataSingleString(gameObject.name);

        Debug.Log("UIEventTrigger::ClickDown():: " + id[0]);


        switch (id[0])
        {

        }
    }

    public void ClickUp()
    {
        string[] id = Parser.Instance.ParseDataSingleString(gameObject.name);

        Debug.Log("UIEventTrigger::ClickUp():: " +  id[0]);

        switch (id[0])
        {

        }
    }

    public void Click()
    {
        string[] id = Parser.Instance.ParseDataSingleString(gameObject.name);

        Debug.Log("UIEventTrigger::Click():: " + id[0]);

        switch (id[0])
        {

        }
    }
}
