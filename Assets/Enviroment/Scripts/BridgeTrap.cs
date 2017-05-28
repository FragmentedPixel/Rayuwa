using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrap : MonoBehaviour
{
    public Transform part1, part2;

    public float duration;
    public float rotationSpeed = 1f;
    public float fallingSpeed = .1f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);

        if(other.CompareTag("Unit"))
        {
            StartCoroutine(FallingCR(part1, 1));
            StartCoroutine(FallingCR(part2, -1));
        }
    }

    private IEnumerator FallingCR(Transform t, int direction)
    {
        Destroy(t.GetComponent<Collider>());
        Destroy(GetComponent<Collider>());
        yield return null;
        FindObjectOfType<Grid>().ReCalculateGird();

        float currentTime = 0f;

        while(currentTime < duration)
        {
            t.Rotate(0f, rotationSpeed * direction, 0f);
            t.position += Vector3.down * fallingSpeed;

            currentTime += Time.deltaTime;
            yield return null;
        }

        Destroy(t.gameObject);
        yield break;
    }
}
