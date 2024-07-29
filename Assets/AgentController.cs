using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentController : Agent
{
   
   [SerializeField] private Transform hedef;
   [SerializeField] private float moveSpeed =4f;


   private Rigidbody rb;

    public override void Initialize()
    {
        rb=GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-20f,-12f), 0.25f, Random.Range(-27f,-35f));
        //hedef.localPosition=new Vector3(Random.Range(-20f,-12f), 0.25f, Random.Range(-27f,-35f));
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
       sensor.AddObservation(transform.localPosition);
       //sensor.AddObservation(hedef.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveRotate = actions.ContinuousActions[0];
        float moveForward = actions.ContinuousActions[1];

        rb.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, moveRotate * moveSpeed, 0f, Space.Self);

        /*
        Vector3 velocity=new Vector3(moveX,0f,moveZ) ;
        velocity = velocity.normalized * Time.deltaTime * moveSpeed;

        transform.localPosition +=velocity;
        */
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction=actionsOut.ContinuousActions;
        continuousAction[0]=Input.GetAxisRaw("Horizontal");
        continuousAction[1]=Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider nesne)
 { 
    if(nesne.gameObject.tag =="Potion")
    {
        AddReward(5f);
        EndEpisode();
    }
    if (nesne.gameObject.tag == "Wall")
    {
        AddReward(-1f);
        EndEpisode();
    }
 }    
}
