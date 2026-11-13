using UnityEngine;

public class Gates : MonoBehaviour
{
    private int _score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out var ball))
        {
            _score++;
            Debug.Log($"Score: {_score}");
            Destroy(ball.gameObject);
        }
    }
}