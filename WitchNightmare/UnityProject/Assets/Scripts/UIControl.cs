using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Witch;

public class UIControl : MonoBehaviour
{
    private static UIControl instance;
    public static UIControl Instance
    {
        get
        {
            if (UIControl.instance == null)
                UIControl.instance = FindObjectOfType<UIControl>();
            return UIControl.instance;
        }
    }    

    // 도구
    public Transform tool;
    private Transform tool_now;
    public void SetTool(Tool _tool)
    {
        tool_now.GetChild(1).GetComponent<Text>().text = "Tool\n" + _tool.name;
    }

    // 사용 아이템
    public Transform itemUse;
    private Transform itemUse_now_obj;
    private int itemUse_now_num;
    public void SetItemUse()
    {       
        if(Player.Instance.Info_item.Length == 0)
        {
            itemUse_now_obj.GetChild(1).GetComponent<Text>().text = "EMPTY";
        }
        else
        {
            if(inven_item_info[0][itemUse_now_num] == -1)
            {
                itemUse_now_obj.GetChild(1).GetComponent<Text>().text = "Item" + itemUse_now_num.ToString() + "\nEMPTY";
                Player.Instance.SetItemNow(-1);
            }
            else
            {
                Player.Instance.SetItemNow(inven_item_info[0][itemUse_now_num]);
                Item item = Player.Instance.GetItemNow();

                Debug.Log(item.name);

                if(item == null)
                    itemUse_now_obj.GetChild(1).GetComponent<Text>().text = "Item" + itemUse_now_num.ToString() + "\nEMPTY";
                else
                    itemUse_now_obj.GetChild(1).GetComponent<Text>().text = "Item" + itemUse_now_num.ToString() + "\n" + item.name;
            }
            
            
        }       
    }

    public void SwapItem(WAY _way)
    {
        switch (_way)
        {
            case WAY.__previous:
                itemUse_now_num--;
                if (itemUse_now_num < 0)
                    itemUse_now_num = inven_item_info[0].Length - 1;
                break;

            case WAY.__stop:
                break;

            case WAY.__next:
                itemUse_now_num++;
                if (itemUse_now_num == inven_item_info[0].Length)
                    itemUse_now_num = 0;
                break;
        }

        SetItemUse();
    }

    // 인벤토리
    public   Transform      inven;
    private  RectTransform  inven_select;
    private  RectTransform  inven_cursor;
    private  Vector2Int     inven_cursor_pos;
    private  Transform[][]  inven_item_obj;
    private  int[][]        inven_item_info;    
    private  int            inven_item_save;
    public void CallInventory()
    {
        inven_select.gameObject.SetActive(false);
        inven_cursor_pos = new Vector2Int(0, 1);
        inven_cursor.anchoredPosition = new Vector2(-81, 10);

        foreach (Transform[] trfs in inven_item_obj)
        {
            foreach (Transform trf in trfs)
            {
                if (trf.gameObject.activeSelf)
                    trf.gameObject.SetActive(false);
            }
        }

        inven_item_info = new int[][]
        {
                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },

                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },

                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },

                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },
        };

        if (Player.Instance.Info_item.Length > 0)
        {
            for (int i = 0; i < Player.Instance.Info_item.Length; i++)
            {
                Item item = Player.Instance.Info_item[i];
                Transform trf = inven_item_obj[item.inven_coordinate.y][item.inven_coordinate.x];
                trf.GetChild(0).GetComponent<Image>().sprite = item.icon;
                trf.GetChild(1).GetComponent<Text>().text = item.name;
                trf.gameObject.SetActive(true);

                inven_item_info[item.inven_coordinate.y][item.inven_coordinate.x] = i;
            }
        }

        inven_item_save = -1;

        inven.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        inven.gameObject.SetActive(false);
    }

    public void InvenMove(int _way, bool _jump = false)
    {
        Vector2Int pos = inven_cursor_pos;

        switch(_way)
        {
            case 2:
                if (pos.y != 3)
                    pos.y++;
                else if (_jump)
                    pos.y = 0;
                
                break;

            case 4:
                if (pos.x != 0)
                    pos.x--;
                else if (_jump)
                    pos.x = 6;

                break;

            case 6:
                if (pos.x != 6)
                    pos.x++;
                else if (_jump)
                    pos.x = 0;

                break;

            case 8:
                if (pos.y != 0)
                    pos.y--;
                else if (_jump)
                    pos.y = 3;

                break;                
        }

        inven_cursor_pos = pos;
        if(pos.y != 0)
            inven_cursor.anchoredPosition = new Vector3(-81f + (pos.x * 27f), 37f - (pos.y * 27f));
        else
            inven_cursor.anchoredPosition = new Vector3(-81f + (pos.x * 27f), 40f);
    }

    public void InvenSelectCell()
    {
        // 아이템 정보 저장 시 셀 번호로 저장하도록


        // 현재 저장된 아이템이 있는지 없는지 체크
        if (inven_item_save == -1)
        {
            // 저장된 아이템이 없으면     

            // 해당 셀에 아이템 없으면 리턴

            // 해당 아이템 아이콘 마우스에 붙이기
            // 해당 아이템 정보 저장 (인벤토리 주소로 해도 될듯)
            if (inven_item_info[inven_cursor_pos.y][inven_cursor_pos.x] == -1)
                return;
            else
            {
                inven_item_save = inven_item_info[inven_cursor_pos.y][inven_cursor_pos.x];

                if (inven_cursor_pos.y != 0)
                    inven_select.anchoredPosition = new Vector3(-81f + (inven_cursor_pos.x * 27f), 37f - (inven_cursor_pos.y * 27f));
                else
                    inven_select.anchoredPosition = new Vector3(-81f + (inven_cursor_pos.x * 27f), 40f);
                inven_select.gameObject.SetActive(true);
            }
        }
        else
        {
            // 저장된 아이템이 있을 경우
            Debug.Log(inven_cursor_pos.ToString());
            if (inven_item_info[inven_cursor_pos.y][inven_cursor_pos.x] == -1)
            {
                // 해당 셀에 아이템이 없으면
                // 저장된 아이템을 해당 셀에
                Item item = Player.Instance.Info_item[inven_item_save];
                Transform trf = inven_item_obj[item.inven_coordinate.y][item.inven_coordinate.x];
                trf.GetChild(0).GetComponent<Image>().sprite = null;
                trf.GetChild(1).GetComponent<Text>().text = "EMPTY";
                trf.gameObject.SetActive(false);

                inven_item_info[item.inven_coordinate.y][item.inven_coordinate.x] = -1;
                item.SetInven(inven_cursor_pos);                
                inven_select.gameObject.SetActive(false);

                // 정보 변경
                inven_item_info[inven_cursor_pos.y][inven_cursor_pos.x] = inven_item_save;

                // 이미지 변경
                trf = inven_item_obj[inven_cursor_pos.y][inven_cursor_pos.x];
                trf.GetChild(0).GetComponent<Image>().sprite = item.icon;
                trf.GetChild(1).GetComponent<Text>().text = item.name;
                trf.gameObject.SetActive(true);

                inven_item_save = -1;                
            }
            else
            {
                // 해당 셀에 아이템이 있으면
                // 저장된 아이템을 해당 셀에
                // 해당 셀에 있던 아이템을 저장된 아이템의 셀로 이동   

                Item item_ori = Player.Instance.Info_item[inven_item_save];
                Item item_dir = Player.Instance.Info_item[inven_item_info[inven_cursor_pos.y][inven_cursor_pos.x]];                
                inven_select.gameObject.SetActive(false);

                // 대상 -> 저장
                item_dir.SetInven(item_ori.inven_coordinate);

                // 정보 변경
                inven_item_info[item_dir.inven_coordinate.y][item_dir.inven_coordinate.x] = inven_item_info[inven_cursor_pos.y][inven_cursor_pos.x];

                // 이미지 변경
                Transform trf = inven_item_obj[item_dir.inven_coordinate.y][item_dir.inven_coordinate.x];
                trf.GetChild(0).GetComponent<Image>().sprite = item_dir.icon;
                trf.GetChild(1).GetComponent<Text>().text = item_dir.name;
                trf.gameObject.SetActive(true);


                // 저장 -> 대상
                item_ori.SetInven(inven_cursor_pos);                

                // 정보 변경
                inven_item_info[inven_cursor_pos.y][inven_cursor_pos.x] = inven_item_save;

                // 이미지 변경
                trf = inven_item_obj[inven_cursor_pos.y][inven_cursor_pos.x];
                trf.GetChild(0).GetComponent<Image>().sprite = item_ori.icon;
                trf.GetChild(1).GetComponent<Text>().text = item_ori.name;
                trf.gameObject.SetActive(true);

                inven_item_save = -1;
            }
        }

        SetItemUse();
    }

    public void InvenSelectCellCancel()
    {
        inven_item_save = -1;
        inven_select.gameObject.SetActive(false);
    }

    public bool InvenAddItem(Item _item)
    {        
        for (int y = 0; y < inven_item_info.Length; y++)
        {
            for (int x = 0; x < inven_item_info[y].Length; x++)
            {
                if(inven_item_info[y][x] == -1)
                {
                    _item.SetInven(new Vector2Int(x, y));

                    Transform trf = inven_item_obj[y][x];
                    trf.GetChild(0).GetComponent<Image>().sprite = _item.icon;
                    trf.GetChild(1).GetComponent<Text>().text = _item.name;
                    trf.gameObject.SetActive(true);

                    inven_item_info[y][x] = Player.Instance.Info_item.Length - 1;
                    SetItemUse();

                    return true;
                }
            }
        }

        return false;
    }

    // 윈도우를 부르는 UI 전반
    private UI_WINDOW win_now;
    public UI_WINDOW Win_now { get { return win_now; } }
    public void CallWindow(UI_WINDOW _win)
    {
        switch(_win)
        {
            case UI_WINDOW.__inventory:
                CallInventory();
                win_now = UI_WINDOW.__inventory;
                break;
        }

        Player.Instance.TurnOnUI();
    }

    public void SwapWindow(UI_WINDOW _win)
    {
        Debug.Log("UIControl::SwapWindow(_win):: win_now(" + win_now.ToString() + ") -> _win(" + _win.ToString() + ")");

        CloseWindow();
        CallWindow(_win);
    }

    public void CloseWindow(UI_WINDOW _win)
    {
        if(win_now != _win || win_now == UI_WINDOW.__none)
        {
            Debug.LogError("UIControl::CloseWindow(_win):: _win(" + _win.ToString() + ") != win_now(" + win_now.ToString() + ")");
            return;
        }

        switch(_win)
        {
            case UI_WINDOW.__inventory:
                CloseInventory();
                break;
        }

        win_now = UI_WINDOW.__none;
        Player.Instance.TurnOffUI();
    }

    public void CloseWindow()
    {
        if(win_now == UI_WINDOW.__none)
        {
            Debug.LogError("UIControl::CloseWindow():: win_now == UI_WINDOW.__none");
            return;
        }

        CloseWindow(win_now);
    }

    // 시간 관련
    public Transform ui_time;
    public void SetTime(TimeManager.TimeData _data)
    {
        // 날짜
        ui_time.GetChild(1).GetComponent<Text>().text = _data.GetMWD();

        // 시간
        ui_time.GetChild(2).GetComponent<Text>().text = _data.GetTime();

        // 날씨
        ui_time.GetChild(3).GetComponent<Text>().text = _data.GetWeather();

        // 위상
        ui_time.GetChild(4).GetComponent<Text>().text = _data.moonphase.ToString();
    }

    private void Awake()
    {
        // 시간 관련 (시간, 일, 월, 주, 날씨, 위상)

        // 도구
        tool_now = tool.GetChild(0);

        // 사용 아이템
        itemUse_now_obj = itemUse.GetChild(0);
        itemUse_now_num = 0;

        // 인벤토리
        inven_select = inven.GetChild(1).GetChild(0).GetComponent<RectTransform>();
        inven_cursor = inven.GetChild(1).GetChild(1).GetComponent<RectTransform>();
        
        inven_item_obj = new Transform[][]
        {
            new Transform[]
            {
                inven.GetChild(2).GetChild(0),
                inven.GetChild(2).GetChild(1),
                inven.GetChild(2).GetChild(2),
                inven.GetChild(2).GetChild(3),
                inven.GetChild(2).GetChild(4),
                inven.GetChild(2).GetChild(5),
                inven.GetChild(2).GetChild(6),
            },

            new Transform[] 
            {
                inven.GetChild(3).GetChild(0),
                inven.GetChild(3).GetChild(1),
                inven.GetChild(3).GetChild(2),
                inven.GetChild(3).GetChild(3),
                inven.GetChild(3).GetChild(4),
                inven.GetChild(3).GetChild(5),
                inven.GetChild(3).GetChild(6),
            },

            new Transform[]
            {
                inven.GetChild(3).GetChild(7),
                inven.GetChild(3).GetChild(8),
                inven.GetChild(3).GetChild(9),
                inven.GetChild(3).GetChild(10),
                inven.GetChild(3).GetChild(11),
                inven.GetChild(3).GetChild(12),
                inven.GetChild(3).GetChild(13),
            },
            
            new Transform[]
            {
                inven.GetChild(3).GetChild(14),
                inven.GetChild(3).GetChild(15),
                inven.GetChild(3).GetChild(16),
                inven.GetChild(3).GetChild(17),
                inven.GetChild(3).GetChild(18),
                inven.GetChild(3).GetChild(19),
                inven.GetChild(3).GetChild(20)
            }      
        };

        inven_item_info = new int[][]
            {
                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },

                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },

                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },

                new int[]
                {
                    -1, -1, -1, -1, -1, -1, -1
                },
            };
    }
}
