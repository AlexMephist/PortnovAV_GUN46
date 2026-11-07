using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotate;

    private IEnumerator Start()
    {
        var body = GetComponent<Rigidbody>();

        while (true)
        {
            yield return new WaitForFixedUpdate();
            body.MoveRotation(body.rotation * Quaternion.Euler(_rotate * Time.fixedDeltaTime));
        }
    }
}