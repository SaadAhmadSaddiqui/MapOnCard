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

    [SerializeField] private GameObject[] uniRooms;

    public GameObject[] MyPlane
    {
        get { return uniRooms; }
        set { uniRooms = value; }
    }

    [SerializeField] private Camera arCamera;
    public Camera Camera
    {
        get { return arCamera; }
        set { arCamera = value; }
    }

    Vector2 touchPosition = default;

    private void Awake()
    {
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
    //    Vector3 bruh = uniRooms[0].transform.position;
    //    Physics.Raycast(arCamera.ScreenPointToRay(bruh), out RaycastHit hit);
    //    {
    //        hit.collider.gameObject.GetComponent<Renderer>().enabled = false;
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
                    if (hitObject.collider.gameObject.GetComponent<Renderer>().enabled == false)
                    {
                        hitObject.collider.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        hitObject.collider.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }
            }
        }
    }
}