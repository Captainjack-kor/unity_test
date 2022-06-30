const WebSocket = require('ws');
const wss = new WebSocket.Server({ port: 8080 }, () => {
  console.log('server started');
})
let increase_I = 0;

wss.on('connection', (ws) => {
  ws.on('message', (data) => {
    ws.send(data) 
    console.log('data received %o', data + `${increase_I++}`);
  })
})

wss.on('listening', () => {
  console.log('server is listening on port 8080');
})