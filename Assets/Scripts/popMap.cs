using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popMap : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameObject g = this.gameObject;
        g.SetActive(true);
        foreach (Transform child in g.transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
