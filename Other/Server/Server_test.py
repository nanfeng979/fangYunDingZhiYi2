import asyncio
import websockets

async def handler(websocket, path):
    while True:
        await asyncio.sleep(2)  # 等待两秒
        await websocket.send("hahahaha")

async def main():
    start_server = await websockets.serve(handler, "localhost", 8080)
    await start_server.wait_closed()

asyncio.get_event_loop().run_until_complete(main())