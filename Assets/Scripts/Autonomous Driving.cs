using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AutonomousDriving : Agent
{

    [SerializeField] private TrackCheckpointManager trackCheckpointManager;
    [SerializeField] private Transform carSpawnPoint;
    [SerializeField] float forwardMovementAction;
    [SerializeField] float steeringMovementAction;

    [SerializeField] private CarController carController;
    private Rigidbody rb;
    Vector3 initpos;
    Vector3 initfor;
    private void Start()
    {
        trackCheckpointManager.OnPlayerCorrectCheckpoint += CheckpointTracker_OnCorrectCheckpoint;
        trackCheckpointManager.OnPlayerWrongCheckpoint += CheckpointTracker_OnWrongCheckpoint;
        initpos=transform.position;
        initfor=transform.forward;
        carController=GetComponent<CarController>();
        rb=GetComponent<Rigidbody>();
        

    }
    private void CheckpointTracker_OnCorrectCheckpoint(object Sender, TrackCheckpointManager.CarCheckPointEventArgs e)
    {
        if (e.carTransform == transform)
        {
            AddReward(1f);

        }
    }

    private void CheckpointTracker_OnWrongCheckpoint(object Sender, TrackCheckpointManager.CarCheckPointEventArgs e)
    {
        if (e.carTransform == transform)
        {
            AddReward(-1f);
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position=initpos+new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f));
        carController.StopVehicleCompletely();
        transform.forward=initfor;
        trackCheckpointManager.ResetCheckpoint(transform);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 nextCheckpoint = trackCheckpointManager.GetNextCheckpoint(transform).transform.forward;
        float DirectionDot = Vector3.Dot(transform.forward, nextCheckpoint);
        sensor.AddObservation(DirectionDot);
        sensor.AddObservation(rb.velocity.magnitude);
        sensor.AddObservation(rb.angularVelocity.magnitude);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
        forwardMovementAction =Input.GetAxis("Horizontal");     
        steeringMovementAction = Input.GetAxis("Vertical");
        ActionSegment<float> contActions = actionsOut.ContinuousActions;
        contActions[0] = forwardMovementAction;
        contActions[1] = steeringMovementAction;
        carController.GetMovementInput();
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        carController.horizontalInput =Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f); 
        carController.verticalInput =Mathf.Clamp(actions.ContinuousActions[1], -1f, 0.5f);
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Wall>(out Wall wall)) 
        {
            AddReward(-0.1f);
        }
    }
    private void punishstopping()
    {
        if(rb.velocity.magnitude<=0.001)
        {
            AddReward(-0.01f);
        }
    }
    private void Update()
    {
        punishstopping();
        ifFlipped();
        checkSpeed();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            AddReward(-0.5f);
            EndEpisode();
        }
    }
    void ifFlipped()
    {
        float zRotation = transform.eulerAngles.z;

        
        if (zRotation > 180)
        {
            zRotation -= 360;
        }

        if (Mathf.Abs(zRotation) > 50)
        {
            Debug.Log("Car Flipped");
            AddReward(-0.5f);
            EndEpisode();
        }
    }
    void checkSpeed()
    {
        if(rb.velocity.magnitude>17.0f)
        {
            AddReward(-0.1f);

        }
    }
}

