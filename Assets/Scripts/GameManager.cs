using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileMapManager tileMapManager;
    public Transform playerPosition;
    public CameraShake cameraShake;

    private AudioSource audioSource;

    private List<MazeDoorEnum> availableMazeDoors;
    private float timeUntilNextMazeChange;
    private float timeElapsedSinceLastMazeChange;
    private readonly float baseTimeUntilMazeChange = 20f;
    private readonly float acceptableDistance = 12f;

    //Percentage of available doors changed
    private readonly float difficulty = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        availableMazeDoors = new List<MazeDoorEnum>();

        audioSource = gameObject.GetComponent<AudioSource>();

        timeUntilNextMazeChange = baseTimeUntilMazeChange + Random.Range(1, 20);
        timeElapsedSinceLastMazeChange = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Change Maze at fixed intervals
        if (Time.realtimeSinceStartup - timeElapsedSinceLastMazeChange > timeUntilNextMazeChange)
        {
            //Maze Change Logic
            PopulateAvailableMazeDoors();
            UpdateMaze();

            //Sound and Camera shake
            PlayMazeShiftSound();
            Invoke("ShakeMazeCamera", 0.5f); //0.5s delay
            Invoke("PlayMazeShiftSound", 0.5f); //0.5s delay

            //Clear the available maze doors
            availableMazeDoors.Clear();

            //Store current time
            timeElapsedSinceLastMazeChange = Time.realtimeSinceStartup;
            //Get next time until maze change
            timeUntilNextMazeChange = baseTimeUntilMazeChange + Random.Range(1, 20);
        }
    }

    private void ShakeMazeCamera()
    {
        //CameraShake
        StartCoroutine(cameraShake.Shake(0.5f, 0.2f));
    }

    private void PlayMazeShiftSound()
    {
        //Play sound
        audioSource.PlayOneShot(audioSource.clip);
    }

    /// <summary>
    /// Update the maze
    /// </summary>
    private void UpdateMaze()
    {
        List<MazeDoorEnum> selectedMazeDoors = new List<MazeDoorEnum>();

        //Select some available maze doors
        bool add = Random.value < difficulty;
        foreach (MazeDoorEnum mazeDoor in availableMazeDoors)
        {
            if (add == true)
            {
                selectedMazeDoors.Add(mazeDoor);
            }
            add = Random.value < difficulty;
        }

        //Update each selected maze door
        foreach (MazeDoorEnum mazeDoor in selectedMazeDoors) 
        {
            tileMapManager.UpdateDoor(mazeDoor, MazeDoorState.Auto);
        }
    }

    /// <summary>
    /// PopulateAvailableMazeDoors populates the available list with Maze doors from tileMapManager.MazeDoor appropriately
    /// </summary>
    private void PopulateAvailableMazeDoors()
    {
        //Loop through tileMapManager MazeDoors and see if there are any doors that are a acceptable distance away from the player to add
        foreach (MazeDoorEnum mazeDoor in tileMapManager.MazeDoors.Keys)
        {
            if (!PlayerCloseToPostion(playerPosition.position, tileMapManager.MazeDoorPosition(mazeDoor)))
            {
                if (!availableMazeDoors.Contains(mazeDoor)) 
                {
                    availableMazeDoors.Add(mazeDoor);
                }
            }
        }
    }

    /// <summary>
    /// Determines whether the distance between two vector3's are within the acceptable range.
    /// </summary>
    /// <param name="playerPosition">Position of the player</param>
    /// <param name="position">Position to compare with</param>
    /// <returns>True or false</returns>
    private bool PlayerCloseToPostion(Vector3 playerPosition, Vector3Int position)
    {
        if (Vector3.Distance(playerPosition, position) < acceptableDistance)
        {
            return true;
        }
        return false;
    }
}