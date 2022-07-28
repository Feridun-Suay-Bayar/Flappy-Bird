using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnifeController : MonoBehaviour
{
    [SerializeField] Button button;
    private Rigidbody rb;
    public float forceForce;
    public float torque;
    private Vector2 startSwipe;
    private Vector2 endSwipe;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !button.IsActive())
        {
            button.gameObject.SetActive(false);
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0) && !button.IsActive())
        {
            rb.isKinematic = false;
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
        if(transform.position.y < -3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void Swipe()
    {
        Vector2 swipe = endSwipe - startSwipe;
        rb.AddForce(swipe * forceForce, ForceMode.Impulse);
        rb.AddTorque(torque, 0f, 0f, ForceMode.Impulse);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            button.gameObject.SetActive(true);
        }
        if (other.CompareTag("Floor"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Restart()
    {
        Debug.Log("Girdi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
