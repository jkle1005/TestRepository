using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using XInputDotNetPure;

using Witch;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (GameManager.instance == null)
                GameManager.instance = FindObjectOfType<GameManager>();
            return GameManager.instance;
        }
    }
    // base info
    private List<Item> base_item;
    public Item[] Base_item { get { return base_item.ToArray(); } }
    private List<Tool> base_tool;
    public Tool[] Base_tool { get { return base_tool.ToArray(); } }


    // FX
    public SpriteRenderer[] fx;
    public SpriteRenderer GetFX(bool _light = true)
    {
        foreach(SpriteRenderer srr in fx)
        {
            if(!srr.gameObject.activeSelf)
            {
                if (_light)
                    srr.material = mat_light;
                else
                    srr.material = mat_nonLight;

                srr.gameObject.SetActive(true);
                return srr;
            }
        }

        Debug.Log("GameManager::GetFX() need more fx");
        return null;
    }

    

    // Camera
    public Camera camera_main;

    // Debug
    public Transform debug_window;
    private int dw_key_num;
    public void SetDebugKey(KeyCode _key, int _func)
    {
        Color color;

        if (_func == 0)
            color = Color.white;
        else
            color = new Color32(100, 100, 100, 255);
                        

        switch(_key)
        {
            case KeyCode.UpArrow:
                debug_window.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = color;                   
                break;

            case KeyCode.LeftArrow:
                debug_window.GetChild(1).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().color = color;
                break;

            case KeyCode.DownArrow:
                debug_window.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>().color = color;
                break;            

            case KeyCode.RightArrow:
                debug_window.GetChild(1).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().color = color;
                break;

            default:
                break;
        }
    }

    public void SetPlayerPosition(Vector3Int _pos)
    {
        debug_window.GetChild(2).GetChild(0).GetComponent<Text>().text = _pos.ToString();
    }

    
    public void SetDebugPad(string _input, float _val)
    {
        Color32 color = new Color32(100, 100, 100, 255);
        int _val_int = Mathf.RoundToInt(_val);
        if (_val != 0f)
            color = Color.white;
        
        switch(_input)
        {
            case "P360_RightTrigger":
                if (_val >= 0.5f)
                    debug_window.GetChild(1).GetChild(1).GetChild(3).GetChild(1).GetComponent<Text>().color = Color.white;
                else
                    debug_window.GetChild(1).GetChild(1).GetChild(3).GetChild(1).GetComponent<Text>().color = new Color32(100, 100, 100, 255);
                break;

            case "P360_LeftTrigger":
                if (_val >= 0.5f)
                    debug_window.GetChild(1).GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().color = Color.white;
                else
                    debug_window.GetChild(1).GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().color = new Color32(100, 100, 100, 255);
                break;

            case "P360_HorizontalDPAD":
                debug_window.GetChild(1).GetChild(1).GetChild(11).GetComponent<Text>().color = color;
                debug_window.GetChild(1).GetChild(1).GetChild(11).GetComponent<Text>().text = _val.ToString("F2");
                break;

            case "P360_VerticalDPAD":
                debug_window.GetChild(1).GetChild(1).GetChild(12).GetComponent<Text>().color = color;
                debug_window.GetChild(1).GetChild(1).GetChild(12).GetComponent<Text>().text = _val.ToString("F2");
                break;

            case "P360_DPAD":
                for (int i = 0; i < 9; i++)
                {
                    debug_window.GetChild(1).GetChild(1).GetChild(1).GetChild(i).GetComponent<Text>().color = new Color32(100, 100, 100, 255);
                }

                debug_window.GetChild(1).GetChild(1).GetChild(1).GetChild(_val_int - 1).GetComponent<Text>().color = Color.white;
                break;

            case "P360_LeftStick":
                for (int i = 0; i < 9; i++)
                {
                    if (i != 4)
                        debug_window.GetChild(1).GetChild(1).GetChild(5).GetChild(i).GetComponent<Text>().color = new Color32(100, 100, 100, 255);
                }

                if (_val_int != 5)
                    debug_window.GetChild(1).GetChild(1).GetChild(5).GetChild(_val_int - 1).GetComponent<Text>().color = Color.white;
                break;

            case "P360_RightStick":
                for (int i = 0; i < 9; i++)
                {
                    if (i != 4)
                        debug_window.GetChild(1).GetChild(1).GetChild(6).GetChild(i).GetComponent<Text>().color = new Color32(100, 100, 100, 255);
                }

                if(_val_int != 5)
                    debug_window.GetChild(1).GetChild(1).GetChild(6).GetChild(_val_int - 1).GetComponent<Text>().color = Color.white;
                break;

                
            case "P360_AButton":
                debug_window.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().color = color;
                break;

            case "P360_BButton":
                debug_window.GetChild(1).GetChild(1).GetChild(2).GetChild(1).GetComponent<Text>().color = color;
                break;

            case "P360_XButton":
                debug_window.GetChild(1).GetChild(1).GetChild(2).GetChild(2).GetComponent<Text>().color = color;
                break;

            case "P360_YButton":
                debug_window.GetChild(1).GetChild(1).GetChild(2).GetChild(3).GetComponent<Text>().color = color;
                break;

            case "P360_LeftBumper":
                debug_window.GetChild(1).GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>().color = color;
                break;
                          
            case "P360_RightBumper":
                debug_window.GetChild(1).GetChild(1).GetChild(4).GetChild(1).GetComponent<Text>().color = color;
                break;

            case "P360_Back":
                debug_window.GetChild(1).GetChild(1).GetChild(7).GetComponent<Text>().color = color;
                break;

            case "P360_Start":
                debug_window.GetChild(1).GetChild(1).GetChild(8).GetComponent<Text>().color = color;
                break;

            case "P360_LeftThumbStickButton":
                debug_window.GetChild(1).GetChild(1).GetChild(5).GetChild(4).GetComponent<Text>().color = color;
                break;

            case "P360_RightThumbStickButton":
                debug_window.GetChild(1).GetChild(1).GetChild(6).GetChild(4).GetComponent<Text>().color = color;
                break;
        }
    }

    // Materials
    public Material mat_light;
    public Material mat_nonLight;


    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                if (debug_window.gameObject.activeSelf)
                    debug_window.gameObject.SetActive(false);
                else
                    debug_window.gameObject.SetActive(true);
            }
        }
    }
    
    private void Awake()
    {
        
    }
}
