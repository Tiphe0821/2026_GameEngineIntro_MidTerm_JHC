using UnityEngine;
using System.IO;
using System.Collections.Generic;


[System.Serializable]
public class StageResult            // 저장될 스테이지 데이터의 클래스 (객체로 저장된다)
{
    public string playerName;
    public int stage;
    public int score;
}

[System.Serializable] // Json으로 저장할 클래스에만 들어가는 거

public class StageResultList        // 위에 작성한 클래스를 리스트로 받아 여러 객체를 저장할 수 있는 클래스
{
    public List<StageResult> results = new List<StageResult>();
}

public static class StageResultSaver
{
    private const string FILE                   = "stage_results.json";     // Appdata에 저장될 파일의 이름
    private const string PLAYER_NAME            = "PlayerName";         // PlayerPrefs 키
    private static string filePath = Path.Combine(Application.persistentDataPath, FILE);
    public static void SaveStage(int stage, int score)      // 데이터 저장 함수 (스테이지 넘버와 점수를 가져온다) // 아마 스테이지는 빌드 인덱스 순서되로 될것 같다.
    {
        StageResultList list = LoadInternal();                          // 로드 인터널 함수를 호출해 기존에 저장된 점수의 리스트를 불러온다
        string playerName = PlayerPrefs.GetString(PLAYER_NAME, "");     // PlayerPrefs로 저장된 플레이어의 이름을 불러온다
        StageResult entry = new StageResult                             // 새로운 스테이지 결과를 새로운 객체로 선언
        {
            playerName = playerName,
            stage = stage, 
            score = score
        };
        list.results.Add(entry);                            // 리스트에 방금 만든 객체 추가
        string json = JsonUtility.ToJson(list, true);       // 리스트에 있는 모든 값을 ToJson 함수를 사용해 테스트로 변환
                                                            // 뒤에 붙는 bool 값은 prettyPrint 를 결정한다. 참값이라면 저장되는 값의 줄을 자동으로 나누고 띄어쓰기가 들어가 파일이 이쁘게 정리된다 (데이터상 단순히 불러오는 것에는 큰 차이가 없다)
        File.WriteAllText(filePath, json);                  // ToJson을 통해 텍스트로 변환된 파일을 그대로 폴더에 저장한다
    }

    public static StageResultList LoadRank()                // StageResult 리스트를 반환하는 함수
    {
        return LoadInternal();
    }

    private static StageResultList LoadInternal()           // StageResult 리스트를 반환해주는 함수
    {
        if (!File.Exists(filePath))  // 파일 자체가 존재하지 않는다면
        {
            return new StageResultList();  // 새 리스트 반환 - (이때 생성되는 리스트는 빈 리스트다)
        }
        string json = File.ReadAllText(filePath);   // json 스트링값에 파일 내 모든 텍스트를 저장한다
        StageResultList list = JsonUtility.FromJson<StageResultList>(json);  // json이라는 이름에 저장된 스트링을 FromJson 함수를 통해 클래스로 다시 변환시킨다
        if (list == null)   // 파일 로드 했을 때 파일이 없다면 
            return new StageResultList();   // 새로운 리스트를 생성해서 넘기기 - (이때 생성되는 리스트는 빈 리스트다)
        else                // 파일 로드했을 때 파일이 있다면
            return list;    // 기존 파일 로드
    }
}
