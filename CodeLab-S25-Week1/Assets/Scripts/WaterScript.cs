using UnityEngine;

public class WaterScript : MonoBehaviour
{
    //
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public KeyCode shakeKey = KeyCode.LeftArrow;
    // public float shakeD = 0.5f;
    // public float shakeA = 0.5f;
    //
    // private Dictionary<KeyCode, Vector3> keytoAxis;
    //
    // public Rigidbody test;
    // private int maxPrefabs = 5000;
    // private int count = 0;
    // private List<Rigidbody> instantiatedPrefabs = new List<Rigidbody>();
    //
    // void Start()
    // {
    //     Debug.Log("im on");
    //     keytoAxis = new Dictionary<KeyCode, Vector3>
    //     {
    //         { KeyCode.LeftArrow, new Vector3(-1, 0, 0) },
    //         { KeyCode.RightArrow, new Vector3(1, 0, 0) },
    //         { KeyCode.UpArrow, new Vector3(0, 1, 0) },
    //         { KeyCode.DownArrow, new Vector3(0, -2, 0) }
    //     };
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     Vector3 newPos = transform.position;
    //
    //     foreach (var key in keytoAxis)
    //     {
    //         if (Input.GetKey(key.Key))
    //         {
    //             newPos = key.Value * Mathf.Sin(Time.time * (shakeD)) * (shakeA);
    //             NewElements(key.Key);
    //             //+ Time.time * 0.5f)
    //         }
    //
    //         if (Input.GetKeyUp(key.Key))
    //         {
    //             count = 0;
    //            
    //         }
    //     }
    //
    //     transform.position = newPos;
    //  
    // }
    //
    // private void NewElements(KeyCode selectedKey)
    // {
    //     switch (selectedKey)
    //     {
    //         case KeyCode.LeftArrow:
    //             Debug.Log("left");
    //                 Rigidbody clone;
    //                 clone = Instantiate(test, transform.position, transform.rotation);
    //                 clone.velocity = transform.TransformDirection(Vector3.down * 10);
    //                 instantiatedPrefabs.Add(clone);
    //                 count++; 
    //             
    //         
    //             break;
    //         case KeyCode.RightArrow:
    //             Debug.Log("right");
    //             break;
    //         case KeyCode.UpArrow:
    //             Debug.Log("up");
    //             break;
    //         case KeyCode.DownArrow:
    //             Debug.Log("down");
    //             break;
    //     }
    // }
    //
    // private void DestroyObjectDelayed()
    // {
    //     Destroy(gameObject);
    //     instantiatedPrefabs.RemoveAll(item => item == this);
    //     Debug.Log(instantiatedPrefabs.Count);
    // }
}
