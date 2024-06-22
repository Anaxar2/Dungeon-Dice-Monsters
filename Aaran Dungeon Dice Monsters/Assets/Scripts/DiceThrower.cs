using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DiceThrower : MonoBehaviour
{
    public Dice diceToThrow;
    public int amountOfDice = 3;
    public float throwForce = 5f;
    public float rollForce = 10f;

    private List<GameObject> spawnedDice = new List<GameObject>(); // keeps track of spawned dice and so we can destroy them.


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
    }

    private async void RollDice()
    {
        if (diceToThrow == null) return;

        foreach( var die in spawnedDice)
        {
            Destroy(die);
        }

        for(int i = 0; i < amountOfDice; i++)
        {
            Dice dice = Instantiate(diceToThrow, transform.position, transform.rotation);
            spawnedDice.Add(dice.gameObject);
            dice.RollDice(throwForce, rollForce, i);
            await Task.Yield();
        }
    }
}
