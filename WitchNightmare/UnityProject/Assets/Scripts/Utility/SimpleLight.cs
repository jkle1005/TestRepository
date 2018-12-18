using UnityEngine;
using System.Collections;

public class SimpleLight : MonoBehaviour {
    private Light lighT;

    public float[] size;
    public float[] power;
    public float[] delay;
    public bool on_size;
    public bool on_power;

    private bool running;

    private bool stop;

    public void Stop()
    {
        stop = true;
    }
    
    IEnumerator lightControl_power()
    {
        int i = 0;
        float _delay = 0f;

        while(true)
        {
            if (stop)
                break;

            lighT.intensity = power[i];

            if (delay.Length == 1)
                _delay = delay[0];
            else
                _delay = delay[i];

            i++;

            if (power.Length == i)
                i = 0;

            yield return new WaitForSeconds(_delay);
        }
    }

    IEnumerator lightControl_size()
    {
        int i = 0;
        float _delay = 0f;

        while (true)
        {
            if (stop)
                break;

            lighT.spotAngle = size[i];

            if (delay.Length == 1)
                _delay = delay[0];
            else
                _delay = delay[i];

            i++;

            if (size.Length == i)
                i = 0;

            yield return new WaitForSeconds(_delay);
        }
    }

    private void Awake()
    {
        lighT = GetComponent<Light>();
        running = false;
    }

    private void OnEnable()
    {
        if(on_power)
            StartCoroutine(lightControl_power());

        if (on_size)
            StartCoroutine(lightControl_size());
    }
}
