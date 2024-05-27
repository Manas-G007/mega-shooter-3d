using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public void Initialise() {
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (activeState != null) activeState.Perform();
        }
        catch
        {
            Debug.Log("killed");
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null) activeState.Exit();
        activeState = newState;
        if (activeState != null){
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        };
    }
}