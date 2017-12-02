using UnityEngine;
using System.Collections;

//Moves a transform from one location to another over a given period of time
public class Mover
{
    Transform transform;
    Vector3 endPosition;
    Vector3 startPosition;
    float endTime;
    float startTime;
 
    public Mover(Transform transform)
    {
        this.transform = transform;
        this.endPosition = transform.position;
    } 

    public void MoveTo(Vector3 position, float time)
    {
        startTime = Time.time;
        endTime = Time.time + time;
        startPosition = transform.position;
        endPosition = position;
    }
    
    public void Update()
    {
        if (transform.position != endPosition)
        {
            float interpolation = (Time.time - startTime) / (endTime - startTime);
            transform.position = Vector3.Lerp(startPosition, endPosition, interpolation);
        }
    }
}
