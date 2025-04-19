using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public float radius;
    public LayerMask layerMask;
    public float speed;

    protected bool isFlying = true;
    public BallShot ballShot;

    protected virtual void Start()
    {
        isFlying = true;
    }

    private void FixedUpdate()
    {
        if (!isFlying) return;

        Vector3 currentPos = transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(currentPos, radius, direction, speed * Time.fixedDeltaTime, layerMask);

        if (hit.collider != null)
        {
            transform.position = hit.centroid;
            direction = Vector2.Reflect(direction, hit.normal);
            transform.position += (Vector3)direction * 0.2f;

            if (hit.collider.CompareTag("Box"))
            {
                Box box = hit.collider.GetComponent<Box>();
                if (box != null)
                    OnHitBox(box);
            }

            if (hit.collider.CompareTag("Boundary"))
            {
                isFlying = false;
                MoveToStartPosition();
            }
        }
        else
        {
            transform.position += (Vector3)direction * speed * Time.fixedDeltaTime;
            Debug.Log("Current Speed: " + speed);

        }
    }

    protected virtual void OnHitBox(Box box)
    {
        box.ReduceHP(30); // default
    }

    public void MoveToStartPosition()
    {
        StartCoroutine(MoveToStartPositionCoroutine());
    }

    IEnumerator MoveToStartPositionCoroutine()
    {
        GetComponent<Collider2D>().enabled = false;

        while ((Vector2)transform.position != startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            yield return null;
        }

        ballShot.GainBall();
        Destroy(gameObject);
    }
    
}