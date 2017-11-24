using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonMenuScript : MonoBehaviour {
    //공통 메뉴 스크립트 단어의 메뉴스크립트도 이걸 따라야 하게될것임
    public GameObject resultScreen;
    public GameObject playScreen;
    public GameObject mainScreen;
	// Use this for initialization


    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        if (resultScreen != null) resultScreen.SetActive(false);
        playScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
    public void ToPlayScreen()
    {
        mainScreen.SetActive(false);
        playScreen.SetActive(true);
    }
    //인자 줄수 있으면 주게될거임

    public void SceneLoad(string name)
    {
        if (name.Split(' ').Length > 1)
            name = name.Split(' ')[name.Split(' ').Length -1];
        Debug.Log(name);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
