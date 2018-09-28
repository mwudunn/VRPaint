using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Vector3 start;
    private Vector3 end;
    private int colorIndex;
    private List<GameObject> lines;
    Color[] colorArr = { Color.blue, Color.cyan, Color.green, Color.red, Color.magenta, Color.white };
    private float brush_size = 0.01f;

    enum buttonKey {
        none = 0,
        one = 1,
        two = 2,
        three = 3
    } 

    void Start() {
        start = gameObject.transform.position;
        colorIndex = 0;
        lines = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        if (start == null) {
            return;
        }
        end = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        buttonKey pressed = buttonKey.none;
		pressed = OVRInput.Get(OVRInput.Button.One) ? buttonKey.one : pressed;
        pressed = OVRInput.Get(OVRInput.Button.Two) ? buttonKey.two : pressed;
        pressed = OVRInput.Get(OVRInput.Button.Three) ? buttonKey.three : pressed;

        var adjust_radius = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);


        if (pressed == buttonKey.one)
        {
            Debug.Log("Starting to paint.");
            Paint();
        }
        else if (pressed == buttonKey.two)
        {
            Clear();
        }
        else if (pressed == buttonKey.three) {
            colorIndex = (colorIndex + 1) % colorArr.GetLength(0);
            
        }

        if (adjust_radius[1] > 0.1)
        {
            brush_size += 0.001f;
        }
        else if (adjust_radius[1] < -0.1) {
            brush_size = Mathf.Min(0.001f, brush_size - 0.001f);
        }

        start = end;

    }

    void Paint()
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(colorArr[colorIndex], colorArr[colorIndex]);
        lr.SetWidth(brush_size, brush_size);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lines.Add(myLine);

    }

    void Clear() {
        GameObject[] linesArray = lines.ToArray();
        lines = new List<GameObject>();
        for (int i = 0; i < linesArray.GetLength(0); i++) {
            GameObject go = linesArray[i];
            linesArray[i] = null;
            Destroy(go);
        }
    }
}
