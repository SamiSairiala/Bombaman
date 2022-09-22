using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public interface IBomb
    {
        void DropBomb(GameObject BombPrefab, Transform DroppersTransform);


        //Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), Mathf.RoundToInt(myTransform.position.y), bombPrefab.transform.position.y), bombPrefab.transform.rotation);

        void ExplodeTimer(float timer);
        
    }
}
