using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Singletons/NumberSprites", fileName = "NumberSprites")]
public class NumberSprites : ScriptableObject
{
   private static NumberSprites _instance;

   public static NumberSprites Instance
   {
      get
      {
         if (_instance == null) _instance = Resources.Load<NumberSprites>("Singletons/NumberSprites");
         return _instance;
      }
   }

   [SerializeField] private List<Sprite> _sprites;

   public Sprite GetNumberedSprite(int value)
   {
      //The -9 and +25 is due to that is the limit, the +9 to values is to skip the negative numbers
      return value <= -9 ? _sprites.First() : value >= 25 ? _sprites.Last() : _sprites[value+9];
   }
}
