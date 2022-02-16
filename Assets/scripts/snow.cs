using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snow : MonoBehaviour
{
    public wind wind;
    public Transform focus;
    public Camera cam;
    public float speed = 2;
    public int n = 100;
    public List<Transform> flakes;
    public List<float> flakeLife;
    public List<float> flakeWeight;
    public GameObject windParticle;

    public Vector2 ratio;
    public Vector2 range;
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
            flakes[i].position += new Vector3(w.x, w.y,0)*speed*flakeWeight[i];
            if (flakes[i].position.x >= focus.position.x + range.x * 0.5f)
            {
                flakes[i].position = new Vector3(focus.position.x - range.x * 0.5f, focus.position.y + range.y * Random.Range(-0.5f, 0.5f), flakes[i].position.z);
            } else if (flakes[i].position.x <= focus.position.x - range.x * 0.5f)
            {
                flakes[i].position = new Vector3(focus.position.x + range.x * 0.5f, focus.position.y + range.y * Random.Range(-0.5f, 0.5f), flakes[i].position.z);
            }
            if (flakes[i].position.y >= focus.position.y + range.y * 0.5f)
            {
                flakes[i].position = new Vector3(focus.position.x + range.x * Random.Range(-0.5f, 0.5f), focus.position.y - range.y * 0.5f, flakes[i].position.z);
            } else if (flakes[i].position.y <= focus.position.y +- range.y * 0.5f)
            {
                flakes[i].position = new Vector3(focus.position.x + range.x * Random.Range(-0.5f, 0.5f), focus.position.y + range.y * 0.5f, flakes[i].position.z);
            }
            flakeLife[i] -= 0.0025f;
            if (flakeLife[i] <= 0)
            {
                flakeLife[i] = 1;
                flakes[i].position = new Vector3(focus.position.x + range.x * Random.Range(-0.5f, 0.5f), focus.position.y + range.y * Random.Range(-0.5f, 0.5f),3);
            }
            float s = Mathf.Pow((Mathf.Pow(1 / (1 + Mathf.Pow((flakeLife[i] *2) - 1, 2)), 2) - 0.25f) * (4f / 3f),0.5f);
            flakes[i].localScale = new Vector3(1, 1, 1) * 0.5f * s * cam.orthographicSize/20f;


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
    }
}
