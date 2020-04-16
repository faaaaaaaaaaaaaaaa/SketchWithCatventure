using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagement : MonoBehaviour

{
    #region Variable
    public GameObject PauseMenu;
    public GameObject CrayonPanal1;
    public GameObject CrayonPanal2;
    public GameObject CrayonPanal3;

    public GameObject CanvasForDraw;

    protected int ClickCrayonCounter = 0;
    protected bool ClickPauseButton = false ;
    public static bool GameIsPause;

    protected bool ClickDrawButton = false;
    protected bool ClickCheckButton = false;

    protected int NumberOfCrayon = 0;

    public GameObject touch;


    #endregion

    #region Button on click

    public void ButtonCrayon1OnClick()
    {
        //ClickCrayonCounter = 0;
        ClickCrayonCounter++;
        NumberOfCrayon = 1;
        CanvasCrayon(NumberOfCrayon);
    }
    public void ButtonCrayon2OnClick()
    {
       // ClickCrayonCounter = 0;
        ClickCrayonCounter++;
        NumberOfCrayon = 2;
        CanvasCrayon(NumberOfCrayon);
    }
    public void ButtonCrayon3OnClick()
    {
        //ClickCrayonCounter = 0;
        ClickCrayonCounter++;
        NumberOfCrayon = 3;
        CanvasCrayon(NumberOfCrayon);
    }

    public void ButtonPauseOnClick()
    {
        ClickPauseButton = true;
        PauseMenuCanvas();
    }

    public void ButtonDrawOnClick()
    {
        ClickDrawButton = true;
        touch.SetActive(false);
        DrawCanvas();
        ClickDrawButton = false;
    }

    public void ButtonCheckOnclick()
    {
        ClickCheckButton = true;
        DrawCanvas();
        touch.SetActive(true);
        ClickCheckButton = false;
    }

    #endregion

    #region Start Update
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region canvas Crayon
    protected void CanvasCrayon(int NumberOfCrayon)
    {
        if(NumberOfCrayon == 1)
        {
            if (ClickCrayonCounter == 1 || ClickCrayonCounter == 2)
            {
                CrayonPanal1.SetActive(true);
                CrayonPanal2.SetActive(false);
                CrayonPanal3.SetActive(false);
                ClickCrayonCounter++;
                CrayonDrawCanvas();
            }
            else if (ClickCrayonCounter == 3)
            {
                CrayonPanal1.SetActive(false);
                touch.SetActive(true);
                ClickCrayonCounter = 0;
            }
        }
        else if(NumberOfCrayon == 2)
        {
            if (ClickCrayonCounter == 1 || ClickCrayonCounter == 2)
            {
                CrayonPanal1.SetActive(false);
                CrayonPanal2.SetActive(true);
                CrayonPanal3.SetActive(false);
                ClickCrayonCounter++;
                CrayonDrawCanvas();
            }
            else if (ClickCrayonCounter == 3)
            {
                CrayonPanal2.SetActive(false);
                touch.SetActive(true);
                ClickCrayonCounter = 0;
            }
        }
        else if (NumberOfCrayon == 3)
        {
            if (ClickCrayonCounter == 1 || ClickCrayonCounter == 2)
            {
                CrayonPanal1.SetActive(false);
                CrayonPanal2.SetActive(false);
                CrayonPanal3.SetActive(true);
                ClickCrayonCounter++;
                CrayonDrawCanvas();
            }
            else if (ClickCrayonCounter == 3)
            {
                CrayonPanal3.SetActive(false);
                touch.SetActive(true);
                ClickCrayonCounter = 0;
            }
        }
    }
    #endregion

    #region Pause menu
    public void PauseMenuCanvas()
    {
        if (ClickPauseButton)
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            ClickPauseButton = false;
        }
    }

    protected void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    protected void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    #endregion

    #region Canvas for draw
    public void DrawCanvas()
    {
        if (ClickDrawButton)
        {
            CanvasForDraw.SetActive(true);
            ClickDrawButton = false;

        }
        if (ClickCheckButton)
        {
                CanvasForDraw.SetActive(false);
                ClickCheckButton = false;
        }
    }

    public void CrayonDrawCanvas()
    {
        CanvasForDraw.SetActive(false);
    }
    #endregion
}