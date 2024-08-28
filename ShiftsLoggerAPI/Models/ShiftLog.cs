using System.Text.Json.Serialization;

namespace ShiftsLoggerAPI.Models;

public class ShiftLog
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime ShiftStartTime { get; set; }
    public DateTime ShiftEndTime { get; set; }
}

public class ShiftLogDTO
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime ShiftStartTime { get; set; }
    public DateTime ShiftEndTime { get; set; }

    public ShiftLogDTO(ShiftLog shiftLog)
    {
        Id = shiftLog.Id;
        FirstName = shiftLog.FirstName;
        LastName = shiftLog.LastName;
        ShiftStartTime = shiftLog.ShiftStartTime;
        ShiftEndTime = shiftLog.ShiftEndTime;
    }

    [JsonConstructor]
    public ShiftLogDTO(int id, string firstName, string lastName, DateTime shiftStartTime, DateTime shiftEndTime)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ShiftStartTime = shiftStartTime;
        ShiftEndTime = shiftEndTime;
    }

    public override string ToString() => $"ID: {Id}, " +
                                         $"Name: {FirstName} {LastName}, " +
                                         $"Start Time: {ShiftStartTime}, " +
                                         $"End Time: {ShiftEndTime}, " +
                                         $"Hours: {Math.Round((ShiftEndTime - ShiftStartTime).TotalHours, 2)}";
}