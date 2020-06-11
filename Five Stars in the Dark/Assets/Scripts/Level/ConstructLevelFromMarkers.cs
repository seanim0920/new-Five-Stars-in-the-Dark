﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ConstructLevelFromMarkers : MonoBehaviour
{
    public static AudioSource levelDialogue;
    public AudioSource secondSource;
    private GameObject player;

    private AudioClip carStart;
    private AudioClip carPark;

    List<Marker> timedObstacleMarkers = new List<Marker>();
    List<Marker> commandMarkers = new List<Marker>();
    List<Marker> dialogueMarkers = new List<Marker>();
    List<Marker> subtitleMarkers = new List<Marker>();
    Dictionary<GameObject, float> spawnedObstacles = new Dictionary<GameObject, float>();
    
    //variables set from the player object
    private SteeringWheelInput wheelFunctions;
    private PlayerControls controls;
    private KeyboardControl keyboard;
    private GamepadControl gamepad;
    private static int controlType; // 0 = Steering Wheel
                                    // 1 = Keyboard
                                    // 2 = Gamepad

    //for lowering the volume when dialogue is playing
    //public Transform leftSpeaker;
    //public Transform rightSpeaker;
    private bool isSpeaking;
    private string[] dialogueInstruments = { "Drums", "Support", "Wind" };
    public float maxVol = 0.8f;

    //for the start cutscene
    private GameObject skipText;
    public AudioSource ambience;
    public Image blackScreen;
    public TextAsset markersFile;
    public static string debugMessage { get; set; }
    public static string subtitleMessage { get; set; } = "";

    float numberOfLanes = 3;
    float laneWidth = 1.8f * 20;
    float roadWidth = 1.8f * 3 * 20;

    //this is public so dialogue rewinding scripts know where to rewind too.
    private float currentDialogueStartTime = 0.0f;
    private float currentDialogueEndTime = 0.0f;
    private float nextDialogueStartTime = 0.0f;
    //this is public so error checking knows how far the player got
    private static float startOfLevel = 0f;
    private static float endOfLevel = 0f;

    bool skipSection = false;

    GameObject nextDialogueTrigger;
    GameObject dialogueStopper;

    Object[] loadedObjects;

    struct Marker
    {
        public float spawnTime;
        public float despawnTime;
        public string data;
        public Marker(float spawnTime, float despawnTime, string data)
        {
            this.spawnTime = spawnTime;
            this.despawnTime = despawnTime;
            this.data = data;
        }
    }

    void sortedMarkerInsert(List<Marker> list, Marker newmarker)
    {
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
            if (newmarker.spawnTime < list[i].spawnTime)
            {
                break;
            }
        }
        list.Insert(i, newmarker);
    }

    public static float getProgress()
    {
        return (levelDialogue.time - startOfLevel) / (endOfLevel - startOfLevel) * 100;
    }

    void parseLevelMarkers()
    {
        timedObstacleMarkers = new List<Marker>();
        dialogueMarkers = new List<Marker>();
        spawnedObstacles = new Dictionary<GameObject, float>();
        levelDialogue = GetComponent<AudioSource>();

        string[] lines = markersFile.text.Split('\n');

        int lineNumber = 1;
        foreach (string line in lines)
        {
            string[] tokens = line.Split(new char[0], System.StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 3) continue;
            float spawnTime = float.Parse(tokens[0].Trim());
            if (spawnTime < 0) spawnTime = 0;
            if (spawnTime > levelDialogue.clip.length) spawnTime = levelDialogue.clip.length;
            float despawnTime = float.Parse(tokens[1].Trim());
            if (despawnTime < spawnTime) despawnTime = spawnTime;
            //markers will either be obstacles/dialogue, or news/realtime events
            if (tokens.Length == 3)
            {
                Marker newMarker = new Marker(spawnTime, despawnTime, tokens[2].Trim());
                if (newMarker.data[0] == '[')
                {
                    sortedMarkerInsert(commandMarkers, newMarker);
                    if (string.Equals(newMarker.data, "[StartControl]") || string.Equals(newMarker.data, "[StartCar]"))
                    {
                        startOfLevel = newMarker.spawnTime;
                    }
                    if (string.Equals(newMarker.data, "[EndControl]"))
                    {
                        endOfLevel = newMarker.spawnTime;
                    }
                }
                else if (newMarker.despawnTime - newMarker.spawnTime > 0.1f) //must have this check because it breaks for dialogue markers of length 0
                {
                    sortedMarkerInsert(dialogueMarkers, newMarker);
                }
                
                Debug.Log(newMarker.data);
            } else
            {
                Marker newMarker = new Marker(spawnTime, despawnTime, string.Join(" ", tokens, 2, tokens.Length - 2));
                if (newMarker.data[0] == '"' || newMarker.data[0] == '<')
                {
                    sortedMarkerInsert(subtitleMarkers, newMarker);
                } else
                {
                    sortedMarkerInsert(timedObstacleMarkers, newMarker);
                }
                Debug.Log(newMarker.data);
            }
            //print("amount of tokens are " + tokens.Length);
            lineNumber++;
        }
    }

    [ContextMenu("Construct Level")]
    void constructLevel()
    {
        parseLevelMarkers();

        constructLevelMap(0);
    }

    void Awake()
    {
        PlaythroughManager.currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        ScoreStorage.Instance.resetScore();
        loadedObjects = Resources.LoadAll("Prefabs/Obstacles", typeof(GameObject));
        player = GameObject.Find("Player");
        wheelFunctions = player.GetComponent<SteeringWheelInput>();
        controls = player.GetComponent<PlayerControls>();
        keyboard = player.GetComponent<KeyboardControl>();
        gamepad = player.GetComponent<GamepadControl>();
        skipText = GameObject.Find("SkipText");
        parseLevelMarkers();

        if (!blackScreen.enabled)
        {
            enableControllers();
        } else
        {
            disableControllers();
        }

        carStart = Resources.Load<AudioClip>("Audio/Car-SFX/Car Ambience/Car-EngineStart");
        carPark = Resources.Load<AudioClip>("Audio/Car-SFX/Car Ambience/Car-EngineStart");


        debugMessage = "starting level now, level ends at " + endOfLevel;
        subtitleMessage = "";
        if (endOfLevel == 0)
        {
            endOfLevel = levelDialogue.clip.length;
        }
        //initial parsing of the theoretical fastest level time, for the sake of score calculation
        //should subtract startleveltime from endleveltime
        ScoreStorage.Instance.setScorePar((int)endOfLevel * 100);


        StartCoroutine(lockWheel());
        StartCoroutine(playLevel());
    }

    public void setControls()
    {
        if(!blackScreen.enabled)
        {
            enableControllers();
        }
    }

    public void enableControllers()
    {
        if (SettingsManager.toggles[0])
        {
            // Idk how else to do all this because apparently these things
            // become null if you reference them when they're disabled
            if(gamepad != null)
            {
                gamepad.enabled = false;
            }
            if(keyboard != null)
            {
                keyboard.enabled = false;
            }
            if(wheelFunctions != null)
            {
                wheelFunctions.enabled = true;
            }
        }
        else if (SettingsManager.toggles[2])
        {
            if(wheelFunctions != null)
            {
                wheelFunctions.enabled = false;
            }
            if(keyboard != null)
            {
                keyboard.enabled = false;
            }
            if(gamepad != null)
            {
                gamepad.enabled = true;
            }
        }
        else
        {
            if(wheelFunctions != null)
            {
                wheelFunctions.enabled = false;
            }
            if(gamepad != null)
            {
                gamepad.enabled = false;
            }
            if(keyboard != null)
            {
                keyboard.enabled = true;
            }
        }
    }

    void disableControllers()
    {
        wheelFunctions.enabled = false;
        gamepad.enabled = false;
        keyboard.enabled = false;
    }

    void constructLevelMap(int curbType)
    {
        float updateRate = 50; //how long fixedupdate runs per second

        float length = levelDialogue.clip.length * controls.neutralSpeed * updateRate;
        GameObject road = Resources.Load<GameObject>("Prefabs/Road"); GameObject map = new GameObject("Map");
        GameObject roadtile = Instantiate(road, new Vector3(0, 0, 1), Quaternion.identity);
        roadtile.transform.localScale = new Vector3(roadWidth, length, 1);
        roadtile.transform.parent = map.transform;
        if (curbType == 0)
        {
            GameObject curb = Resources.Load<GameObject>("Prefabs/Curb");
            GameObject leftcurb = Instantiate(curb, new Vector3((-roadWidth / 2 - 0.5f), 0, 1), Quaternion.identity);
            leftcurb.transform.localScale = new Vector3(20, length, 1);
            leftcurb.transform.parent = map.transform;
            GameObject rightcurb = Instantiate(curb, new Vector3((roadWidth / 2 + 0.5f), 0, 1), Quaternion.identity);
            rightcurb.transform.localScale = new Vector3(20, length, 1);
            rightcurb.transform.parent = map.transform;
            player.transform.position = new Vector3(0, -length / 2, 0);
        }
        else
        {
            GameObject curb = Resources.Load<GameObject>("Prefabs/Obstacles/TutorialCurb");
            GameObject leftcurb = Instantiate(curb, new Vector3((-roadWidth / 2 - 0.5f), 0, 1), Quaternion.identity);
            leftcurb.transform.localScale = new Vector3(20, length, 1);
            leftcurb.transform.parent = map.transform;
            GameObject rightcurb = Instantiate(curb, new Vector3((roadWidth / 2 + 0.5f), 0, 1), Quaternion.identity);
            rightcurb.transform.localScale = new Vector3(20, length, 1);
            rightcurb.transform.parent = map.transform;
            player.transform.position = new Vector3(0, -length / 2, 0);
        }
    }

    void replaceMarker(float resumeTime)
    {
        int updateRate = 50;
        if (nextDialogueTrigger != null) Destroy(nextDialogueTrigger);
        //create a physical marker that must be hit before the next piece of dialogue can play
        nextDialogueTrigger = Instantiate(Resources.Load<GameObject>("Prefabs/DisposableTrigger"), player.transform.position + new Vector3(0, (resumeTime - levelDialogue.time) * controls.maxSpeed * updateRate - 10, 1), Quaternion.identity);
    }

    IEnumerator playLevel()
    {
        levelDialogue.Play();
        nextDialogueStartTime = levelDialogue.clip.length;
        print("current level time is " + levelDialogue.time);
        //perform these checks every frame for as long as the dialogue plays
        while (dialogueMarkers.Count > 0 || timedObstacleMarkers.Count > 0 || commandMarkers.Count > 0 || levelDialogue.isPlaying || levelDialogue.time != 0)
        {
            //print("More dialogue markers? " + (dialogueMarkers.Count > 0));
            //print("More time obstacle markers? " + (timedObstacleMarkers.Count > 0));
            //print("More command markers" + (commandMarkers.Count > 0));
            //print("Playing level dialogue? " + levelDialogue.isPlaying);
            yield return new WaitForSeconds(0);

            if (dialogueMarkers.Count > 0 && !isSpeaking && nextDialogueTrigger == null)
            {
                //figure out when the current dialogue section ends and the next starts
                currentDialogueStartTime = dialogueMarkers[0].spawnTime;
                currentDialogueEndTime = dialogueMarkers[0].despawnTime;

                //start playing the dialogue from wherever it left off
                // Debug.Log("Is this what gets called after the tableaux?");
                // Debug.Break();
                levelDialogue.time = currentDialogueStartTime;
                levelDialogue.Play();
                isSpeaking = true;
                // Debug.Log("Setting speaking to true" + isSpeaking);

                if (dialogueMarkers.Count > 1)
                {
                    nextDialogueStartTime = dialogueMarkers[1].spawnTime;
                    replaceMarker(nextDialogueStartTime);
                }
                dialogueMarkers.RemoveAt(0);
                print("playing current dialogue at " + dialogueMarkers.Count);
            }
            else
            {
                if (levelDialogue.time >= currentDialogueEndTime)
                {
                    isSpeaking = false;
                    //the next dialogue could start at the same moment the current dialogue ends, so a <= is needed.
                    if (nextDialogueStartTime >= currentDialogueEndTime && levelDialogue.time >= nextDialogueStartTime) //fixes the level ending bug since nextdialoguestarttime isn't changed on the last item of dialogueMarkers
                    {
                        levelDialogue.Pause();
                    }
                }
            }

            if (dialogueStopper != null && !dialogueStopper.CompareTag("Car"))
            {
                print("dialogue should be stopped here");
                bool wasPlaying = levelDialogue.isPlaying;
                levelDialogue.Pause();
                while (dialogueStopper != null && !dialogueStopper.CompareTag("Car") && !skipSection)
                {
                    yield return new WaitForSeconds(0);
                }
                if (dialogueStopper != null && !dialogueStopper.CompareTag("Car")) Destroy(dialogueStopper);
                print("dialogue is good to go");
                controls.enabled = true;
                enableControllers();
                replaceMarker(nextDialogueStartTime);
                if (wasPlaying)
                {
                    while (PlayError.playingHurtSound == true)
                    {
                        yield return new WaitForSeconds(0);
                    }
                    print("target car finished, playing current dialogue at " + dialogueMarkers.Count);
                    levelDialogue.Play();
                }
                else
                {
                    while (PlayError.playingHurtSound == true)
                    {
                        yield return new WaitForSeconds(0);
                    }
                    print("This part gets run after activating the guardrail tableaux in tutorial " + dialogueMarkers.Count);
                    levelDialogue.Play();
                }
            }

            //check list of markers to see if the next command is due
            if (commandMarkers.Count > 0)
            {
                Marker commandMarker = commandMarkers[0];
                string command = commandMarker.data;
                if (levelDialogue.time >= commandMarker.spawnTime)
                {
                    debugMessage += "parsing command: " + command;
                    AudioClip radioClip = Resources.Load<AudioClip>("LevelFiles/" + SceneManager.GetActiveScene().name + "/" + command.Trim('[', ']'));
                    print("Looking for audiofile" + SceneManager.GetActiveScene().name + "/" + command.Trim('[', ']'));
                    if (radioClip != null)
                    {
                        secondSource.clip = radioClip;
                        secondSource.Play();
                    }
                    else if (string.Equals(command, "[RevealScreen]") || string.Equals(command, "[ShowUI]"))
                    {
                        enableControllers();
                        blackScreen.enabled = false;
                    }
                    else if (string.Equals(command, "[HideScreen]") || string.Equals(command, "[HideUI]"))
                    {
                        disableControllers();
                        blackScreen.enabled = true;
                    }
                    else if (string.Equals(command, "[StartCar]") || string.Equals(command, "[StartControl]"))
                    {
                        print("next dialogue start time is " + nextDialogueStartTime);
                        replaceMarker(nextDialogueStartTime);
                        print("started car at time " + levelDialogue.time);
                        StartCoroutine(startCar());
                    }
                    else if (string.Equals(command, "[EndControl]"))
                    {
                        // Debug.Log("dialogue time: " + levelDialogue.time);
                        // Debug.Log("end control time: " + commandMarker.spawnTime);
                        print("ending player control");
                        // Debug.Break();
                        StartCoroutine(parkCar());
                    }
                    else if (string.Equals(command, "[ConstructMap]"))
                    {
                        // Debug.Break();
                        StartCoroutine(ConstructMap(1));
                    }
                    commandMarkers.RemoveAt(0);
                }
            }

            //check list of markers to see if the next subtitle is due
            updateSubtitle();

            //check list of markers to see if the next obstacle is due
            if (timedObstacleMarkers.Count > 0)
            {
                Marker obstacleMarker = timedObstacleMarkers[0];
                print("trying to spawn obstacle at time " + obstacleMarker.spawnTime);
                print("level dialogue playing? " + levelDialogue.isPlaying);

                //if the next obstacle is due or if the obstacle trigger was touched, spawn it
                if (obstacleMarker.spawnTime < levelDialogue.time)
                {
                    debugMessage += "spawning obstacles: " + obstacleMarker.data;
                    spawnObstacles(obstacleMarker.despawnTime, obstacleMarker.data);
                    timedObstacleMarkers.RemoveAt(0);
                } else if (!isSpeaking)
                {
                    obstacleMarker.spawnTime -= Time.deltaTime;
                }
            }

            //check all active obstacles to see if any should be despawned
            foreach (KeyValuePair<GameObject, float> pair in spawnedObstacles)
            {
                if (levelDialogue.time >= pair.Value)
                {
                    GameObject obj = pair.Key;
                    debugMessage += "despawning obstacle: " + obj.name;

                    if (obj.transform.position.x > player.transform.position.x)
                        obj.transform.Rotate(0, 0, -45);
                    else
                        obj.transform.Rotate(0, 0, 45);
                    if (!pair.Key.CompareTag("Stopped"))
                    {
                        obj.GetComponent<CapsuleCollider2D>().isTrigger = true;
                        Destroy(pair.Key, 5);
                    }

                    spawnedObstacles.Remove(obj);
                    // Debug.Break();
                    break;
                }
                //obstacles can spawn prematurely, but not despawn prematurely
            }

            //for debugging
            if (skipSection)
            {
                skipSection = false;
                Destroy(nextDialogueTrigger);
                foreach (KeyValuePair<GameObject, float> pair in spawnedObstacles)
                {
                    Destroy(pair.Key);
                }
                spawnedObstacles.Clear();

                isSpeaking = false;
            }
        }

        print("level ended");
        //This is where the level ends
        ScoreStorage.Instance.setScoreAll();
        MasterkeyEndScreen.currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
        ScoreStorage.Instance.setScoreProgress(100);

        if (!PlaythroughManager.hasPlayedLevel(SceneManager.GetActiveScene().buildIndex))
        {
            PlaythroughManager.saveLevelHistory(SceneManager.GetActiveScene().buildIndex);
        }

        if (!GameObject.Find("Cutscene Camera"))
        {
            if (SceneManager.GetActiveScene().name == "Level 4.5" ||
                SceneManager.GetActiveScene().name == "Tutorial" ||
                SceneManager.GetActiveScene().name == "Level 5" ||
                SceneManager.GetActiveScene().name.Contains("Cutscene")) //mini-levels will be less than 3 minutes
            {
                LoadScene.LoadNextSceneWithoutWipe();
            }
            else if (SceneManager.GetActiveScene().name.Contains("Ending"))
            {
                LoadScene.Loader("Credits");
            }
            else
            {
                LoadScene.Loader("EndScreen");
            }
        }
    }

    void spawnObstacles(float despawnTime, string obstacleData)
    {
        string[] obstacleSeq = obstacleData.Split(',');
        foreach (string obstacle in obstacleSeq)
        {
            //instantiate the obstacles plotted at this time

            string[] tokens = obstacle.Trim().Split(new char[] { ' ', '\t' });
            float spawnDistance = 200;

            string prefab = "";
            foreach (var obj in loadedObjects)
            {
                if (string.Equals(obj.name, tokens[0].Trim(), System.StringComparison.OrdinalIgnoreCase))
                {
                    // Debug.Log("found obstacle");
                    prefab = obj.name;
                }
            }
            if (prefab == "") { Debug.Log("could not load obstacle"); break; }

            if (tokens.Length == 2)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Obstacles/" + prefab),
                    new Vector3(player.transform.position.x, player.transform.position.y + spawnDistance, 0),
                    Quaternion.identity);
                if (nextDialogueTrigger != null) //checks whether trigger was already hit, if so spawn another one and spawn it further ahead. not the best programming practice but itll do for now.
                    Destroy(nextDialogueTrigger);
                print("paused dialogue here");
                if ((string.Equals(prefab, "quickturn", System.StringComparison.OrdinalIgnoreCase)))
                {
                    if (tokens.Length > 1)
                    {
                        if ((string.Equals(tokens[1].Trim(), "right", System.StringComparison.OrdinalIgnoreCase)))
                        {
                            obj.GetComponent<QuickTurn>().mustTurnLeft = false;
                        }
                        else
                        {
                            obj.GetComponent<QuickTurn>().mustTurnLeft = true;
                        }
                    }
                }
                else if ((string.Equals(prefab, "stoplight", System.StringComparison.OrdinalIgnoreCase)))
                {
                    if (tokens.Length > 1)
                    {
                        string pattern = tokens[1].ToLower().Trim();
                        obj.GetComponent<Stoplight>().pattern = pattern;
                    }
                }
                else if ((string.Equals(prefab, "target", System.StringComparison.OrdinalIgnoreCase)))
                {
                    if (tokens.Length > 1)
                    {
                        string pattern = tokens[1].ToLower().Trim();
                        obj.GetComponent<TargetMovement>().sequence = pattern;
                    }
                }
                else if ((string.Equals(prefab, "revealtableaux", System.StringComparison.OrdinalIgnoreCase)))
                {
                    if (tokens.Length > 1)
                    {
                        string tableauxName = tokens[1].ToLower().Trim();

                        obj.GetComponent<DisplayStrafeTableaux>().tableauxNum = int.Parse(tableauxName);
                    }
                }
                else if ((string.Equals(prefab, "incomingcar", System.StringComparison.OrdinalIgnoreCase)))
                {
                    if (tokens.Length > 1)
                    {
                        float xpos = tokens[1].ToLower()[0] == 'l' ? (-roadWidth + laneWidth) / 2 + (laneWidth * (float.Parse(tokens[1].Substring(4)) - 1)) :
                        tokens[1].ToLower()[0] == 'r' ? (-roadWidth + laneWidth) / 2 + (laneWidth * Random.Range(0, numberOfLanes)) :
                        tokens[1].ToLower().Trim() == "playersleft" && player.transform.position.x > (-roadWidth + laneWidth) / 2 ? player.transform.position.x - laneWidth :
                        tokens[1].ToLower().Trim() == "playersright" && player.transform.position.x < (roadWidth + laneWidth) / 2 ? player.transform.position.x + laneWidth :
                        player.transform.position.x;

                        obj.transform.position = new Vector3(xpos, player.transform.position.y + spawnDistance, 0);

                        Vector3 startRotation = new Vector3(0, 0, 180);
                        // Quaternion startQuaternion;
                        // startQuaternion.eulerAngles = startRotation;
                        obj.transform.eulerAngles = startRotation;
                    }
                }
                dialogueStopper = obj;
            }
            else
            {
                float xpos = tokens[2].ToLower()[0] == 'l' ? (-roadWidth + laneWidth) / 2 + (laneWidth * (float.Parse(tokens[2].Substring(4)) - 1)) :
                    tokens[2].ToLower()[0] == 'r' ? (-roadWidth + laneWidth) / 2 + (laneWidth * Random.Range(0, numberOfLanes)) :
                    tokens[2].ToLower().Trim() == "playersleft" && player.transform.position.x > (-roadWidth + laneWidth) / 2 ? player.transform.position.x - laneWidth :
                    tokens[2].ToLower().Trim() == "playersright" && player.transform.position.x < (roadWidth + laneWidth) / 2 ? player.transform.position.x + laneWidth :
                    player.transform.position.x;
                float ypos = player.transform.position.y + (tokens[1].ToLower()[0] == 'a' || tokens[1].ToLower()[0] == 'f' ? spawnDistance : -spawnDistance);
                //print(tokens[0].Trim());
                Quaternion rotation = Quaternion.identity;
                // if((string.Equals(prefab, "incomingcar", System.StringComparison.OrdinalIgnoreCase)))
                // {
                //     rotation = Quaternion.Euler(0f, 0f, 135f); // For some reason this turns cars 180 degrees instead
                // }
                spawnedObstacles.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Obstacles/" + prefab),
                    new Vector3(xpos, ypos, 0),
                    rotation), despawnTime);
            }
        }
    }

    private void updateSubtitle()
    {
        if (subtitleMarkers.Count > 0)
        {
            Marker subtitleMarker = subtitleMarkers[0];

            //if the next obstacle is due or if the obstacle trigger was touched, spawn it
            if (levelDialogue.time >= subtitleMarker.spawnTime)
            {
                subtitleMessage = subtitleMarker.data;
                if (levelDialogue.time >= subtitleMarker.despawnTime)
                {
                    if (subtitleMarkers.Count > 1)
                    {
                        if (subtitleMarkers[1].spawnTime - subtitleMarker.despawnTime > 1)
                        {
                            StartCoroutine(emptySubtitleAfterDelay());
                        }
                    }
                    subtitleMarkers.RemoveAt(0);
                }
                else if (!levelDialogue.isPlaying && levelDialogue.time - subtitleMarker.despawnTime < 1)
                {
                    subtitleMessage = "";
                    subtitleMarkers.RemoveAt(0);
                }
            }
        }
    }

    private IEnumerator emptySubtitleAfterDelay()
    {
        yield return new WaitForSeconds(1);
        subtitleMessage = "";
    }

    IEnumerator startCar()
    {
        if (!PlaythroughManager.hasPlayedLevel(SceneManager.GetActiveScene().buildIndex))
        {
            PlaythroughManager.saveLevelHistory(SceneManager.GetActiveScene().buildIndex);
        }
        if (skipText != null)
        {
            skipText.GetComponent<Text>().enabled = false;
        }
        ambience.Play();
        CountdownTimer.setTracking(true); //marks when the level is commanded to start
        yield return new WaitForSeconds(1);
        secondSource.PlayOneShot(carStart);
        StartCoroutine(wheelRumble());
        yield return new WaitForSeconds(1);
        controls.enabled = true;
        CountdownTimer.decrementTime(2); //to make up for the two seconds took to start the engine
    }
    IEnumerator parkCar()
    {
        controls.parkCar();
        CountdownTimer.setTracking(false);
        secondSource.PlayOneShot(carPark);
        yield return new WaitForSeconds(1);
        //blackScreen.CrossFadeAlpha(0, 3.0f, false);
    }

    IEnumerator lockWheel()
    {
        while (!controls.enabled)
        {
            wheelFunctions.PlaySoftstopForce(100);
            yield return new WaitForSeconds(0);
        }
        wheelFunctions.StopSoftstopForce();
    }

    IEnumerator wheelRumble()
    {
        for (int loop = 0; loop < 25; loop++)
        {
            wheelFunctions.PlayDirtRoadForce(loop * 2);
            yield return new WaitForSeconds(0);
        }
        for (int loop = 50; loop > 10; loop--)
        {
            wheelFunctions.PlayDirtRoadForce(loop);
            yield return new WaitForSeconds(0);
        }
        wheelFunctions.StopDirtRoadForce();
    }
    IEnumerator ConstructMap(int curbType)
    {
        constructLevelMap(curbType);
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Backslash) && Input.GetKeyDown(KeyCode.Comma) && Input.GetKeyDown(KeyCode.T)))
        {
            skipSection = true;
        }
    }
}