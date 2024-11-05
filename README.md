# Program-Exercises

演習という形でやってみたかったプログラムの組み方や自身コーディング力向上を図るためのプロジェクト

**<制作で活かすとともにきれいなコーディングや設計を目指す>**

**01 エディタ拡張**

1,元のオブジェクトのマテリアルを他のオブジェクトのマテリアルに反映

[EditorWindowTest.cs](ProgramExercises/Assets/Resource/Script/Editor/EditorWindowTest.cs)

https://github.com/user-attachments/assets/5d1f5a79-f7d1-4c28-ab7e-edb5515bd432

※UIBuilderでインスペクターを作成


2,Inspectorで配列の要素の名前を表示

[CustomElementName.cs](ProgramExercises/Assets/Resource/Script/Editor/CustomElementName.cs)

![ProgramExercises - TMP_Animation - Windows, Mac, Linux - Unity 2023 2 0f1 _DX11_ 2024_10_13 21_25_35](https://github.com/user-attachments/assets/2db9894a-054a-48d4-82ed-504c8ab6735b)


3,AudioClipの別々の設定ができないなと感じ、サウンドのエディター拡張

[MySoundManagerEditor.cs](ProgramExercises/Assets/Resource/Script/Editor/MySoundManagerEditor.cs)

[MySoundManager](ProgramExercises/Assets/Resource/Script/MySoundManager)

[動画](https://youtu.be/ZsRM6C0OSkQ)

![ProgramExercises - SoundTest - Windows, Mac, Linux - Unity 2023 2 0f1 _DX11_ 2024_10_24 6_45_29](https://github.com/user-attachments/assets/a833d0a3-ad6b-4e2c-8c4c-ce5270eaeddf)


**02 デザインパターンの演習**

1,Singleton

[SingletonTest.cs](ProgramExercises/Assets/Resource/Script/DesignPatterns/SingletonTest.cs)

![ProgramExercises - SampleScene - Windows, Mac, Linux - Unity 2023 2 0f1_ _DX11_ 2024_09_20 5_33_34](https://github.com/user-attachments/assets/75aa4d0d-261a-46b3-88ae-ba280a8c63e0)


2,Factory

[FactoryTest.cs](ProgramExercises/Assets/Resource/Script/DesignPatterns/FactoryTest.cs)

![ProgramExercises - SampleScene - Windows, Mac, Linux - Unity 2023 2 0f1_ _DX11_ 2024_09_20 5_33_56](https://github.com/user-attachments/assets/21aa45ae-f9c3-4d85-a4b7-4e70ca12db9d)


3,Observer

[ObserverTest.cs](ProgramExercises/Assets/Resource/Script/DesignPatterns/ObserverTest.cs)

https://github.com/user-attachments/assets/279d6ccd-c12f-441e-8b12-d932ef865e6c

4,Decorator

[DecoratorTest.cs](ProgramExercises/Assets/Resource/Script/DesignPatterns/DecoratorTest.cs)

![ProgramExercises - SampleScene - Windows, Mac, Linux - Unity 2023 2 0f1_ _DX11_ 2024_09_20 5_35_28](https://github.com/user-attachments/assets/8fb84731-8f2d-48a6-8f7f-f69cc933e1b4)

5,Strategy

[StrategyTest.cs](ProgramExercises/Assets/Resource/Script/DesignPatterns/StrategyTest.cs)

https://github.com/user-attachments/assets/8dd466ae-be4b-411b-a8af-c16cf35ed124

6,State

[StateTest.cs](ProgramExercises/Assets/Resource/Script/DesignPatterns/StateTest.cs)

https://github.com/user-attachments/assets/aaf80791-4767-4e92-a643-5f41f9869af9

7,Command

[CommandTest](ProgramExercises/Assets/Resource/Script/DesignPatterns/CommandTest)

https://github.com/user-attachments/assets/6cda7f03-7bcd-454f-96a8-e7caaaa018b1


**03 ObjectPoolの使用**

https://github.com/user-attachments/assets/472a6f9d-5e84-4450-886b-1e5e5d6c15da

[参考URL](https://huchat-gamedev.net/explanation-object-pool/)

**04 モールス信号**

csvからパターンのデータを読み込んで文字を表示

[InputMorse.cs](ProgramExercises/Assets/Resource/Script/Notes/InputMorse.cs)

https://github.com/user-attachments/assets/4cb2666e-f0e4-40f0-ba30-d863bca4fe69

~~※各処理を別々のスクリプトで分ける予定~~
2024/09/25 処理分け済み

作ったモールスのパターンのデータを読み込み、出されたパターン通りに入力すると成功できなければ失敗

[JudgeNotes.cs](ProgramExercises/Assets/Resource/Script/Notes/JudgeNotes.cs)

https://github.com/user-attachments/assets/60bde784-7b37-4c5a-bf49-dd4914a565f4

**05 画像の読み込み->キャラクターの移動の流れ(addressableの使用)**

[LoadImage.cs](ProgramExercises/Assets/Resource/Script/CharaMove/LoadImage.cs)

https://github.com/user-attachments/assets/f94c47eb-ca1d-40df-bb97-606d1107385f

**06 TextMeshProのアニメーション**

1,テキストのフェード

[FadeText.cs](ProgramExercises/Assets/Resource/Script/TMPAnimation/FadeText.cs)

https://github.com/user-attachments/assets/65e97c81-4893-4647-8104-074b1d1a914d


2,テキストの拡大縮小

[ChangeScaleText.cs](ProgramExercises/Assets/Resource/Script/TMPAnimation/ChangeScaleText.cs)

https://github.com/user-attachments/assets/07f85d9b-305b-405b-be99-81f35d165056


3,テキストのウェーブ

[WaveText.cs](ProgramExercises/Assets/Resource/Script/TMPAnimation/WaveText.cs)

https://github.com/user-attachments/assets/eb056838-8f53-44be-81eb-ebac991d5e35


4,テキストの上下移動

[UpDownText.cs](ProgramExercises/Assets/Resource/Script/TMPAnimation/UpDownText.cs)

https://github.com/user-attachments/assets/3eb247ea-8ab0-4b14-bf73-a786a8692f73


5,文字の回転

[RatateCharaText.cs](ProgramExercises/Assets/Resource/Script/TMPAnimation/RatateCharaText.cs)

https://github.com/user-attachments/assets/1e6df58f-26c2-4173-9340-d32d9b453506

6,Dotweenを用いてのスコアのカウントアップ

[AnimationScoreText.cs](ProgramExercises/Assets/Resource/Script/TMPAnimation/ScoreAnimation/AnimationScoreText.cs)

[DisplayScoreHitory.cs](ProgramExercises/Assets/Resource/Script/TMPAnimation/ScoreAnimation/DisplayScoreHitory.cs)

https://github.com/user-attachments/assets/77634e6d-cbde-4c65-b9c3-6f4df0ecf036

思いついたら追加予定
