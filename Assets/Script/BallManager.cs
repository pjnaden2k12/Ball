using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    public int ballCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetBallCount(int count)
    {
        ballCount = count;
    }

    public void AddBall(int amount)
    {
        ballCount += amount;
    }

    public void RemoveBall(int amount)
    {
        ballCount -= amount;
        if (ballCount < 0) ballCount = 0;
    }
}
