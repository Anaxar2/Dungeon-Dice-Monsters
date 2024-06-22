using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    public Transform[] diceFaces;
    public Rigidbody rb;

    private int diceIndex = -1; // keeps track of which dice "this is". dice, 1, 2. 3 etc.

    private bool hasStoppedRolling;
    private bool delayFinished; 

    //public float throwForce = 5f;
    //public float rollForce = 10f;

    public static UnityAction<int, int> OnDiceResult; // keeps track of which dice is which and what result to assign to each dice. 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!delayFinished) return;

        if(!hasStoppedRolling && rb.velocity.sqrMagnitude == 0f)
        {
            hasStoppedRolling = true;
            GetNumberOnTopFace();
        }
    }

    [ContextMenu("Get Top Face")]
    private int GetNumberOnTopFace()
    {
        if (diceFaces == null) return -1; // null result

        var topFace = 0; // by defaults setting top face to 1 (1 is element 0 in the array)  
        var lastYPosition = diceFaces[0].position.y; //   
         
        for(int i = 0; i < diceFaces.Length; i++)
        {
            if(diceFaces[i].position.y > lastYPosition) 
            {
                lastYPosition = diceFaces[i].position.y;
                topFace = i;
            }
        }

        Debug.Log($"Dice result {topFace + 1}");

        OnDiceResult?.Invoke(diceIndex, topFace + 1);
        return topFace + 1;
    }

    public void RollDice(float throwForce, float rollForce, int i)
    {
        diceIndex = i;
        var randomVariance = Random.Range(-1f, 1f);
        rb.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.Impulse);

        var randX = Random.Range(0f, 1f);
        var randY = Random.Range(0f, 1f);
        var randZ = Random.Range(0f, 1f);

        rb.AddTorque(new Vector3(randX, randY, randZ) * (rollForce + randomVariance), ForceMode.Impulse);

        DelayResult();

        }

    private async void DelayResult()
    {
        await Task.Delay(1000);
        delayFinished = true;
    }
}
