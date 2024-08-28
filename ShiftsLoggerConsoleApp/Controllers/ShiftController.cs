using ShiftsLoggerAPI.Models;
using System.Net.Http.Json;

namespace ShiftsLoggerConsoleApp.Controllers;

internal class ShiftController
{
    private const string ShiftLoggerUri = "https://localhost:7264/api/ShiftLogs";

    /// <summary>
    /// Retrieves all shift logs.
    /// </summary>
    /// <returns>A collection of shift logs.</returns>
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

    /// <summary>
    /// Retrieves a specific shift log by ID.
    /// </summary>
    /// <param name="id">The ID of the shift log to retrieve.</param>
    /// <returns>The shift log with the specified ID, or null if not found.</returns>
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

    /// <summary>
    /// Adds a new shift log.
    /// </summary>
    /// <param name="shift">The shift log to add.</param>
    /// <returns>True if the shift log was added successfully, false otherwise.</returns>
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

    /// <summary>
    /// Deletes a shift log by ID.
    /// </summary>
    /// <param name="id">The ID of the shift log to delete.</param>
    /// <returns>True if the shift log was deleted successfully, false otherwise.</returns>
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

    /// <summary>
    /// Updates a shift log.
    /// </summary>
    /// <param name="shift">The updated shift log.</param>
    /// <returns>True if the shift log was updated successfully, false otherwise.</returns>
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
