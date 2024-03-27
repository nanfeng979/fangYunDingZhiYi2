#include <boost/beast/core.hpp>
#include <boost/beast/websocket.hpp>
#include <boost/asio/dispatch.hpp>
#include <boost/asio/strand.hpp>
#include <algorithm>
#include <cstdlib>
#include <functional>
#include <iostream>
#include <memory>
#include <string>
#include <thread>
#include <vector>

namespace beast = boost::beast;
namespace http = beast::http;
namespace websocket = beast::websocket;
namespace net = boost::asio;
using tcp = boost::asio::ip::tcp;

// WebSocket回话,处理一个WebSocket连接
class session : public std::enable_shared_from_this<session>
{
    websocket::stream<beast::tcp_stream> ws_;
    beast::flat_buffer buffer_;
    std::string message_ = "广播广播ing";

public:
    explicit session(tcp::socket&& socket)
        : ws_(std::move(socket))
    {
    }

    // 启动会话
    void run()
    {
        // 接受WebSocket握手
        ws_.async_accept(
            beast::bind_front_handler(
                &session::on_accept,
                shared_from_this()));
    }

    void on_accept(beast::error_code ec)
    {
        if (ec)
            return fail(ec, "accept");

        // 读取WebSocket消息
        do_read();
    }

    void do_read()
    {
        // 读取WebSocket消息
        ws_.async_read(
            buffer_,
            beast::bind_front_handler(
                &session::on_read,
                shared_from_this()));
    }

    void on_read(beast::error_code ec, std::size_t bytes_transferred)
    {
        boost::ignore_unused(bytes_transferred);

        if (ec)
            return fail(ec, "read");

        // 广播消息
        broadcast();

        // 持续读取
        do_read();
    }

    void broadcast()
    {
        // 异步发送消息
        ws_.async_write(
            net::buffer(message_),
            beast::bind_front_handler(
                &session::on_write,
                shared_from_this()));
    }

    void on_write(beast::error_code ec, std::size_t bytes_transferred)
    {
        boost::ignore_unused(bytes_transferred);

        if (ec)
            return fail(ec, "write");
    }

private:
    void fail(beast::error_code ec, char const* what)
    {
        std::cerr << what << ": " << ec.message() << "\n";
    }
};

// WebSocket服务器
class server
{
    net::io_context ioc_;
    tcp::acceptor acceptor_;
    std::thread broadcaster_;

public:
    server(tcp::endpoint endpoint)
        : acceptor_(ioc_, endpoint)
    {
        // 启动广播线程
        broadcaster_ = std::thread([this]() {
            broadcast();
            });
    }

    // 启动服务器
    void run()
    {
        do_accept();
        ioc_.run();
    }

private:
    void do_accept()
    {
        // 接受新连接
        acceptor_.async_accept(
            net::make_strand(ioc_),
            beast::bind_front_handler(
                &server::on_accept,
                this));
    }

    void on_accept(beast::error_code ec, tcp::socket socket)
    {
        if (ec)
        {
            fail(ec, "accept");
        }
        else
        {
            // 创建新的WebSocket会话
            std::make_shared<session>(std::move(socket))->run();
        }

        // 接受下一个连接
        do_accept();
    }

    void broadcast()
    {
        for (;;)
        {
            std::this_thread::sleep_for(std::chrono::seconds(2));
            ioc_.dispatch([&]() {
                static_cast<session*>(nullptr)->broadcast();
                });
        }
    }

    void fail(beast::error_code ec, char const* what)
    {
        std::cerr << what << ": " << ec.message() << "\n";
    }
};

int main(int argc, char* argv[])
{
    try
    {
        auto const address = net::ip::make_address("0.0.0.0");
        unsigned short port = static_cast<unsigned short>(std::atoi("8080"));

        server srv{ tcp::endpoint{address, port} };
        srv.run();
    }
    catch (std::exception const& e)
    {
        std::cerr << "Error: " << e.what() << std::endl;
        return EXIT_FAILURE;
    }
}