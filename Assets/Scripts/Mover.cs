using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Vector3 _start;
    [SerializeField]
    private Vector3 _end;
    [SerializeField, Min(0.1f)]
    private float _speed = 1f;
    [SerializeField, Min(0f)]
    private float _delay = 1f;

    private IEnumerator Start()
    {
        var body = GetComponent<Rigidbody>();
        var parent = transform.parent;
        
        var from = parent != null ? parent.TransformPoint(_start) : _start;
        var to = parent != null ? parent.TransformPoint(_end) : _end;

        while (true)
        {
            yield return Move(body, from, to);
            yield return Move(body, to, from);
        }
    }

    private IEnumerator Move(Rigidbody body, Vector3 from, Vector3 to)
    {
        var duration = Vector3.Distance(from, to) / _speed;
        var time = 0f;

        while (time < duration)
        {
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
            body.MovePosition(Vector3.Lerp(from, to, Mathf.Clamp01(time / duration)));
        }

        yield return new WaitForSeconds(_delay);
    }

    private void OnDrawGizmosSelected()
    {
        var parent = transform.parent;
        var from = parent != null ? parent.TransformPoint(_start) : _start;
        var to = parent != null ? parent.TransformPoint(_end) : _end;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(from, to);
        Gizmos.DrawSphere(from, 0.1f);
        Gizmos.DrawSphere(to, 0.1f);
    }
}
