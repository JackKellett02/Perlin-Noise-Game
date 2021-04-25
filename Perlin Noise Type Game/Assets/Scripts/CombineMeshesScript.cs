using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshesScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (Serialize Fields)

	#endregion

	#region Variable Declarations

	#endregion

	#region Private Functions

	#endregion

	#region Public Access Functions (Getters and setters)
	public void CombineMeshes() {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }
	#endregion
}
