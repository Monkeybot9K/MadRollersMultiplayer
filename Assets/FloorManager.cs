using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {

	[SerializeField] Floor startingFloor;
	
	public void Init (CharactersManager charactersManager) {

        if (Data.Instance.isArcadeMultiplayer) return;

	    Floor newFloor = Instantiate(startingFloor) as Floor;
        newFloor.Init(charactersManager);
	}
}
