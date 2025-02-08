using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpointManager : MonoBehaviour
{
    public List<CheckPoint> checkPoints;
    public event EventHandler<CarCheckPointEventArgs> OnPlayerCorrectCheckpoint;
    public event EventHandler<CarCheckPointEventArgs> OnPlayerWrongCheckpoint;

    [SerializeField] private List<Transform> RacersTransformList;
    
    private List<int> nextCheckpointIndexList;

    // Start is called before the first frame update
    void Start()
    {
        nextCheckpointIndexList = new List<int>();
        foreach (Transform racerTransform in RacersTransformList)
        {
            nextCheckpointIndexList.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Awake()
    {
        foreach(CheckPoint checkPoint in checkPoints) 
        {
            checkPoint.SetTrackCheckPoint(this);
        }
        nextCheckpointIndexList = new List<int>();
        foreach (Transform racerTransform in RacersTransformList)
        {
            nextCheckpointIndexList.Add(0);
        }
    }
    public void ResetCheckpoint(Transform racerTransform)
    {
        nextCheckpointIndexList[RacersTransformList.IndexOf(racerTransform)] = 0;
    }
    public void CarPassedCheckpoint(CheckPoint checkpoint, Transform racerTransform)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[RacersTransformList.IndexOf(racerTransform)];

        CarCheckPointEventArgs e = new CarCheckPointEventArgs
        {
            carTransform = racerTransform
        };

        if (checkPoints.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            nextCheckpointIndexList[RacersTransformList.IndexOf(racerTransform)] = (nextCheckpointIndex + 1) % checkPoints.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, e);
        }

        else
        {
            OnPlayerWrongCheckpoint?.Invoke(this, e);
        }
    }



    public class CarCheckPointEventArgs : EventArgs
    {
        public Transform carTransform { get; set; }
    }

    public void ResetCarChecpoint(Transform racerTransform)
    {
        nextCheckpointIndexList[RacersTransformList.IndexOf(racerTransform)] = 0;
    }

    public Transform GetNextCheckpoint(Transform racerTransform)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[RacersTransformList.IndexOf(racerTransform)];


        Transform NextCheckpointTransform = null;

        int count = 0;

        foreach (CheckPoint CheckpointTransform in checkPoints)
        {
            if (count == nextCheckpointIndex)
            {
                NextCheckpointTransform = CheckpointTransform.transform;
           
                break;
            }

            else
                count++;
        }

        return NextCheckpointTransform;

    }
}

