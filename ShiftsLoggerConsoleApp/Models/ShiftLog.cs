namespace ShiftsLoggerConsoleApp.Models;

public class ShiftLog
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime ShiftStartTime { get; set; }
    public DateTime ShiftEndTime { get; set; }
}

public class ShiftLogDTO(ShiftLog shiftLog)
{
    public int Id { get; set; } = shiftLog.Id;
    public string? FirstName { get; set; } = shiftLog.FirstName;
    public string? LastName { get; set; } = shiftLog.LastName;
    public DateTime ShiftStartTime { get; set; } = shiftLog.ShiftStartTime;
    public DateTime ShiftEndTime { get; set; } = shiftLog.ShiftEndTime;
}