using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        // Pagination
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public int? TotalItems { get; set; }
        public int? TotalPages => TotalItems.HasValue && PageSize.HasValue && PageSize.Value > 0
            ? (int)Math.Ceiling((double)TotalItems.Value / PageSize.Value)
            : null;
        public bool? HasPrevious => CurrentPage.HasValue && CurrentPage > 1;
        public bool? HasNext => CurrentPage.HasValue && TotalPages.HasValue && CurrentPage < TotalPages;

        public BaseResponse()
        {
            Errors = new List<string>();
        }

        // Success without pagination
        public static BaseResponse<T> SuccessResponse(T data, string message = "Operation completed successfully")
        {
            return new BaseResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        // Success with pagination
        public static BaseResponse<T> SuccessResponse(T data, int currentPage, int pageSize, int totalItems, string message = "Operation completed successfully")
        {
            return new BaseResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        // Failure without errors list
        public static BaseResponse<T> FailureResponse(string message)
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

        // Failure with errors list
        public static BaseResponse<T> FailureResponse(string message, List<string> errors)
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors
            };
        }

        // Additional helper methods
        public static BaseResponse<T> NotFoundResponse(string message = "Resource not found")
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

        public static BaseResponse<T> BadRequestResponse(string message = "Bad request")
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

        public static BaseResponse<T> UnauthorizedResponse(string message = "Unauthorized")
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
