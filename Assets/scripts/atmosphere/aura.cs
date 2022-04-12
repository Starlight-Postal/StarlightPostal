using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aura : MonoBehaviour
{
    public Transform trans;
    public SpriteRenderer sprite;
    public float speed = 0.1f;
    public float range;
    public float period;
    public float size = 1;
    public Color color;
    public Vector3 pos;

    public int lifetime = 90;

    public int TICK = 0;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        trans.localScale = new Vector3(size,size,size);
        pos = trans.localPosition;
        
        FixedUpdate();
        sprite.enabled = true;
        TICK = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TICK++;
        float life = Mathf.Pow(1 - (Mathf.Pow(((float)TICK / lifetime) - 0.5f, 2) * 4),0.5f);
        if (life > 0)
        {
            trans.localPosition = pos + new Vector3(0, Mathf.Sin((TICK * speed) + period) * range, 0);
            sprite.color = color * new Color(1, 1, 1, life);
            trans.localScale = new Vector3(1, 1, 1) * size * life;
        } else
        {
            trans.localScale = new Vector3(0, 0, 0);
        }
        if (TICK > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
