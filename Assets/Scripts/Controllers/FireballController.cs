using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float scaleSpeed = 1.0f;

    void Start()
    {
        StartCoroutine(ScaleAndDestroyCoroutine());
        GetComponent<AudioSource>().Play();
    }

    private IEnumerator ScaleAndDestroyCoroutine()
    {
        // Wait for 2 seconds
        yield return new WaitForSecondsRealtime(2);
        // Gradually scale down the GameObject
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            yield return null;
        }

        // Ensure the GameObject is completely scaled down
        transform.localScale = Vector3.zero;

        // Destroy the GameObject
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            // destroy self
            Destroy(gameObject);
        }
    }
}