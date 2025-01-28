using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public KeyCode shakeKey = KeyCode.LeftArrow;
    public float shakeD = 0.5f;
    public float shakeA = 0.5f;

    private Dictionary<KeyCode, Vector3> keytoAxis;

    public Rigidbody test;
    private int maxPrefabs = 5000;
    private float count = 0;

    public GameObject glass;
    private Renderer glassRenderer;
    private Color initialColor;
    private float minMaterial = 0.5f;
    private float maxMaterial = 0.1f;
    
    void Start()
    {
        glassRenderer = glass.GetComponent<Renderer>();
        if (glassRenderer != null)
        {
            initialColor = glassRenderer.material.color;
        }
        Debug.Log("im on");
        keytoAxis = new Dictionary<KeyCode, Vector3>
        {
            { KeyCode.LeftArrow, new Vector3(-1, 0, 0) },
            { KeyCode.RightArrow, new Vector3(1, 0, 0) },
            { KeyCode.UpArrow, new Vector3(0, 1, 0) },
            { KeyCode.DownArrow, new Vector3(0, -2, 0) }
        };
    }

    // Update is called once per frame
    void Update()
    {
        DestroyObjectDelayed();

        Vector3 newPos = transform.position;

        foreach (var key in keytoAxis)
        {
            if (Input.GetKey(key.Key))
            {
                newPos = key.Value * Mathf.Sin(Time.time * (shakeD)) * (shakeA);
                NewElements(key.Key);
                //+ Time.time * 0.5f)
            }
            
        }

        transform.position = newPos;
        UpdateMaterial();
    }

    private void NewElements(KeyCode selectedKey)
    {
        switch (selectedKey)
        {
            case KeyCode.LeftArrow:
                Debug.Log("left");
                createPrefab();
                break;
            case KeyCode.RightArrow:
                Debug.Log("right");
                createPrefab();
                break;
            case KeyCode.UpArrow:
                Debug.Log("up");
                createPrefab();
                break;
            case KeyCode.DownArrow:
                Debug.Log("down");
                createPrefab();
                break;
        }
    }

    private void createPrefab()
    {
        if (count < maxPrefabs)
        {
            Rigidbody clone;
            clone = Instantiate(test, transform.position, transform.rotation);
            clone.linearVelocity = transform.TransformDirection(Vector3.down * 10);
            count++; 
        }
      
    }

    private void DestroyObjectDelayed()
    {
        if (gameObject.CompareTag("newasset"))
        {
            Debug.Log("destroyed after 10 seconds.");

            Destroy(gameObject);
        }
        
    }

    private void UpdateMaterial()
    {
        if (glassRenderer != null)
        {
            float targetOpacity = Mathf.Lerp(minMaterial, maxMaterial, (float)count / maxPrefabs);
            Color currentColor = glassRenderer.material.GetColor("_Color");
            glassRenderer.material.SetColor("_Color", new Color(currentColor.r, currentColor.g, currentColor.b, targetOpacity));

        }
    }
}