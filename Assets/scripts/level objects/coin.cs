using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public Transform trans;
    public Transform balloon;
    bool get = false;
    public float pullRange;
    public float collectRange;
    public float speed = 0.25f;
    public SaveFileManager gdata;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        balloon = GameObject.Find("Center").GetComponent<Transform>();
        get = false;
        gdata = GameObject.FindObjectsOfType<SaveFileManager>()[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector2.Distance(new Vector2(trans.position.x,trans.position.y),new Vector2(balloon.position.x,balloon.position.y));
        if (get)
        {
            float a = Mathf.Atan2(trans.position.y - balloon.position.y, trans.position.x - balloon.position.x);
            float s = (1f*speed) / (d - 2);
            trans.position += new Vector3(Mathf.Cos(a) * -s, Mathf.Sin(a) * -s, 0);
            if (d < collectRange)
            {
                CoinSoundEffect.CoinCollect();
                Destroy(gameObject);
            }
        }
        else if (d < pullRange)
        {
            if (!get)
            {
                gdata.saveData.coins++;
            }
            get = true;
        }
        trans.eulerAngles += new Vector3(0, 0, 5);
    }
}
