using ChatServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSocketsController : ControllerBase
    {
        private readonly ILogger<WebSocketsController> _logger;
        private readonly WebSocketServer _server;

        public WebSocketsController(ILogger<WebSocketsController> logger, WebSocketServer server)
        {
            _logger = logger;
            _server = server;
        }

        [HttpGet("/ws")]
        public async Task Get()
        {
            await _server.ProcessRequest(HttpContext);
        }        
    }
}
