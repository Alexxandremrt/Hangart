using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class Pen : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;


    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;

    public InputActionProperty ondraw;

    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void Update()
    {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        float ondrawvalue = ondraw.action.ReadValue<float>();
        //bool isLeftHandDrawing = false;
        if (ondrawvalue == 1)
        {
            Draw();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
        }
        else if (Input.GetButton("Jump"))
        {
            SwitchColor();
        }   
        else if (scrollAmount < 0)
        {
            if(penWidth > 0.02)
            {
                penWidth -=(float)0.01;
            }
            
        }
        else if (scrollAmount > 0)
        {
            if(penWidth < 1)
            {
                penWidth += (float)0.01;
            }
            
        }
    }

    private void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            Material newMaterial = new Material(Shader.Find("Standard"));
            newMaterial.color = tipMaterial.color;
            currentDrawing.material = newMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.position);
        }
        else
        {
            var currentPos = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPos, tip.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.position);
            }
        }
    }

    private void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }
}
