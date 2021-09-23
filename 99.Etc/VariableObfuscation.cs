using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 변수 난독화를 구현
// 프레임마다 암호화를 구현하는 형태?

// 근데 딜인젝션이나, 기타 리패키징 방법의 해킹으로부터는 안전할수 없다.
// 따라서 실제로 만들경우에는 이를 앱가드등의 외부 보안 플러그인이랑 병행해서 사용하는식 가자.
// 아니면 서버로부터 통신으로 apk 등의 실제 실행파일이 변조된건지를 체크하는 방법을 고려해야함.

public class VariableObfuscation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
