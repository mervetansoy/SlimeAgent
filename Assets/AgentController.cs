using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentController : Agent
{
   
   [SerializeField] private Transform hedef;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(-17f,0.24999997f,-32f);

        int randm=Random.Range(0,2);
        if(randm==0)
        {
            hedef.localPosition=new Vector3(-14f,0.5f,-32f);
        }
        if( randm ==1)
        {
            hedef.localPosition=new Vector3(-19.5f,0.5f,-32f);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
       sensor.AddObservation(transform.localPosition);
       sensor.AddObservation(hedef.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float move = actions.ContinuousActions[0];
        float moveSpeed=2f;

        transform.localPosition +=new Vector3(move,0f) * Time.deltaTime*moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction=actionsOut.ContinuousActions;
        continuousAction[0]=Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter(Collider nesne)
 { 
    if(nesne.gameObject.tag =="Potion")
    {
        AddReward(10f);
        EndEpisode();
    }
    if (nesne.gameObject.tag == "Wall")
    {
        AddReward(-5f);
        EndEpisode();
    }
 }    
}
