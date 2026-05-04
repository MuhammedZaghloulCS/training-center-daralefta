using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.IRepositories.ISysRepositories
{
    public interface ISysAccTransactionRepository
    {
        Task<SessionAttendanceResponse> GetSessionAttendance(int sessionId);
    }

    public class SessionAttendanceResponse
    {
        public int SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public string Topic { get; set; }
        public bool IsFutureSession { get; set; }
        public List<AttendanceDto> PresentStudents { get; set; } = new();
        public List<AttendanceDto> AbsentStudents { get; set; } = new();
        public List<AttendanceDto> PresentLecturers { get; set; } = new();
        public List<AttendanceDto> AbsentLecturers { get; set; } = new();
    }

    public class AttendanceDto
    {
        public Guid UserId { get; set; }
        public string Pin { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstEntry { get; set; }
        public string LastExit { get; set; }
        public string Duration { get; set; }
        public bool IsPresent { get; set; }
    }
}
