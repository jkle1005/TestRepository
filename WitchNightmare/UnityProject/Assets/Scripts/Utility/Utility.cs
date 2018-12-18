// Decompiled with JetBrains decompiler
// Type: Utility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ED5B8037-DA9D-43BE-83BD-B17E18F59E74
// Assembly location: E:\게임 자료\HellHunter\실행파일\Test2\hell_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

using System.Linq;

public struct PairValue <T1, T2>
{
    public T1 value_t1;
    public T2 value_t2;

    public PairValue(T1 _t1, T2 _t2)
    {
        value_t1 = _t1;
        value_t2 = _t2;
    }
}

public class Coordinate
{
    public float max_x;
    public float min_x;
    public float max_y;    
    public float min_y;

    public Coordinate(float _minx, float _maxx, float _miny, float _maxy)
    {
        max_x = _maxx;
        min_x = _minx;
        max_y = _maxy;
        min_y = _miny;
    }
}

public class StringPlus
{
    public string str;
    public Color color;
    public float size;
    public Font font;

    public StringPlus(string _str, Color _col, float _size = 0f, Font _font = null)
    {
        str = _str;
        color = _col;
        size = _size;
        font = _font;
    }
}

public class AnimationCut
{
    public Sprite spr;
    public float delay;
    public Vector3 pos;

    public AnimationCut(Sprite _spr, float _del)
    {
        spr = _spr;
        delay = _del;
        pos = Vector3.zero;
    }

    public AnimationCut(Sprite _spr, float _del, Vector3 _pos)
    {
        spr = _spr;
        delay = _del;
        pos = _pos;
    }
}


public class Utility : MonoBehaviour
{
    private static Utility instance;

    public static Utility Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Utility>();
            return instance;
        }
    }

    public int GetRandomInt(int[] _chance)
    {
        List<int> intList = new List<int>();
        for (int index1 = 0; index1 < _chance.Length; ++index1)
        {
            if (_chance[index1] != 0)
            {
                for (int index2 = 0; index2 < _chance[index1]; ++index2)
                    intList.Add(index1);
            }
        }
        return intList[Random.Range(0, intList.Count)];
    }

    public int GetRandomInt(List<int> _chance)
    {
        List<int> intList = new List<int>();
        for (int index1 = 0; index1 < _chance.Count; ++index1)
        {
            if (_chance[index1] != 0)
            {
                for (int index2 = 0; index2 < _chance[index1]; ++index2)
                    intList.Add(index1);
            }
        }
        return intList[Random.Range(0, intList.Count)];
    }

    public Vector3 CheckDirect(Vector3 _ori, Vector3 _dir)
    {

        return (_ori - _dir).normalized;
    }

    public void DelayDestroy(GameObject _obj, float _time)
    {
        StartCoroutine(delayDestroy(_obj, _time));
    }

    private IEnumerator delayDestroy(GameObject _obj, float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(_obj);
    }

    public void DelayOff(GameObject _obj, float _time)
    {
        StartCoroutine(delayOff(_obj, _time));
    }

    private IEnumerator delayOff(GameObject _obj, float _time)
    {
        yield return new WaitForSeconds(_time);
        _obj.SetActive(false);
    }

    IEnumerator crt_slow;
    public void Slow(float _scale, float _time)
    {
        if (crt_slow != null)
            StopCoroutine(crt_slow);

        crt_slow = slow(_scale, _time);
        StartCoroutine(crt_slow);
    }

    private IEnumerator slow(float _scale, float _time)
    {
        Time.timeScale = _scale;
        float t = 0f;

        while (true)
        {
            t += Time.unscaledDeltaTime;

            if (t >= _time)
                break;

            yield return null;
        }
        Time.timeScale = 1f;
    }

    public System.Array CombineArray(System.Array _list1, System.Array _list2)
    {
        System.Array _result = new System.Array[_list1.Length + _list2.Length];
        System.Array.Copy(_list1, 0, _result, 0, _list1.Length);
        System.Array.Copy(_list2, 0, _result, _list1.Length, _list2.Length);
        return _result;
    }

    public AnimationCut[] CombineAnimationCut(Sprite[] _spr, float[] _del)
    {
        List<AnimationCut> ac = new List<AnimationCut>();

        if ((_spr.Length != _del.Length) && _del.Length != 1)
            Debug.LogError(_spr.Length + ", " + _del.Length);

        for (int i = 0; i < _spr.Length; i++)
        {
            if (_del.Length == 1)
            {
                ac.Add(new AnimationCut(_spr[i], _del[0]));
            }
            else
            {
                ac.Add(new AnimationCut(_spr[i], _del[i]));
            }
        }

        return ac.ToArray();
    }

    public AnimationCut[] CombineAnimationCut(Sprite[] _spr, float[] _del, Vector3[] _pos)
    {
        List<AnimationCut> ac = new List<AnimationCut>();

        if (((_spr.Length != _del.Length) && _del.Length != 1) || _spr.Length != _pos.Length)
            Debug.LogError(_spr.Length + ", " + _del.Length);

        for (int i = 0; i < _spr.Length; i++)
        {
            if (_del.Length == 1)
            {
                ac.Add(new AnimationCut(_spr[i], _del[0], _pos[i]));
            }
            else
            {
                ac.Add(new AnimationCut(_spr[i], _del[i], _pos[i]));
            }
        }

        return ac.ToArray();
    }

    public string GetRandomStr(int _length = 25)
    {
        string _result = "";


        for (int i = 0; i < _length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                _result += Random.Range(0, 10).ToString();
            }
            else
            {
                _result += (char)Random.Range(65, 91);
            }
        }

        return _result;
    }

    public Vector2 GetRandomWay()
    {
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                return new Vector2(1f, 1f);

            case 1:
                return new Vector2(1f, -1f);

            case 2:
                return new Vector2(-1f, -1f);

            case 3:
                return new Vector2(-1f, 1f);

            default:
                Debug.LogError("Utility::GetRandomWay::Random.Range(0, 4) = " + i);
                return Vector3.zero;


        }
    }
    
    public float GetRotation(Vector3 _base, Vector3 _obj)
    {
        Vector3 v = _base - _obj;
        float angle = Mathf.Atan2(v.z, v.x) * (180f / Mathf.PI);
        // angle = Mathf.Ceil(angle * 0.1f) * 10f;

        if (-90f >= angle && angle > -180f)
        {
            angle = angle * -1f - 90f;
        }
        else if (180f >= angle && 90 < angle)
        {
            angle = 270f - angle;
        }
        else if (90 >= angle && 0 < angle)
        {
            angle = 270f - angle;
        }
        else if (0 >= angle && -90 < angle)
        {
            angle = 270 - angle;
        }

        return angle;
    }

    public int GetLimitCalculation(int _num, int _limit, bool _up, bool _reset = true)
    {
        if(_up)
        {
            if (_num >= _limit)
            {
                if (_reset)
                    return 0;
                else
                    return _limit;
            }
            else
                return _num;
        }
        else
        {
            if (_num <= _limit)
            {
                if (_reset)
                    return 0;
                else
                    return _limit;
            }
            else
                return _num;
        }
    }

    public Vector3 GetMousePosition(float _z = 0f)
    {
        float _c = Camera.main.orthographicSize * 2f / 9f;
        float _x = (Input.mousePosition.x - (Screen.width * 0.5f)) / Screen.width * _c * 16f;
        float _y = (Input.mousePosition.y - (Screen.height * 0.5f)) / Screen.height * _c * 9f;
        
        Vector3 camera = Camera.main.transform.position;

        _x += camera.x;
        _y += camera.y;

        return new Vector3(_x, _y, _z);
    }

    public string GetMoneyString(int _num)
    {
        string ori = _num.ToString();
        string result_re = "";
        string result = "";

        int j = 0;
        for (int i = ori.Length - 1; i >= 0; i--)
        {
            result_re += ori[i];
            j++;

            if (j == 3 && i != 0)
            {
                result_re += ",";
                j = 0;
            }
        }

        for (int i = result_re.Length - 1; i >= 0; i--)
        {
            result += result_re[i];
        }

        return result;
    }

    public int Choose(int[] _par)
    {
        int sum = 0;

        foreach (int num in _par)
            sum += num;

        float var = Random.value * sum;

        for (int i = 0; i < _par.Length; i++)
        {
            if(var < _par[i])
            {
                return i;            
            }
            else
            {
                var -= _par[i];
            }
        }

        return _par.Length - 1;
    }

    public int Choose(float[] _par)
    {
        float sum = 0;

        foreach (float num in _par)
            sum += num;

        float var = Random.value * sum;

        for (int i = 0; i < _par.Length; i++)
        {
            if (var < _par[i])
            {
                return i;
            }
            else
            {
                var -= _par[i];
            }
        }

        return _par.Length - 1;
    }

    public int Choose(SortedDictionary<float, int> _dic)
    {
        float sum = 0;

        foreach (KeyValuePair<float, int> kvp in _dic)
            sum += kvp.Key;

        float var = Random.value * sum;
        int i = 0;

        foreach(KeyValuePair<float, int> kvp in _dic)
        {
            if(var < kvp.Key)
            {
                return kvp.Value;
            }
            else
            {
                if (i < _dic.Count)
                    var -= kvp.Key;
                else
                    return kvp.Value;
            }
            i++;
        }

        Debug.LogError("Utility::Choose(Dic):: Code0000");
        return 0;
    }

    public int Choose(PairValue<float, int>[] _pair)
    {
        var list_sorted =
            from pair in _pair
            orderby pair.value_t1
            select pair;

        float sum = 0;

        foreach (PairValue<float, int> pv in list_sorted)
            sum += pv.value_t1;

        float var = Random.value * sum;
        int i = 0;

        foreach(PairValue<float, int> pv in list_sorted)
        {
            if(var < pv.value_t1)
            {
                return pv.value_t2;
            }
            else
            {
                if (i <= list_sorted.Count())
                    var -= pv.value_t1;
                else
                    return pv.value_t2;
            }
            i++;
        }

        Debug.LogError("Utility::Choose(PV):: Code0000");
        return 0;
    }

    public bool isEven(int _num)
    {
        if (_num % 2 == 0)
            return true;
        else
            return false;
    }
}
