using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof (ThirdPersonCharacter))]

public class EnemyController : MonoBehaviour {
    [SerializeField]
    Transform target;
    bool isActive = false;
    NavMeshAgent agent;
    ThirdPersonCharacter character;

    void Start () {
        agent = GetComponent<NavMeshAgent> ();
        character = GetComponent<ThirdPersonCharacter> ();
        agent.updateRotation = false;
        agent.updatePosition = true;
        isActive = true;
    }

    private void Update () {
        if (isActive) {
            if (target != null)
                agent.SetDestination (target.position);
            if (agent.remainingDistance > agent.stoppingDistance) {
                character.Move (agent.desiredVelocity, false, false);
            } else {
                character.Move (Vector3.zero, false, false);
            }
        } else {
            return;
        }

    }

    public void SetTarget (Transform target) {
        this.target = target;
    }

    void OnCollisionEnter (Collision other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log ("ゲームオーバーです。");
            isActive = false;
        }
    }
}
