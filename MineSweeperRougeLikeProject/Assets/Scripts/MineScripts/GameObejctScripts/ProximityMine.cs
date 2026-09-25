using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class ProximityMine : Mine
{
    List<Color> colors = new List<Color>();
    public float duration = 3f;
    public float distance = 2f;
    public bool isExploding;

    public Sprite targetSprite;

    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);

        colors.Add(Color.white);
        colors.Add(Color.green);
        colors.Add(Color.yellow);
        colors.Add(new Color(1,1,0,1));
        colors.Add(Color.red);
    }

    public override void MineSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnSquareActivate += Reveal;
    }

    public override void MineUnSubscribe()
    {
        base.MineUnSubscribe();
        ActionEvents.Instance.OnSquareActivate -= Reveal;
    }

    public void Reveal(Vector2 position)
    {
        if(!neighbours.Contains(position) || isExploding) return;
        isExploding = true;
        mineRoomManager.RevealContainer(this.position);
        StartCoroutine(SetForExplode());
    }

    public IEnumerator SetForExplode()
    {
        float elapsed = 0f;
        bool shouldBeep = false;
        if(!transform.parent.TryGetComponent(out SpriteRenderer spriteRenderer)) {yield break;}

        GameObject targetGameObject = Instantiate(new GameObject(), transform);
        targetGameObject.transform.position = transform.parent.position;
        targetGameObject.transform.localScale *= 2*distance;
        SpriteRenderer targetSpriteRenderer = targetGameObject.AddComponent<SpriteRenderer>();
        targetSpriteRenderer.sprite = targetSprite;
        targetSpriteRenderer.sortingOrder = 3;

        while (elapsed < duration)
        {
            if(isActivated) break;
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            
            for(int i = 0; i < colors.Count; i++)
            {
                if((i+1)<t*colors.Count) continue;
                if(spriteRenderer.color != colors[i]) shouldBeep = true;
                spriteRenderer.color = colors[i];
                break;
            }

            if(shouldBeep)
            {
                SoundManager.Instance.Play("BoomBeep", null, true, 1f, 1.6f);
                shouldBeep = false;
            }
            
            yield return null;
        }
        //Change to conditional damage
        spriteRenderer.color = Color.white;

        Destroy(targetGameObject);

        Explode();
    }

    public void Explode()
    {
        SoundManager.Instance.Play("Explosion", null, true, 2f, 0.7f);
        Vector3 worldMousePos = RunPlayerStats.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
        if(Vector2.Distance(transform.parent.position, worldMousePos)<distance && !isActivated) Activate();
        mineRoomManager.grid.squares[mineRoomManager.GetPostion(position)].SetDisabled(true);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.parent.position, distance);
    }

}
