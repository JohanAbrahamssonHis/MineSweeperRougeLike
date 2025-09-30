using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[CreateAssetMenu(menuName = "BossMod/DVD", fileName = "DVD")]
public class DVD : BossModification
{
    public float speed = 1;
    private Vector2 _direction;
    private Grid _grid;
    [SerializeField] private float MaxDistanceFromCenter = 3;
    private Vector2 _startPos = new Vector2();
    

    public override void Modification()
    {
        _direction = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
        _grid = RunPlayerStats.Instance.MineRoomManager.grid;
        _startPos = _grid.transform.position;
    }

    public override void UpdateModification()
    {
        _grid.transform.position += (Vector3)(_direction * (speed * Time.deltaTime));
        CheckHitDetection(_grid.transform.position.x, _startPos.x, ref _direction.x);
        CheckHitDetection(_grid.transform.position.y, _startPos.y, ref _direction.y);
    }

    public override string Description => "Grid moves and bounces around";

    private void CheckHitDetection(float pos, float sPos, ref float dir)
    {
        if ((pos > MaxDistanceFromCenter + sPos && dir>=0) || (pos < -MaxDistanceFromCenter + sPos && dir<=0)) dir *= -1;
    }
}
