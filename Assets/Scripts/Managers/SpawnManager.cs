using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private GameObject droppablePrefab;
    [SerializeField]
    private Transform dropTrackerTransform;
    [SerializeField]
    private float dropTrackerOffset, vigZoomSpeed, vigOriginal, vigGoal;

    [SerializeField]
    private VolumeProfile volumeProfile;

    private Vignette vig;
    private int stepCount;
    private float dropTimeInterval = 0.1f, timer;
    private bool holding;
    private GameObject currentDroppable;
    private LineRenderer trackerLine;
    private bool slowed;

    private void Start()
    {
        trackerLine = dropTrackerTransform.GetComponent<LineRenderer>();
        volumeProfile.TryGet<Vignette>(out vig);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSceneManager.Instance.IsGamePaused())
            return;

        timer -= Time.deltaTime;
        //Instantiate
        if (!holding)
        {
            if (timer < 0)
            {
                holding = true;
                currentDroppable = Instantiate(droppablePrefab);
            }
        }
        else
        {
            if (currentDroppable != null)
            {
                //Tracking
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    currentDroppable.transform.position = hit.point + Vector3.up;
                    //Tracker
                    dropTrackerTransform.position = hit.point + Vector3.up * dropTrackerOffset;
                    //Line
                    trackerLine.SetPosition(0, hit.point + Vector3.up);
                    trackerLine.SetPosition(1, hit.point);
                    trackerLine.startColor = Color.white;
                    trackerLine.endColor = Color.green;
                    trackerLine.startWidth = 0.01f;
                    trackerLine.endWidth = 0.01f;
                }

                //Slow-Mo
                if (Input.GetMouseButton(0))
                {
                    if (!slowed)
                    {
                        Time.timeScale = 0.1f;
                        slowed = true;
                    }

                    vig.intensity.value += vigZoomSpeed * Time.deltaTime;
                    if (vig.intensity.value > vigGoal)
                    {
                        vig.intensity.value = vigGoal;
                    }
                }

                //Release
                if (Input.GetMouseButtonUp(0))
                {
                    holding = false;
                    timer = dropTimeInterval;
                    stepCount++;
                    currentDroppable.GetComponent<IDroppableObject>().OnDrop();
                    Time.timeScale = 1f;
                    vig.intensity.value = vigOriginal;
                    slowed = false;
                }
            }
        }
    }

    public int GetStepCount()
    {
        return stepCount;
    }
}
