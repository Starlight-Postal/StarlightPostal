using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static player;

public class GrappleHook : MonoBehaviour {
    LineRenderer line;

    [SerializeField] LayerMask grapplableMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 1f;
    [SerializeField] float grappleShootSpeed = 20f;
    [SerializeField] float realInDistance = 5f;


    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector2 target;
    public player player;
    anchor var;

    //player inBalloon;

    private void Start() {
        line = GetComponent<LineRenderer>();
        player = GameObject.Find("player").GetComponent<player>();
        var = GetComponent<anchor>();

    }

    private void Update() {
        if (Input.GetMouseButtonDown(1) && !isGrappling && player.inBalloon) {
            StartGrapple();
        }
        else if (Input.GetMouseButtonDown(1) && isGrappling || !player.inBalloon && !var.isAnchoredd)
        {
            retracting = false;
            isGrappling = false;
            line.enabled = false;
        }

        if (retracting && Vector2.Distance(transform.position, target) > realInDistance) {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position);

    //        if (Vector2.Distance(transform.position, target) < realInDistance) {
    //            retracting = false;
    //            isGrappling = false;
    //            line.enabled = false;
    //        }
        }
    }

    private void StartGrapple() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null) {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple() {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position); 

        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime) {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }
        
        line.SetPosition(1, target);
        retracting = true;
    }
    /*
     *     //Grappling varables
    GrappleHook gh;
    float speed = 5f;
     * 
     *         
     * 
     * 
     *         //Grappling checks
        if (!gh.retracting)
        {
            rb.velocity = new Vector2(ReadOnlyCollectionBase.velocity.x, ReadOnlyCollectionBase.velocity.y).normalized * speed;
        }
        else
        {
            ReadOnlyCollectionBase.velocity = Vector2.zero;
        }
     */
}
