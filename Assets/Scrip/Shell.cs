using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	public Rigidbody myRigidbody;
	public float forceMin;
	public float forceMax;
	// thời gian sống và biến mất
	float lifetime = 4;
	float fadetime = 2;

	void Start () {
		// tạo lực bay ra cho vỏ đạn
		float force = Random.Range (forceMin, forceMax);
		myRigidbody.AddForce (transform.right * force);
		// góc quay
		myRigidbody.AddTorque (Random.insideUnitSphere * force);

		StartCoroutine (Fade ());
	}
	// phai dần và biến mất
	IEnumerator Fade() {
		yield return new WaitForSeconds(lifetime);

		float percent = 0;
		float fadeSpeed = 1 / fadetime;
		Material mat = GetComponent<Renderer> ().material;
		Color initialColour = mat.color;

		while (percent < 1) {
			percent += Time.deltaTime * fadeSpeed;
			mat.color = Color.Lerp(initialColour, Color.clear, percent);
			yield return null;
		}

		Destroy (gameObject);
	}
}
