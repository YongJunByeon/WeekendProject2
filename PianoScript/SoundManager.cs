using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    //TODO : 버튼을 누르면 해당 사운드가 재생되도록 하기
    //1. 키보드 입력을 하면 List? 딕셔너리? 내에서 해당 값을 찾고, 그값의 플레이를 실행시키는거 <<딕셔너리는 O(1)이라고 하지만 id값같은 쓸모 없는걸 넣게될것이고 리스트는 (n)이라고 했던거같음
    //ㄴ 그럼 List배열로하고 활성중이 리스트인 경우에만 하도록 하는건 어떤지
    //2.Key자체에서 키를 입력받으면 하는식 << 이래도 cur octave(아마 현재 옥타브, 같은음 다른 옥타브가 재생되면 안되니깐)같은 문제가 있을듯

        //옥타브 이동은 가장 유력한건 shift,ctrl q,w를 이용 전자는 치는중에 필요한 옥타브 이동이고 q,w는 영구 옥타브이동임
        //아마 리스트+ 배열로해서 n을 12까지로 줄이게될거같음
        
        //걍 피아노 키보드를 3개만 보여주면 되지 않았을까 싶기도함 
    string soundPath;
    Transform keySetter;
    [SerializeField]
    //어떻게 하면 #따로할수 있을까?
    //간단하게 asd hjkl 은 오리지날
    //샵부분(7이후)는 we uio 어떨까
    List<KEYS>[] soundKeys ;
    int octaveCur = 0;
    public KeyCode nextKey = KeyCode.LeftShift;//다음옥타브 5면 0을로
    public KeyCode prevKey = KeyCode.LeftControl;//이전 옥타브 0이면 4로
    public KeyCode plusKey = KeyCode.Z;//임시 +1옥타브 (5면 0)
    public KeyCode minusKey = KeyCode.C;//임시 -1옥타브(0 -> 4)

    public KeyCode[] octaveKeys = {
        KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.H, KeyCode.J, KeyCode.K,
        KeyCode.L,
        KeyCode.W, KeyCode.E, KeyCode.U,
        KeyCode.I, KeyCode.O
    };
    private Transform playScreen;
    private Transform mainScreen;
	// Use this for initialization
	void Start () {

        playScreen = GameObject.Find("UI Root").transform.Find("InPlay");
        mainScreen = playScreen.parent.Find("Main");//루트 찾는것보단 이게 나을것
        if (soundPath == null)
        {
            soundPath = "PianoOctaves\\";
        }
        keySetter = playScreen.Find("KeyZone");
        //우선 옥타브를 어떻게 전부할지 하기전에 키입력-> 재생을 먼저함

        if (keySetter == null) return;

        soundKeys = new List<KEYS>[keySetter.childCount];
        octaveCur = keySetter.childCount / 2;
        for(int i = 0; i < keySetter.childCount; i++)
        {
            soundKeys[i] = new List<KEYS>();
            //옥타브 오브젝트 여기의 오리지널, 샾을 뒤져볼것 간단하게 자식2개
            Transform octaveObject = keySetter.GetChild(i);
            //둘다 아마 오리지널,샾공간임을 뜻함
            Transform originalMaybe = octaveObject.GetChild(0);
            Transform sharpMaybe = octaveObject.GetChild(1);
            //Octave의 값을 저장
            int octaveNum = int.Parse(keySetter.GetChild(i).name.Split(' ')[1] );
            //구성 이 옥타브넘버값 + 공간내의 자식의 이름을 조합해서 음악을 조합하고 soundKeys에 넣을거임
            //오리지날이면 n번째 자식 +  Num + wmv/ sharp면 Num+ #+ wmv

            for (int j = 0; j < originalMaybe.childCount; j++)
            {
                soundKeys[i].Add(originalMaybe.GetChild(j).GetComponent<KEYS>());
                var temp = soundPath + originalMaybe.GetChild(j).name + octaveNum;
                AudioClip clip = Resources.Load(temp) as AudioClip;
                if (clip == null) continue;
                originalMaybe.GetChild(j).GetComponent<KEYS>().AddSound(clip);
            }

            for(int j = 0; j < sharpMaybe.childCount; j++)
            {
                soundKeys[i].Add(sharpMaybe.GetChild(j).GetComponent<KEYS>());
                var temp = soundPath + sharpMaybe.GetChild(j).name + octaveNum+"#";
                AudioClip clip = Resources.Load(temp) as AudioClip;
                if (clip == null)
                {
                    soundKeys[i].RemoveAt(soundKeys[i].Count -1);//w,o가 말썽된다이러면 
                    continue;
                } 
                sharpMaybe.GetChild(j).GetComponent<KEYS>().AddSound(clip);

            }
        }
	}
	void PlayKey(int moveVal)
    {
        for (int i = 0; i < octaveKeys.Length; i++)
        {
            if (Input.GetKey(plusKey))
            {
                if (octaveCur == 0) moveVal = soundKeys.Length - 1;
                else moveVal = -1;
            }
            else if (Input.GetKey(minusKey))
            {
                if (octaveCur >= soundKeys.Length - 1)
                {
                    moveVal = -(soundKeys.Length - 1);
                }
                else moveVal = 1;
            }

            if (Input.GetKeyDown(octaveKeys[i]))
            {
                soundKeys[octaveCur + moveVal][i].PlayOctave();
            }
            else if (Input.GetKey(octaveKeys[i]))
            {
                soundKeys[octaveCur + moveVal][i].Pressed(true);
            }
            else soundKeys[octaveCur + moveVal][i].Pressed(false);
        }

    }
	// Update is called once per frame
	void Update () {
        int moveVal = 0;
        if (Input.GetKeyDown(nextKey))
        {//맨위 그러니깐 5부터 먹었기에 이렇게 내려야함
            octaveCur--;
            if (octaveCur < 0)
                octaveCur = soundKeys.Length - 1;

        }
        else if(Input.GetKeyDown(prevKey))
        {
            octaveCur++;
            if (octaveCur >= soundKeys.Length)
                octaveCur = 0;

        }
        PlayKey(moveVal);
	}

    public void Playable()
    {
        playScreen.gameObject.SetActive(true);

        mainScreen.gameObject.SetActive(false);
    }
}
