using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;

public class ImageRecognition : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;

    [SerializeField] private Camera arCamera;
    public Camera Camera
    {
        get { return arCamera; }
        set { arCamera = value; }
    }

    Vector2 touchPosition = default;

    private void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject child in g)
        {
            child.SetActive(false);
        }
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {   
        trackedImageManager.trackedImagesChanged += OnImageChanged;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var imageTracked in args.added)
        {
            imageTracked.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    //private void Start()
    //{
    //    Ray ray = arCamera.ScreenPointToRay(touchPosition);
    //    Physics.Raycast(ray, out RaycastHit hitObject);
    //    if (hitObject.collider.gameObject.GetComponent<Renderer>().enabled == true)
    //    {
    //        GameObject g = hitObject.collider.gameObject;
    //        g.SetActive(true);
    //        foreach (Transform child in g.transform)
    //        {
    //            if (child.gameObject.activeSelf == true)
    //            {
    //                child.gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //}


    void Update()
    {
        

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                if (Physics.Raycast(ray, out RaycastHit hitObject))
                {
                    if (hitObject.collider.gameObject.GetComponent<Renderer>().enabled == true)
                    {
                        GameObjectController goc = gameObject.AddComponent<GameObjectController>();
                        goc.ToggleChildren(hitObject.collider.gameObject, true);
                    }
                    else
                    {
                        GameObjectController goc = gameObject.AddComponent<GameObjectController>();
                        goc.ToggleChildren(hitObject.collider.gameObject, false);
                    }
                }
            }
        }
    }
}