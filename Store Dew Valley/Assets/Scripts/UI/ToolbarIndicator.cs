using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarIndicator : MonoBehaviour
{

    private float indicatorPos;
    private float curPos;

    [HideInInspector]
    public int indCurLoc = 0;

    private RectTransform toolbarTransform;

    public void Update()
    {

        indicatorPos = Input.GetAxis("Mouse ScrollWheel");
        if (indicatorPos >= 0.1)
        {
            //move indicator up
            UpdateIndicatorPos(-1);
            curPos = indicatorPos;
        }
        else if (indicatorPos <= -0.1)
        {
            //move indicator down
            UpdateIndicatorPos(1);
            curPos = indicatorPos;
        }

    }
    public void UpdateIndicatorPos(int moveDir)
    {
        if (moveDir == 1)
        {
            indCurLoc += 1;
            if (indCurLoc >= 9)
            {
                indCurLoc = 0;
            }
            toolbarTransform = ToolbarPanel.instance.uIToolbars[indCurLoc].GetComponent<RectTransform>();
        }
        else
        {
            indCurLoc -= 1;
            if (indCurLoc < 0)
            {
                indCurLoc = 8;
            }
            toolbarTransform = ToolbarPanel.instance.uIToolbars[indCurLoc].GetComponent<RectTransform>();
        }
        
        transform.position = toolbarTransform.position;


    }
}
