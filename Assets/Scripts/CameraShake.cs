using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    /// <summary>
    /// Trigger a camera shake script to be run along side the update cycle
    /// </summary>
    /// <param name="shakeDuration">Duration of the camera shake</param>
    /// <param name="shakeStrength">Strength of the camera shake</param>
    /// <returns></returns>
    public IEnumerator Shake(float shakeDuration, float shakeStrength) 
    {
        //Save original position
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < shakeDuration) 
        {
            //Shake camera randomly
            float x = Random.Range(-1f, 1f) * shakeStrength;
            float y = Random.Range(-1f, 1f) * shakeStrength;

            transform.localPosition = new Vector3(x,y,originalPosition.z);

            elapsed += Time.deltaTime;

            //Wait for next frame
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
