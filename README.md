<h2 align="center"> 🎉 Unity Library 🎉 </h2><br>

해당 리포지토리는 유니티로 개발하면서 필요했던 기능을 제가 직접 모아놓거나 제작해서 만든 라이브러리입니다. <br>
This repository is a library where Jack Hwang collects or creates features implemented for Unity development.

<h2 align="center"> 🍳 Getting Started 🍳 </h2><br>

이 리포지토리에는 Unity에서 제공하지 않는 기능과 스크립트 및 셰이더와 같은 Unity용 유틸리티가 포함되어 있습니다.<br>
This repository contains features not provided by Unity and utilities for Unity such as scripts and shaders.
<br><br>
아래의 Git 명령을 사용하여 Unity 프로젝트에서 직접 사용할 수 있습니다. <br>
You can use it in your Unity project using the Git command below.

<br>

```
git clone https://github.com/DevHwangIT/MyLibrary.git | git submodule add https://github.com/DevHwangIT/MyLibrary.git
```
<br>

내부에서 사용되는 코드는 아래와 같은 경로에 따른 네임스페이스로 구분되어 있어 기존 코드와의 충돌을 방지하였습니다.<br>
Codes used inside are divided into namespaces according to the following paths to prevent conflicts with existing codes.

<br>

```C#
using namnspace MyLibrary/...
```
<br>

<h2 align="center"> ⚠ Warning ⚠ </h2><br>
Assets / MyLibrary / GameTemplate 의 경우 Unity 개발에 자주사용되는 Manager 클래스를 Partial 및 싱글톤을 바탕으로 구현해놓은 폴더입니다.<br> ( 만일, 해당 기능이 기존 코드와 충돌하거나 필요없다면 해당 경로만 삭제 후 사용하시기 바랍니다. )<br><br>

In the case of Assets / MyLibrary / GameTemplate, it is a folder that implements the Manager class, which is often used for Unity development, based on a singleton.<br> ( If the function conflicts with the existing code or is unnecessary, please use it after deleting the corresponding path.)

<br>

<h2 align="center"> 🛠 Update History 🛠 </h2>
<details>
<summary><h3> 업데이트 내역 ( History Detail )</h3></summary>
<div markdown="1">
  
2022.06.20 Write ReadMe documents
 
</div>
</details>

<h2 align="center"> 📚 Content 📚</h2>

<details>
<summary><h3> 구성 내용 ( Contents Detail ) </h3></summary>
<div markdown="1">
<h4> [Path] Assets / MyLibrary / 1.DesignPattern </h4>
- 유니티에서 사용할 수 있는 디자인 패턴 관련 기능을 모아둔 디렉토리입니다.<br>
  ( A directory contains features related to design patterns available in Unity. )

<h4> [Path] Assets / MyLibrary / 2.Mathematic </h4>  
- 유니티에서 사용할 수 있는 수학 연산과 관련된 기능이 포함된 디렉토리입니다.<br>
  ( A directory containing functions related to mathematical operations available in Unity. )
  
<h4> [Path] Assets / MyLibrary / 3.Tools </h4>
- 유니티에서 사용할 수 있는 게임 개발을 위한 편리한 기능과 도구가 있는 디렉토리입니다.
  ( A directory with convenient features and tools for game development available in Unity. )

<h4> [Path] Assets / MyLibrary / 4.Utility </h4>
- 유니티에서 자주 사용되지만, 구현되지 않은 게임 개발을 위한 함수들이 구현되어 있는 디렉토리입니다.
  ( A directory where features for game development that are frequently used but not implemented in Unity are implemented. )

<h4> [Path] Assets / MyLibrary / 5.Mobile </h4>
- 유니티에서 모바일 빌드시에 사용할 수 있는 기능들이 모여있는 디렉토리입니다.
  ( A directory is a collection of features that can be used when building mobile in Unity. )

<h4> [Path] Assets / MyLibrary / 6.Network </h4>
- 유니티에서 사용할 수 있는 네트워크 및 통신을 위한 기능이 구현되어 있는 디렉토리입니다.
  ( A directory that implements functions for networking and communication that can be used in Unity. )

<h4> [Path] Assets / MyLibrary / 7.Shader </h4>
- 유니티에서 사용할 수 있는 쉐이더 기능을 위한 디렉토리입니다.
  ( A Directory for shader features available in Unity. )

<h4> [Path] Assets / MyLibrary / 8.Attribute </h4>
- 유니티 에디터에서 사용할 수 있는 애트리뷰트 기능을 구현해 놓은 디렉토리입니다.
  (A directory that implements attribute functions that can be used in the Unity editor. )

<h4> [Path] Assets / MyLibrary / 99.Etc </h4>
- 유니티에서 사용할 수 있지만 분류가 명확하지 않아 이를 구분하기 위한 디렉토리입니다.
  ( It is available in Unity, but the classification is not clear, so it is a directory to distinguish them. )

<h4> [Path] Assets / MyLibrary / GameTemplate </h4>
- Unity에서 자주 사용되는 Manager 관련 클래스 및 기능이 구현되어 있는 디렉토리입니다. <br>
  
[GameTemplate에 관한 중요한 내용은 경고를 참조하세요.](https://github.com/DevHwangIT/MyLibrary/edit/main/README.md#--warning--) <br> 
  
  ( This is the directory where Manager-related classes and functions frequently used in Unity are implemented. )   
[See the warnings for important information about GameTemplates.](https://github.com/DevHwangIT/MyLibrary/edit/main/README.md#--warning--)
  
</div>
</details>

<h2 align="center"> 📧 Contact 📧 </h2>

만일, 해당 라이브러리 사용에 있어서 문제가 있거나 개선할 부분이 있다면 아래를 통해서 연락해주세요. 🙏🙏<br>
(If you have problems using the library, Please contact us through below. 🙏🙏)

📬 Email : zxc1063@naver.com or dlsxo1063@cau.ac.kr

