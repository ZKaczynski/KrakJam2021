using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flare : MonoBehaviour
{
    Light2D light2D;

    public float lifetime = 10f;
    public float flickeringIntensivity = 0.01f;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        StartCoroutine(FlareLife());
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value/10);
            if (Random.value > 0.5)
            {
                light2D.intensity = light2D.intensity - flickeringIntensivity;
            }
            else
            {
                light2D.intensity = light2D.intensity + flickeringIntensivity;
            }
        }


    }

    IEnumerator FlareLife()
    {

        yield return new WaitForSeconds(lifetime);

        while (light2D.intensity > 0.3)
        {
            yield return new WaitForSeconds(Random.value / 20);
            light2D.intensity -= flickeringIntensivity;
            
        }

        Destroy(gameObject);
        

    }
}
