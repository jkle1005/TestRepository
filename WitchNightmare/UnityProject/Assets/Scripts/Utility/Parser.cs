using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Witch;

using LitJson;

public class Parser : MonoBehaviour
{
    private static Parser instance;
    public static Parser Instance
    {
        get
        {
            if (Parser.instance == null)
                Parser.instance = FindObjectOfType<Parser>();
            return Parser.instance;
        }
    }
    
    public enum PHASE
    {
        __ready,
        __start,
        __next,
        __end,
        __end2,
    }
    
    private int[][] ParseDataDouble(string _data)
    {
        string str_ori = _data;
        string str_now = "";
        List<int> term = new List<int>();
        List<int[]> result = new List<int[]>();
        int i = 0;
        PHASE phase = PHASE.__ready;
        bool first = true;
        bool stop = false;
        int num_now = 0;
        int num_max = 0;

        if (str_ori.Length != 0)
        {
            if(str_ori[0] == '0')
            {
                return new int[][] { new int[]{ 0 } };
            }
            else
            {
                while (true)
                {
                    // Debug.Log(i.ToString() + ": " + str_now.ToString() + str_ori[i].ToString());
                    if (str_ori[i] == '_')
                    {
                        phase = PHASE.__next;
                    }
                    else if (str_ori[i] == '[')
                    {
                        phase = PHASE.__start;
                    }
                    else if (str_ori[i] == ']')
                    {

                        phase = PHASE.__end;
                    }
                    else
                    {
                        str_now += str_ori[i];
                    }

                    switch (phase)
                    {
                        case PHASE.__start:
                            if (first)
                            {
                                // Debug.Log(str_now);
                                num_max = System.Convert.ToInt32(str_now);
                                num_now = 0;
                                first = false;
                            }
                            str_now = "";
                            phase = PHASE.__ready;
                            break;

                        case PHASE.__next:
                            // Debug.Log("next: " + str_now);
                            term.Add(System.Convert.ToInt32(str_now));
                            str_now = "";
                            phase = PHASE.__ready;
                            break;

                        case PHASE.__end:
                            // Debug.Log("end: " + str_now);
                            term.Add(System.Convert.ToInt32(str_now));
                            result.Add(term.ToArray());
                            term.Clear();
                            num_now++;
                            phase = PHASE.__ready;
                            break;
                    }

                    i++;

                    if (str_ori.Length == i)
                        break;

                    if (stop)
                        break;

                    if (i >= 1000)
                    {
                        Debug.LogError("ERROR");
                        break;
                    }

                }
            }
        }
        else
        {
            Debug.LogError("Parser::GetEventInfo()::ParseData()::error_00");
        }

        if(result.Count != num_max && result.Count != num_now)
        {
            Debug.LogError("Parser::GetEventInfo()::ParseData()::error_01");
        }

        return result.ToArray();
    }

    private int[][][] ParseDataDouble(string _data, int _start, int _num)
    {
        string str_ori = _data;
        string str_now = "";
        List<int> term_1 = new List<int>();
        List<int[]> term_2 = new List<int[]>();
        List<int[][]> result = new List<int[][]>();
        int i = 0;
        PHASE phase = PHASE.__ready;
        bool stop = false;
        int num_now = 0;
        int num_max = _num;

        bool start = false;
        int start_check = 0;

        if (str_ori.Length != 0)
        {
            if (str_ori[0] == '0')
            {
                Debug.Log("asd");
                return new int[][][] { new int[][] { new int[] { 0 } } };
            }
            else
            {
                while (true)
                {
                    // Debug.Log(i.ToString() + ": " + str_now.ToString() + str_ori[i].ToString());
                    if (str_ori[i] == '_')
                    {
                        if (num_now < num_max)
                        {
                            phase = PHASE.__next;
                        }
                        else
                        {
                            phase = PHASE.__end;
                        }
                    }
                    else if (str_ori[i] == '[')
                    {
                        phase = PHASE.__start;
                    }
                    else if (str_ori[i] == ']')
                    {
                        phase = PHASE.__end2;
                    }
                    else
                    {
                        if(start)
                            str_now += str_ori[i];
                    }

                    switch (phase)
                    {
                        case PHASE.__start:
                            str_now = "";
                            start_check = 0;
                            phase = PHASE.__ready;
                            break;

                        case PHASE.__next:
                            // Debug.Log("next: " + str_now);
                            if(start)
                            {
                                term_1.Add(System.Convert.ToInt32(str_now));
                                str_now = "";
                                num_now++;
                            }
                            else
                            {
                                start_check++;
                                if (start_check == _start)
                                    start = true;
                            }
                            phase = PHASE.__ready;
                            break;

                        case PHASE.__end:
                            // Debug.Log("end: " + str_now);
                            if(start)
                            {
                                term_1.Add(System.Convert.ToInt32(str_now));
                                term_2.Add(term_1.ToArray());
                                str_now = "";
                                term_1.Clear();
                                num_now = 0;
                            }

                            phase = PHASE.__ready;
                            break;

                        case PHASE.__end2:
                            term_1.Add(System.Convert.ToInt32(str_now));
                            term_2.Add(term_1.ToArray());
                            result.Add(term_2.ToArray());
                            term_2.Clear();
                            term_1.Clear();
                            str_now = "";
                            phase = PHASE.__ready;

                            start = false;
                            start_check = 0;
                            num_now = 0;
                            break;
                    }

                    i++;

                    if (str_ori.Length == i)
                    {
                        // Debug.Log(str_ori.Length.ToString() + " == " + i.ToString());
                        break;
                    }
                        

                    if (stop)
                    {
                        // Debug.Log(stop.ToString());
                        break;
                    }
                        

                    if (i >= 1000)
                    {
                        Debug.LogError("ERROR");
                        break;
                    }

                }
            }
        }
        else
        {
            Debug.LogError("Parser::GetEventInfo()::ParseData()::error_00");
        }

        /*
        if (result.Count != num_max && result.Count != num_now)
        {
            Debug.LogError("Parser::GetEventInfo()::ParseData()::error_01");
        }*/

        return result.ToArray();
    }

    private int[] ParseDataSingle(string _data)
    {
        string str_now = "";
        List<int> term = new List<int>();
        int i = 0;

        if (_data.Length != 0)
        {
            while (true)
            {
                if (_data[i] == '_')
                {
                    term.Add(System.Convert.ToInt32(str_now.ToString()));
                    str_now = "";
                }
                else
                {
                    str_now += _data[i];
                }

                i++;

                if (_data.Length == i)
                {
                    term.Add(System.Convert.ToInt32(str_now.ToString()));
                    break;
                }

                if (i >= 100)
                {
                    Debug.LogError("ERROR");
                    break;
                }

            }
        }
        else
        {
            Debug.LogError("Parser::GetEventInfo()::ParseData()::error_00");
        }

        return term.ToArray();
    }

    public string[] ParseDataSingleString(string _data)
    {
        string str_now = "";
        List<string> term = new List<string>();
        int i = 0;

        bool stop = false;

        if (_data.Length != 0)
        {
            while (true)
            {
                if (_data[i] == '_')
                {
                    term.Add(str_now.ToString());
                    str_now = "";
                }
                else
                {
                    str_now += _data[i];
                }

                i++;

                if (_data.Length == i)
                {
                    term.Add(str_now.ToString());
                    break;
                }


                if (stop)
                    break;

                if (i >= 100)
                {
                    Debug.LogError("ERROR");
                    break;
                }

            }
        }
        else
        {
            Debug.LogError("Parser::GetEventInfo()::ParseData()::error_00");
        }

        return term.ToArray();
    }

    private int[] ParseDataToString(string _data)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < _data.Length; i++)
        {
            result.Add(System.Convert.ToInt32(_data[i].ToString()));
        }

        return result.ToArray();
    }   

    private void Awake()
    {

    }
}
