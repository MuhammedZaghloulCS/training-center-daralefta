using Application.Features.Attendance.Queries.GetSessionAttendance;
using ClosedXML.Excel;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get attendance for a specific session
        /// </summary>
        [HttpGet("session/{sessionId:int}")]
        public async Task<IActionResult> GetSessionAttendance(int sessionId)
        {
            var result = await _mediator.Send(new GetSessionAttendanceQuery { SessionId = sessionId });
            
            if (result == null)
                return NotFound(new { Success = false, Message = "Session not found" });
            
            return Ok(new { Success = true, Data = result });
        }

        /// <summary>
        /// Export attendance to Excel
        /// </summary>
        [HttpGet("session/{sessionId:int}/export")]
        public async Task<IActionResult> ExportSessionAttendance(int sessionId)
        {
            var result = await _mediator.Send(new GetSessionAttendanceQuery { SessionId = sessionId });
            
            if (result == null)
                return NotFound(new { Success = false, Message = "Session not found" });

            using var workbook = new XLWorkbook();
            
            // Students Sheet
            var studentsSheet = workbook.Worksheets.Add("Students");
            AddAttendanceSheet(studentsSheet, result, result.PresentStudents, result.AbsentStudents, "المتدربين");

            // Lecturers Sheet
            var lecturersSheet = workbook.Worksheets.Add("Lecturers");
            AddAttendanceSheet(lecturersSheet, result, result.PresentLecturers, result.AbsentLecturers, "المحاضرين");

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"Attendance_{result.Topic}_{result.SessionDate:yyyy-MM-dd}.xlsx";
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        private void AddAttendanceSheet(IXLWorksheet sheet, SessionAttendanceResponse session, 
            List<AttendanceDto> present, List<AttendanceDto> absent, string title)
        {
            // Header
            sheet.Cell(1, 1).Value = $"تقرير حضور {title} - {session.Topic}";
            sheet.Cell(1, 1).Style.Font.Bold = true;
            sheet.Cell(1, 1).Style.Font.FontSize = 14;
            sheet.Range(1, 1, 1, 7).Merge();
            sheet.Cell(2, 1).Value = $"التاريخ: {session.SessionDate:yyyy-MM-dd}";
            sheet.Range(2, 1, 2, 7).Merge();

            // Present Section
            int row = 4;
            sheet.Cell(row, 1).Value = "الحاضرون";
            sheet.Cell(row, 1).Style.Font.Bold = true;
            sheet.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightGreen;
            sheet.Range(row, 1, row, 7).Merge();

            row++;
            AddTableHeader(sheet, row);
            row++;
            
            foreach (var p in present)
            {
                AddAttendanceRow(sheet, row, p);
                row++;
            }

            // Absent Section
            row++;
            sheet.Cell(row, 1).Value = "الغائبون";
            sheet.Cell(row, 1).Style.Font.Bold = true;
            sheet.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightPink;
            sheet.Range(row, 1, row, 7).Merge();

            row++;
            AddTableHeader(sheet, row);
            row++;
            
            foreach (var a in absent)
            {
                AddAttendanceRow(sheet, row, a);
                row++;
            }

            // Summary
            row++;
            sheet.Cell(row, 1).Value = $"إجمالي الحاضرين: {present.Count} | إجمالي الغائبين: {absent.Count}";
            sheet.Range(row, 1, row, 7).Merge();

            // Adjust columns
            sheet.Columns().AdjustToContents();
            sheet.RightToLeft = true;
        }

        private void AddTableHeader(IXLWorksheet sheet, int row)
        {
            sheet.Cell(row, 1).Value = "الاسم";
            sheet.Cell(row, 2).Value = "البريد الالكتروني";
            sheet.Cell(row, 3).Value = "رقم الهاتف";
            sheet.Cell(row, 4).Value = "الدخول";
            sheet.Cell(row, 5).Value = "الخروج";
            sheet.Cell(row, 6).Value = "المدة";
            sheet.Cell(row, 7).Value = "الحالة";

            for (int col = 1; col <= 7; col++)
            {
                sheet.Cell(row, col).Style.Font.Bold = true;
                sheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            }
        }

        private void AddAttendanceRow(IXLWorksheet sheet, int row, AttendanceDto dto)
        {
            sheet.Cell(row, 1).Value = dto.FullName;
            sheet.Cell(row, 2).Value = dto.Email ?? "-";
            sheet.Cell(row, 3).Value = dto.PhoneNumber ?? "-";
            sheet.Cell(row, 4).Value = dto.FirstEntry;
            sheet.Cell(row, 5).Value = dto.LastExit;
            sheet.Cell(row, 6).Value = dto.Duration;
            sheet.Cell(row, 7).Value = dto.IsPresent ? "حاضر" : "غائب";
            sheet.Cell(row, 7).Style.Fill.BackgroundColor = dto.IsPresent ? XLColor.LightGreen : XLColor.LightPink;
        }
    }
}
