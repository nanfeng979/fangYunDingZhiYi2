using UnityEngine;
using Y9g;

public class GamePlayingManager : Singleton<GamePlayingManager>
{
   [SerializeField]
   private GameObject TiesList;

   public GameObject GetTiesListGameObject()
   {
       return TiesList;
   }
}