using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpHelper : MonoBehaviour
{
    private bool shouldLerp = false;

    public float timeStartedLerping;
    public float lerpTime;
    
    public Vector2 endPosition;
    public Vector2 startPosition;

    // Start is called before the first frame update
    private void StartLerping()
    {
        timeStartedLerping = Time.time;

        shouldLerp = true;
    }
    private void Start()
    {
        StartLerping();
    }
    // Update is called once per frame
    void Update()
    {
        if (shouldLerp)
        {
            transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
        }
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
}
