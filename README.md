# bottest-dotnet

## Bot_GazeTest
C#の Bot で Event が扱えることを確認できます．
- https://github.com/mlab-hci/bottest-dotnet/tree/master/Bot_GazeTest のBotと
- https://github.com/mlab-hci/BotFramework-WebChat-Test/blob/inspect/samples/backchannel/index.html のWebChatを
- https://github.com/mlab-hci/offline-dl-Test の Offline DirectLine でつないでください．

### テスト内容
WebChat の backchannel サンプル (Node.js) を C# .NET にしたもので，使い方は同じです．中では以下の処理が行われています．

1. 色の名前をBotに送ると背景の色が変わる（Bot から WebChat へ event を送るテスト）
    1. WebChat から色の名前(#ccccff, green, etc.)を message で Bot に送る．
    1. Bot は受け取った色の名前をそのまま event の Value に入れてWebChatに送る．（ただし現時点では使いまわしのため． Node.js のサンプルと違い，Gaze 情報その他を message で送るサンプルもくっついています．要整理．）
    1. WebChat はそれに従ってブラウザ内の画面の色を変える．
1. ボタンを押すとBotがメッセージを表示する（WebChat から Bot へ event を送るテスト）
    1. WebChat から event を Bot に送る．
    1. Bot は event を受け取ったら WebChat に message を送る．

### Offline DirectLine Setup
1. npm install （省略可）
1. npm run build （省略可）
1. npm run directline 3000 http://localhost:3979/api/messages

### WebChat Setup
1. npm install （省略可）
1. npm run build （省略可）
    - 各サンプルの index.html の中でこのプロジェクトのルートにある botchat.js, botchat.css（WebChat の本体）を呼びますが，相対パスで指定されています．js, css が見当たらない場合は build が必要です．
1. BotFramework-WebChat/samples/backchannel/index.html?domain=http://localhost:3000/directline&webSocket=false&pi=100
    - pi (pollingInterval): 100ms で指定．
    - オリジナルを使う場合は pi を指定できません

## WPF_WebChat
WPF のアプリケーションに WebChat を表示します．（WebBrowser コントロールで WebChat を開いています．）
1. MainWindow.xaml.cs の中で，WebChat のサンプルのあるパスを指定してください．

Note:
- デフォルトのWebBrowserコントロールはなんと IE7 を呼んでいるので，PCに入っている新しいIEに置き換えています．
    - 具体的には，Appのコンストラクタとデストラクタでレジストリの登録と削除をしています．
    - 参考: http://qiita.com/hbsnow/items/e732621b223e2a5d5df8
- WebChat側 (DirectLineJS) は ajax を使っていますが，IE11 のキャッシュが影響して Bot 側の出力が描画されません．
    - GET polling に対するレスポンスでは常にヘッダで No-Cache などをつける，サーバ側 (Offline DirectLine) を変更しました．
    
## （参考）デバッグ用ツール
- [Fiddler](https://www.telerik.com/download/fiddler) が通信の流れを見れて超便利!


