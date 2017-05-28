MohyoButton
===========

ワンクリックでもひょれるやつ 
  
## ダウンロード
### Windows  
下のDOWNLOADをクリックするとダウンロードが始まります。  
  
[DOWNLOAD](https://github.com/0V/MohyoButton/releases/download/1.1.2/MohyoButton1.1.2.zip)
  
  
## 使い方
Don't think! Feeeel!  
Yes! You can Mohyo!!  
You can Mohyo!!!!  

## 設定
keyinfo.xml を書き換えることで設定を変更できます。詳しくはパラメータの項をご覧ください。
設定をいじった結果もひょれなくなった場合、keyinfo.xml を削除して設定を初期化してください。

## パラメータ
keyinfo.xml に記述されているパラメータです。  
  
* ConsumerKey  
使用するコンシューマーキーです。よくわからない場合はいじらないでください。  
  
* ConsumerSecret  
使用するコンシューマーシークレットキーです。よくわからない場合はいじらないでください。  
  
* AccessToken  
使用するアカウントのアクセストークンです。よくわからない場合はいじらないでください。  
  
* AccessTokenSecret  
使用するアカウントのアクセストークンシークレットです。よくわからない場合はいじらないでください。  
  
* CountMessage  
ツイートに追記する内容を記述します。「[COUNT]」という文字列が入っていた場合、その部分がこれまでにもひょった回数に書き換わります。  
例. [COUNT]回もひょったもひょ！  
  
* PostCountMessage  
CountMessage で設定した内容を実際に追記するかどうかを真偽地で指定します。  
true:追記する false:追記しない  
  
* UserWordListName  
デフォルトの単語リスト以外の単語リストを用いる場合、単語が記述されたファイルのパスを指定します。詳しくは[ユーザー単語リスト]の項をご覧ください。  



## ユーザー単語リスト
デフォルトの単語リスト以外をツイートする内容として設定する方法の説明です。単語をx行ごとに記述したファイルを作成し keyinfo.xml でそのファイルのパスを設定してください。詳しくは前項パラメータを参照してください。  

記述例として、「sample_words.txt」というファイルがあるので、記述の仕方の参考にしてください。  
デフォルトでは「words.txt」というファイルをユーザー単語リストとして読み込む設定になっているので、「sample_words.txt」を「words.txt」として保存してからアプリケーションを起動すると、使用される単語が「words.txt」のものに置き換わっているのが確認できます。  



## 回数カウント機能
なんかいもひょったかカウントする機能です。keyinfo.xml から回数の投稿は無効化できます。投稿を無効化していても、カウント自体はされています。  
ただし、ユーザー単語リストを使っているときは回数にカウントされません。



### あっぷでーとログ
2016/12/25 v1.1.2
* ユーザー単語リストを使うとカウンターがマイナスされる場合があるバグ修正

2016/01/20 v1.1.1
* 回数カウンターのバグ修正

2016/01/20 v1.1.0
* 設定機能追加
* 回数カウント機能追加
* Closeボタン追加
* ユーザー単語リスト機能追加
* README追加
