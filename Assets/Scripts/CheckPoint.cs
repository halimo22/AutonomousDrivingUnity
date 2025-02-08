using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    TrackCheckpointManager checkpointManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CarController>(out CarController car))
        {
            checkpointManager.CarPassedCheckpoint(this, car.transform);
        }
    }
    public void SetTrackCheckPoint(TrackCheckpointManager checkpointManager)
    {
        this.checkpointManager = checkpointManager; 
    }


}
