using ShiftsLoggerAPI.Models;
using System.Net.Http.Json;

namespace ShiftsLoggerConsoleApp.Controllers;

internal class ShiftController
{
    private const string ShiftLoggerUri = "https://localhost:7264/api/ShiftLogs";

    internal static async Task<IEnumerable<ShiftLogDTO>> GetAllShiftLogs()
    {
        try
        {
            var endpoint = ShiftLoggerUri;
            using HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            using HttpClient client = new(handler);
            var response = await client.GetFromJsonAsync<IEnumerable<ShiftLogDTO>>(endpoint);
            return response ?? [];
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred with API call: {ex}");
        }
    }

    internal static async Task<ShiftLogDTO?> GetShiftLog(int id)
    {
        try
        {
            var endpoint = $"{ShiftLoggerUri}/{id}";
            using HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            using HttpClient client = new(handler);
            return await client.GetFromJsonAsync<ShiftLogDTO>(endpoint);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred with API call: {ex}");
        }
    }

    internal static async Task<bool> AddShift(ShiftLogDTO shift)
    {
        try
        {
            var endpoint = ShiftLoggerUri;
            using HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            using HttpClient client = new(handler);
            return (await client.PostAsJsonAsync(endpoint, shift)).IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred with API call: {ex}");
        }
    }

    internal static async Task<bool> DeleteShift(int id)
    {
        try
        {
            var endpoint = $"{ShiftLoggerUri}/{id}";
            using HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            using HttpClient client = new(handler);
            return (await client.DeleteAsync(endpoint)).IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred with API call: {ex}");
        }
    }

    internal static async Task<bool> UpdateShift(ShiftLogDTO shift)
    {
        try
        {
            var endpoint = $"{ShiftLoggerUri}/{shift.Id}";
            using HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            using HttpClient client = new(handler);
            return (await client.PutAsJsonAsync(endpoint, shift)).IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred with API call: {ex}");
        }
    }
}
