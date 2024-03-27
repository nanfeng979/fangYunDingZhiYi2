import asyncio
import websockets

# 用来保存所有连接的客户端
connected = set()

async def handler(websocket, path):
    # 将新连接的客户端加入到集合中
    connected.add(websocket)
    try:
        while True:
            # 接收客户端发送的消息
            message = await websocket.recv()
            print(f"Received message: {message}")
            
            # 广播消息给所有其他客户端
            for conn in connected:
                # if conn != websocket:
                    await conn.send(message)
    finally:
        # 当客户端断开连接时，将其从集合中移除
        connected.remove(websocket)

async def main():
    start_server = await websockets.serve(handler, "localhost", 8080)
    await start_server.wait_closed()

asyncio.get_event_loop().run_until_complete(main())
