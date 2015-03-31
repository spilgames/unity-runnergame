using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

	public Camera c;
	public bool active = true;
	public Sprite sprite_layer1;
	public Material spriteMaterial;
	public float scale_layer1 = 4f;
	public float speed_layer1 = 0.2f;
	public float movespeed_layer1 = 0f;
	public int orderInLayer = 0;
	public float yoffset = 0;
	public float yPivot = 0.5f;
	public float startAlpha = 1f;


	private float layer1_width;
	private float current_speed_pos = 0;

	Transform layer1;
	SpriteRenderer layer2;


	System.Collections.Generic.LinkedList<GameObject> layer1s = new System.Collections.Generic.LinkedList<GameObject> ();
	public float pixelToUnits = 32f;

	void Start () {
		c = Camera.main;
		layer1 = (Transform)transform.GetChild (0).gameObject.GetComponent<Transform>();

		//add first sprite
		GameObject s = AddSprite (sprite_layer1.texture);
		s.transform.parent = layer1;
		s.transform.position = new Vector3 (0, 2, 0);
		s.transform.localScale = new Vector3 (scale_layer1, scale_layer1, 1);
		SpriteRenderer sprRenderer = s.GetComponent<SpriteRenderer>();

		//determine width of sprite
		layer1_width = sprRenderer.bounds.size.x;

		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0.5f,0));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0.5f,0));

		int numberOfItems = (int)((rightEdge - leftEdge).x / layer1_width);

		numberOfItems += (int)(numberOfItems / 2);

		numberOfItems = 3;

		//add items
		for (int i=-((int)numberOfItems/2); i<=((int)numberOfItems/2); i++) {

			if (i==0){
				layer1s.AddLast (s);
				continue;
			}

			GameObject s2 = AddSprite (sprite_layer1.texture);
			s2.transform.parent = layer1;
			s2.transform.position = new Vector3 (RoundToNearestPixel(layer1_width*i), 0, 0);
			s2.transform.localScale = new Vector3 (scale_layer1, scale_layer1, 1);
			layer1s.AddLast (s2);
		}

		setAlpha (startAlpha);
	}

	public GameObject AddSprite (Texture2D tex) {
		Texture2D _texture = tex;
		Sprite newSprite = Sprite.Create(_texture, new Rect(0f, 0f, _texture.width, _texture.height), new Vector2(0.5f, yPivot),pixelToUnits);
		GameObject sprGameObj = new GameObject();
		sprGameObj.name = "s";
		sprGameObj.AddComponent<SpriteRenderer>();
		SpriteRenderer sprRenderer = sprGameObj.GetComponent<SpriteRenderer>();

		if (spriteMaterial != null) {
			sprRenderer.material = spriteMaterial;
			//sprRenderer.material.shader 
		}

		sprRenderer.sprite = newSprite;
		sprRenderer.sortingOrder = orderInLayer;

		return sprGameObj;
	}
	
	public float RoundToNearestPixel(float unityUnits)
	{
		float valueInPixels = unityUnits * pixelToUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
		return roundedUnityUnits;
	}

	public void moveBefore(GameObject a,GameObject b){
		Vector3 v = b.transform.position;
		
		v.x = RoundToNearestPixel(v.x-layer1_width);
		a.transform.position = v;

		layer1s.RemoveLast ();
		layer1s.AddFirst (a);

	}

	public void moveAfter(GameObject a,GameObject b){
		Vector3 v = b.transform.position;

		v.x = RoundToNearestPixel(layer1_width+v.x);
		a.transform.position = v;
	
		layer1s.RemoveFirst ();
		layer1s.AddLast (a);
	}

	public void setVisible(bool isHidden)
	{
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers)
		{
			r.enabled = isHidden;
		} 

		active = isHidden;
	}

	public void setAlpha(float f){
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers)
		{
			//r.material.color = new Color(1.0f,1.0f,1.0f,f);

			if (spriteMaterial != null) {
				r.material = spriteMaterial;
				//sprRenderer.material.shader 
			}
		}
	}
	
	void FixedUpdate () {

			if (!active) {
				return;
			} else {

			}

		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0.5f,0));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0.5f,0));
		float leftItemDiff = (layer1s.First.Value.transform.position - leftEdge).x + (layer1_width/2);
		float rightItemDiff = (rightEdge - layer1s.Last.Value.transform.position).x + (layer1_width/2);

		if (leftItemDiff < 0) {
			moveAfter (layer1s.First.Value, layer1s.Last.Value);
		} else if (rightItemDiff < 0) {
			moveBefore (layer1s.Last.Value,layer1s.First.Value);
		}

		Vector3 cameraDiff = c.transform.position - new Vector3(0,0,0);

		float newBackgroundPosx = cameraDiff.x * speed_layer1*1;
		float newBackgroundPosy = cameraDiff.y * speed_layer1*1;

		if (movespeed_layer1 != 0){
			current_speed_pos += movespeed_layer1;
			newBackgroundPosx +=current_speed_pos;
		}

		Vector3 newPos = layer1.transform.position;
		newPos.x = newBackgroundPosx;
		newPos.y = newBackgroundPosy + yoffset;
		layer1.transform.position = newPos;
	}
}
