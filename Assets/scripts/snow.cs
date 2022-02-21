using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snow : MonoBehaviour
{
    public wind wind;
    public Transform focus;
    public Camera cam;
    public float speed = 2;
    public float gravity = 0.05f;
    public float size = 0.25f;
    public float decayRate = 0.025f;
    public int n = 100;
    public List<Transform> flakes;
    public List<float> flakeLife;
    public List<float> flakeWeight;
    public List<Vector2> flakeV;
    public GameObject windParticle;

    public Vector2 ratio;
    public Vector2 range;
    public float buffer = 1.1f;
    // Start is called before the first frame update
    void Start()
    {
        //wind = GameObject.Find("wind").GetComponent<wind>();
        focus = GameObject.Find("Main Camera").GetComponent<Transform>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        range = ratio * cam.orthographicSize;

        for (int i = 0; i < n; i++)
        {
            spawnFlake(focus.position.x + range.x * Random.Range(-0.5f, 0.5f), focus.position.y + range.y * Random.Range(-0.5f, 0.5f),windParticle);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        range = ratio * cam.orthographicSize;
        for (int i = 0;i < flakes.Count;i++)
        {
            Vector2 w = (wind.getWind(flakes[i].position.x, flakes[i].position.y))*1f;
            //flakes[i].position += new Vector3(w.x, w.y,0)*speed*flakeWeight[i];
            flakeV[i] *= 0.95f;
            flakeV[i] += new Vector2(w.x, w.y) * speed * flakeWeight[i];
            flakes[i].position += new Vector3(flakeV[i].x, flakeV[i].y,0);
            flakes[i].position += new Vector3(0, (-gravity * flakeWeight[i])/(w.magnitude+1), 0);
            flakes[i].eulerAngles += new Vector3(0, 0, (flakeWeight[i] - 1) * 10);
            if (flakes[i].position.x >= focus.position.x + range.x * buffer)
            {
                flakes[i].position = new Vector3(focus.position.x - range.x * buffer, focus.position.y + range.y * Random.Range(-buffer,buffer), flakes[i].position.z);
                flakeV[i] = new Vector2(0, 0);
            } else if (flakes[i].position.x <= focus.position.x - range.x * buffer)
            {
                flakes[i].position = new Vector3(focus.position.x + range.x * buffer, focus.position.y + range.y * Random.Range(-buffer,buffer), flakes[i].position.z);
                flakeV[i] = new Vector2(0, 0);
            }
            if (flakes[i].position.y >= focus.position.y + range.y * buffer)
            {
                flakes[i].position = new Vector3(focus.position.x + range.x * Random.Range(-buffer, buffer), focus.position.y - range.y * buffer, flakes[i].position.z);
                flakeV[i] = new Vector2(0, 0);
            } else if (flakes[i].position.y <= focus.position.y +- range.y * buffer)
            {
                flakes[i].position = new Vector3(focus.position.x + range.x * Random.Range(-buffer, buffer), focus.position.y + range.y * buffer, flakes[i].position.z);
                flakeV[i] = new Vector2(0, 0);
            }
            float decay = decayRate/ (flakeV[i].magnitude+1);
            
            flakeLife[i] -= decay;
            if (flakeLife[i] <= 0)
            {
                flakeLife[i] = 1;
                flakes[i].position = new Vector3(focus.position.x + range.x * Random.Range(-buffer, buffer), focus.position.y + range.y * Random.Range(-buffer, buffer),3);
                flakeV[i] = new Vector2(0, 0);
            }
            float s = Mathf.Pow((Mathf.Pow(1 / (1 + Mathf.Pow((flakeLife[i] *2) - 1, 2)), 2) - 0.25f) * (4f / 3f),0.5f);
            flakes[i].localScale = new Vector3(1, 1, 1) * size * s * cam.orthographicSize/20f;


        }
    }

    void spawnFlake(float x,float y, GameObject pf)
    {
        //GameObject flakePos = GameObject.CreatePrimitive(PrimitiveType.Plane);
        GameObject flakeObj = Instantiate(pf, new Vector3(x,y,3), Quaternion.identity);
        //flakePos.transform.position = new Vector3(x, y,3);
        //flakePos.transform.eulerAngles = new Vector3(-90, 0, 0);
        //flakePos.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        flakes.Add(flakeObj.transform);
        flakeLife.Add(Random.Range(0f,1f));
        flakeWeight.Add(Random.Range(0.9f, 1.1f));
        flakeV.Add(new Vector2(0, 0));
    }
}
