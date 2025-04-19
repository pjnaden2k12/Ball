using System.Collections;
using UnityEngine;

public class BallShot : MonoBehaviour
{
    public Transform lookAt;
    public Transform lookAtTarget;
    public Camera maincamera;

    public float minY = 2;

    public int maxBall = 5; // Chỉ là số lượng bóng gốc khi vào game

    public Ball ballPrefab;

    public float ballSpeed = 5;

    private int currentBall; // Số bóng hiện tại để bắn
    public TextMesh textMesh;

    private Game game;

    private bool isShooting = false;

    private void Start()
    {
        game = FindObjectOfType<Game>();
        currentBall = maxBall;
        BallManager.Instance.SetBallCount(currentBall);
        UpdateBallText();
    }

    private void Update()
    {
        if (isShooting) return;

        // Điều khiển hướng bắn
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPos = maincamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            if (targetPos.y < minY)
                targetPos.y = minY;
            targetPos.x = Mathf.Clamp(targetPos.x, 0.6f, 8.5f);
            lookAtTarget.position = targetPos;
            lookAt.LookAt(lookAtTarget);
        }

        // Khi nhả chuột, bắn bóng
        if (Input.GetMouseButtonUp(0) && BallManager.Instance.ballCount > 0)
        {
            isShooting = true;
            StartCoroutine(ShootCoroutine(lookAt.forward, BallManager.Instance.ballCount));
        }
    }

    private IEnumerator ShootCoroutine(Vector3 direction, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Ball ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.direction = direction;
            ball.speed = ballSpeed;
            ball.startPos = transform.position;

            BallManager.Instance.RemoveBall(1);
            UpdateBallText();

            yield return new WaitForSeconds(0.1f);
        }

        // Chờ đến khi số lượng bóng quay lại đủ (reset lại lượt mới)
        while (BallManager.Instance.ballCount < count)
            yield return null;

        game.EndTurn();
        isShooting = false;
    }

    public void GainBall()
    {
        BallManager.Instance.AddBall(1);
        UpdateBallText();
    }

    private void UpdateBallText()
    {
        textMesh.text = "x" + BallManager.Instance.ballCount;
    }
}
