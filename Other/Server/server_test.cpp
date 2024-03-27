#include <boost/beast/core.hpp>
#include <boost/beast/websocket.hpp>
#include <boost/asio.hpp>
#include <iostream>
#include <string>
#include <thread>
#include <vector>
#include <mutex>

namespace websocket = boost::beast::websocket;
using tcp = boost::asio::ip::tcp;

std::vector<websocket::stream<tcp::socket>> clients;
std::mutex clients_mutex;

void broadcast_message(const std::string& message) {
    std::lock_guard<std::mutex> lock(clients_mutex);
    for (auto& ws : clients) {
        ws.text(true);
        ws.write(boost::asio::buffer(message));
    }
}

void session(tcp::socket socket) {
    try {
        websocket::stream<tcp::socket> ws{std::move(socket)};
        ws.accept();
        {
            std::lock_guard<std::mutex> lock(clients_mutex);
            clients.push_back(std::move(ws));
        }
        for (;;) {
            boost::beast::flat_buffer buffer;
            ws.read(buffer); // Keep the connection alive by reading messages
        }
    } catch (std::exception& e) {
        std::cerr << "Session error: " << e.what() << std::endl;
    }
}

void run_server(short port) {
    boost::asio::io_context ioc;
    tcp::acceptor acceptor{ioc, {tcp::v4(), port}};
    for (;;) {
        tcp::socket socket{ioc};
        acceptor.accept(socket);
        std::thread{session, std::move(socket)}.detach();
    }
}

int main() {
    const short port = 8080;
    std::thread server_thread{[port] { run_server(port); }};
    for (;;) {
        broadcast_message("广播广播ing");
        std::this_thread::sleep_for(std::chrono::seconds(2));
    }
    return 0;
}
