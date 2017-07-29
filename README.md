# bottest-dotnet

## Event の扱い
- https://github.com/hkawash/bottest-dotnet/tree/master/Bot%20Application1/Bot_GazeTest のBotと
- https://github.com/hkawash/BotFramework-WebChat/blob/inspect/samples/backchannel/index.html のWebChatを
- https://github.com/hkawash/offline-dl-Test の Offline DirectLine でつなぐとテストできます．

## Offline DirectLine Setup
1. npm install （省略可）
1. npm run build （省略可）
1. npm run directline 3000 http://localhost:3979/api/messages

## WebChat Setup
1. npm install （省略可）
1. npm run build （省略可）
1. BotFramework-WebChat/samples/backchannel/index.html?domain=http://localhost:3000/directline&webSocket=false&pi=100
    - pi (pollingInterval): 100ms で指定．
    - オリジナルを使う場合は pi を指定できません
