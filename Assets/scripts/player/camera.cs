using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class camera : MonoBehaviour
{
    public bool follow = true;

    public float speed = 0.05f;
    public Transform trans;
    public Camera cam;
    public Transform balloonTrans;
    public Transform playerTrans;
    public Transform target;

    public player player;
    public balloon balloon;

    public float balloonSize = 20;
    public float playerSize = 10;
    public float conversationSize = 1.5f;

    public Vector3 balloonOff;
    public Vector3 playerOff;
    Vector3 offset;

    private Vector2 ratio;
    public Vector2 camRange;

    public Transform range;
    public float playerLookRange = 1.0f;
    public float balloonLookRange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        cam = gameObject.GetComponent<Camera>();
        ratio = new Vector2(cam.aspect, 1);
        //balloonTrans = GameObject.Find("balloon").GetComponent<Transform>();
        playerTrans = GameObject.Find("player").GetComponent<Transform>();
        target = playerTrans;
        player = GameObject.Find("player").GetComponent<player>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (follow)
        {
            camRange = ratio * cam.orthographicSize;

            Vector3 cameraMove;
            float lookRange;
            if (player.inBalloon)
            {
                target = balloonTrans;
                cameraMove = balloonOff;
                lookRange = balloonLookRange;
            }
            else
            {
                target = playerTrans;
                cameraMove = playerOff;
                lookRange = playerLookRange;
            }

            cameraMove += offset * lookRange;

            trans.position += ((new Vector3(target.position.x, target.position.y, trans.position.z) + cameraMove) - trans.position) * speed;
            if (target == balloonTrans)
            {
                if (balloon.anchored&&balloon.anchor.landed)
                {
                    cam.orthographicSize += (balloonSize * 0.5f - cam.orthographicSize) * 0.0025f;
                }
                else
                {
                    cam.orthographicSize += (balloonSize - cam.orthographicSize) * 0.0025f;
                }
            }
            if (target == playerTrans)
            {
                if (player.currentConversation != null && player.currentConversation.ZoomCamera())
                {
                    cam.orthographicSize += (conversationSize - cam.orthographicSize) * 0.01f;
                } else
                {
                    cam.orthographicSize += (playerSize - cam.orthographicSize) * 0.01f;
                }
            }

            if (range != null)
            {
                //Debug.Log(trans.position.x+" , "+ (range.position.x - (range.localScale.x / 2f - camRange.x)));
                if (trans.position.x < range.position.x - (range.localScale.x / 2f - camRange.x))
                {
                    trans.position = new Vector3(range.position.x - (range.localScale.x / 2f - camRange.x), trans.position.y, trans.position.z);
                }
                if (trans.position.x > range.position.x + (range.localScale.x / 2f - camRange.x))
                {
                    trans.position = new Vector3(range.position.x + (range.localScale.x / 2f - camRange.x), trans.position.y, trans.position.z);
                }
                if (trans.position.y < range.position.y - (range.localScale.y / 2f - camRange.y))
                {
                    trans.position = new Vector3(trans.position.x, range.position.y - (range.localScale.y / 2f - camRange.y), trans.position.z);
                }
                if (trans.position.y > range.position.y + (range.localScale.y / 2f - camRange.y))
                {
                    trans.position = new Vector3(trans.position.x, range.position.y + (range.localScale.y / 2f - camRange.y), trans.position.z);
                }
            }
        }
    }

    public void Teleport() {
        target = balloonTrans;
        trans.position = new Vector3(target.position.x,target.position.y,trans.position.z) + offset;
        cam.orthographicSize = balloonSize;
    }

    void OnLook(InputValue value) {
        var input = value.Get<Vector2>();
        offset = new Vector3(input.x, input.y, 0);
    }

    public void snapToTarget()
    {
        trans.position = new Vector3(target.position.x, target.position.y, trans.position.z);
    }

}
