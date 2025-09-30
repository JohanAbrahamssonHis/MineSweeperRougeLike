using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectAbilityHolder : MonoBehaviour, IInteractable, ITextable
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererFlagContainer;
    private GameObject _decalHolder;
    private SpriteRenderer _spriteRendererCount;
    private EffectAbility _effectAbility;
    void Start()
    {
        _effectAbility = RunPlayerStats.Instance.currentEffectAbility;
        _decalHolder = transform.GetChild(1).transform.GetChild(0).gameObject;
        _spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _spriteRendererCount = transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>();
        _spriteRendererFlagContainer = _decalHolder.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.sprite = _effectAbility.sprite;
        _spriteRendererCount.gameObject.SetActive(!_effectAbility.isInfinite);
        _spriteRendererCount.sprite = NumberSprites.Instance.GetNumberedSprite(_effectAbility.count);
        _decalHolder.SetActive(_effectAbility is Flag);
        _spriteRendererFlagContainer.sprite = RunPlayerStats.Instance.FlagMineSelected == null ? null : RunPlayerStats.Instance.FlagMineSelected.sprite;
    }

    public void Interact()
    {
        _effectAbility =  RunPlayerStats.Instance.GetNextEffectAbility();
        RunPlayerStats.Instance.currentEffectAbility = _effectAbility;
        TextVisualSingleton.Instance.textVisualObject.SetObject(gameObject,_effectAbility);
    }

    public string Name => _effectAbility.Name;
    public string Description => _effectAbility.Description;
}
