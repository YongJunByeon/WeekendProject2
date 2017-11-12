using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    public static MenuScript inst;
    [SerializeField]
    private UILabel inputCount;
    private UILabel inputTime;

    private GameObject letterGame;
    private GameObject inMain;
    [SerializeField]
    private GameObject result;
    //그냥 결과
    void Awake()
    {
        if (inst == null) inst = this;
    }
    // Use this for initialization
    void Start () {
        var temp = GameObject.Find("UI Root").transform.Find("Main").Find("Inputs").transform;

        if (inputCount == null || inputTime == null)
        {

            inputCount = temp.Find("QuizCount").Find("Input").Find("Label").GetComponent<UILabel>();
            inputTime = temp.Find("Time").Find("Input").Find("Label").GetComponent<UILabel>();
        }
        if (letterGame == null) letterGame = temp.parent.parent.Find("LetterFind").gameObject;
        if (inMain == null) inMain = temp.parent.gameObject;
        if (result == null) result = temp.parent.parent.Find("Result").gameObject;
    }
    public void LetterGameStart()
    {
        int val1 = 0;
        int val2 = 0;

        if (int.TryParse(inputCount.text, out val1) == false) val1 = 0;
        else val1 = int.Parse(inputCount.text);
        if (int.TryParse(inputTime.text, out val2) == false) val2 = 0;
        
        this.GetComponent<WordScript>().QuizSet(val1, val2 * 60);

        inMain.SetActive(false);
        letterGame.SetActive(true);
    }
    public void BackToMenu()
    {
        letterGame.SetActive(false);
        result.SetActive(false);
        inMain.SetActive(true);
         inputCount.parent.GetComponent<UIInput>().LoadValue();
    }
    //word에서 수행할까 싶었으나, 그러면 호출은 되는데 좀 이상할거같음
    public void Exit()
    {
        Application.Quit();
    }
    public void WinOrLose(bool win)
    {
        //게임은 끄고
        letterGame.SetActive(false);

        result.SetActive(true);
        UILabel resultText = result.transform.Find("Label").GetComponent<UILabel>();
        if(win)//이겼는가? 졌는가?
        {
            resultText.text = "제한 시간 안에 문제를 모두 풀었습니다!";
        }
        else
        {
            resultText.text = "TIME OVER!";
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
