using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public GameObject comboText;
    public GameObject finishedFloor;

    //Ui
    public Button screenBtn;
    public GameObject scoreText;
    public GameObject heightText;
    public GameObject timeText;


    public AudioSource floorPopUp;
    public AudioClip floorPopUpSound;

    private int floorHeight = 1;
    private int floorFinished = 1;
    private int floorParts = 1;
    private int prevFloor = -1;
    private int combo = 1;
    private int score = 0;

    private int trenFloor = 1;
    private float period = 0.4f;
    private float nextActionTime = 0.0f;

    private int TimesClicked = 0;
    private int TimesMerged = 0;

    private float timeCounter;
    private float remaingTime;
    private bool finished = false;

    private bool started = false;

    //Camera
    private Vector3 cameraTarget = new Vector3(0, 6, -11);

    public GameObject[] floorList;
    // Start is called before the first frame update
    void Start()
    {

        timeCounter = 0.0f;
        remaingTime = 30f;
        Time.timeScale = 1;
        floorPopUp.clip = floorPopUpSound;
        int temp = 0;
        //Create initial objects for cycling floors
        foreach (GameObject go in floorList)
        {
            floorList[temp] = Instantiate(go, new Vector3(-0.045f, 0.986f * 2, 0.353f), Quaternion.Euler(-90, 180, 0));
            floorList[temp].SetActive(false);
            MeshRenderer r = floorList[temp].GetComponent<MeshRenderer>();
            Color newColor = r.material.color;
            newColor.a = 0.5f;
            r.material.color = newColor;
            temp++;
        }
        Button btn = screenBtn.GetComponent<Button>();
        btn.onClick.AddListener(onCLick);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeCounter)
        {
            timeCounter += 1f;
            if(remaingTime > 0 && started)
                remaingTime--;

            if (remaingTime == 0)
                TimeExpired();

            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = "TIME: " + remaingTime;
        }

        //Cycle floors
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            if (trenFloor > 0)
            {
                floorList[trenFloor - 1].SetActive(false);
                floorList[trenFloor].SetActive(true);
            }
            else
            {
                floorList[floorList.Length - 1].SetActive(false);
                floorList[trenFloor].SetActive(true);
            }

            trenFloor++;
            if (trenFloor > 3)
                trenFloor = 0;
        }

        moveCamera();
    }

    void TimeExpired()
    {
        Time.timeScale = 0;
        finished = true;
        screenBtn.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Time's up \n Press anywhere to start again";
    }

    void onCLick()
    {
        if(finished)
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (!started)
        {
            started = true;
            screenBtn.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }

        TimesClicked++;
        if (!PauseButton.paused)
        if(floorParts < 10)
        {
            trenFloor--;
            if (trenFloor < 0)
                trenFloor = 3;

            //Spawn new floor
            GameObject temp = Instantiate(floorList[trenFloor], new Vector3(-0.045f, 0.986f*(floorFinished + floorParts), 0.353f), Quaternion.Euler(-90, 180, 0));
            temp.SetActive(true);
            MeshRenderer r = temp.GetComponent<MeshRenderer>();
            Color newColor = r.materials[1].color;
            newColor.a = 1f;
            r.material.color = newColor;
            temp.tag = "Part";

            //Play sound
            floorPopUp.PlayOneShot(floorPopUpSound);

            //Increse combo
            if (prevFloor == trenFloor)
                combo++;
            else
                combo = 1;


            //Draw combo
            if(combo > 1)
            {
                var g = Instantiate(comboText, new Vector3(0, 0.986f * (floorFinished + floorParts), 0), Quaternion.Euler(0,0,0));
                g.GetComponent<TMPro.TextMeshPro>().text = "x" + combo.ToString();
                g.GetComponent<TMPro.TextMeshPro>().color = newColor;
            }

            //Add score
            score += combo * 10;

            prevFloor = trenFloor;
            floorParts++;
        } else
        {
            TimesMerged++;
            MergeDelete();

            floorParts = 1;

            GameObject temp = Instantiate(finishedFloor, new Vector3(-0.045f, 0.986f * (floorFinished + floorParts), 0.353f), Quaternion.Euler(-90, 180, 0));
            temp.SetActive(true);
            floorFinished++;



            //Camera.main.transform.Translate(0, 1, 0);
            cameraTarget = cameraTarget + new Vector3(0, 1, 0);

            score += combo * 100;
        }

        
        
        foreach (GameObject go in floorList)
        {
            go.transform.position = new Vector3(-0.045f, (floorFinished + floorParts)* 0.986f, 0.353f);
        }

        updateUI();
    }

    public void updateUI()
    {
        heightText.GetComponent<TMPro.TextMeshProUGUI>().text = "HEIGHT: " + (floorFinished - 1).ToString();
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "SCORE: " + (score).ToString();
    }

    public void moveCamera()
    {
        if (!Camera.main.transform.position.Equals(cameraTarget))
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraTarget, 2 * Time.deltaTime);
        }
    }

    void MergeDelete()
    {
        GameObject[] toDelete = GameObject.FindGameObjectsWithTag("Part");
        float t = 0;
        for(int i = toDelete.Length-1; i >= 0; i--)
        {
            toDelete[i].GetComponent<FloorScript>().DestroyAfter(t);
            t += 0.1f;
        }
        Destroy(toDelete[0]);

    }

}
