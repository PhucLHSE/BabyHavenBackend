using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

public static class IpAddressHelper
{
    public static string GetClientIpAddress(HttpContext httpContext)
    {
        if (httpContext == null)
            return "Unknown";

        // Lấy IP từ X-Forwarded-For (nếu chạy sau proxy/load balancer)
        if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues forwardedFor))
        {
            var ipList = forwardedFor.ToString().Split(',');
            var clientIp = ipList.FirstOrDefault()?.Trim();
            if (IsValidIpAddress(clientIp))
                return clientIp;
        }

        // Lấy IP từ RemoteIpAddress (nếu không có proxy)
        var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString();
        if (IsValidIpAddress(remoteIp))
            return remoteIp;

        return "Unknown";
    }

    private static bool IsValidIpAddress(string ip)
    {
        return !string.IsNullOrEmpty(ip) &&
               IPAddress.TryParse(ip, out var address) &&
               (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ||
                address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);
    }
}
