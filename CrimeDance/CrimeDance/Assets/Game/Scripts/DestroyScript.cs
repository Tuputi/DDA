using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    void UnactivateThis()
    {
        this.gameObject.SetActive(false);
    }
}
