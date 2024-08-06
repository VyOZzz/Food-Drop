using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    private float yBoundary = 6;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -yBoundary)
        {
            if(gameObject.CompareTag("Food"))
                GameManager.Instance.LostFood(1);
            Destroy(gameObject);
        }
    }
}
