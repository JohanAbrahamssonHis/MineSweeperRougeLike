using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMine : Mine
{
    public float runTime = 15;
    private float currentTime;
    private float timeBank;
    public override void MineUpdate()
    {
        base.MineUpdate();
        Run();
    }

    public void Run()
    {
        if(!RunPlayerStats.Instance.ActiveTimer) return;
        currentTime += Time.deltaTime;
        if (currentTime < runTime+timeBank || Context.isDisabled || Context.isActivated)return;

        List<Vector2> tempNeighbours = new(Context.neighbours);
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;

        for (int i = tempNeighbours.Count - 1; i >= 0; i--)
        {
            int randomNeighbour = Random.Range(0, tempNeighbours.Count);
            int moveResult = mineRoomManager.TryMoveMine(this, tempNeighbours[randomNeighbour]);
            if (moveResult == 0)
            {
                tempNeighbours.RemoveAt(randomNeighbour);
            }
            else if (moveResult == -1)
            {
                return;
            }
        }

        timeBank=currentTime;
    }
}
