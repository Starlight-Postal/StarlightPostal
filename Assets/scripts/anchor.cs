using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static player;

public class anchor : MonoBehaviour
{
    LineRenderer line;

    [SerializeField] LayerMask anchorMask;
    [SerializeField] float maxDistance = 30f;
    [SerializeField] float anchorSpeed = 1f;
    [SerializeField] float anchorShootSpeed = 20f;
    [SerializeField] float realInDistance = 5f;


    public bool isAnchoredd = false;
    [HideInInspector] public bool anchoring = false;

    Vector2 target;
    public player player;
    //player inBalloon;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        player = GameObject.Find("player").GetComponent<player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAnchoredd && player.inBalloon)
        {
            StartAnchor();
        }
        else if (Input.GetMouseButtonDown(0) && isAnchoredd && player)
        {
            anchoring = false;
            isAnchoredd = false;
            line.enabled = false;
        }

        if (anchoring)
        {
            Vector2 anchorPos = Vector2.Lerp(transform.position, target, anchorSpeed * Time.deltaTime);

            transform.position = anchorPos;

            line.SetPosition(0, transform.position);

            if (Vector2.Distance(transform.position, target) < realInDistance)
            {

            }
        }
    }

    private void StartAnchor()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, anchorMask);

        if (hit.collider != null)
        {
            isAnchoredd = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Anchor());
        }
  /*      else
        {
            hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDistance, anchorMask);

            if (hit.collider != null)
            {
                isAnchored = true;
                target = hit.point;
                line.enabled = true;
                line.positionCount = 2;

                StartCoroutine(Anchor());
            }
        }
  */
    }

    IEnumerator Anchor()
    {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;

        for (; t < time; t += anchorShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        anchoring = true;
    }
}