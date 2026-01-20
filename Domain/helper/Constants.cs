using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public static class Message
    {
        // رسائل نجاح
        public const string Success = "تمت العملية بنجاح.";
        public const string UserCreated = "تم إنشاء المستخدم بنجاح.";
        public const string DataSaved = "تم حفظ البيانات بنجاح.";
        public const string LoginSuccess = "تم تسجيل الدخول بنجاح.";
        public const string RoleAssigned = "تم تعيين الدور للمستخدم بنجاح.";

        // رسائل خطأ
        public const string Error = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
        public const string UserNotFound = "المستخدم غير موجود.";
        public const string InvalidCredentials = "اسم المستخدم أو كلمة المرور غير صحيحة.";
        public const string AccessDenied = "ليس لديك صلاحية للوصول إلى هذا المورد.";
        public const string PasswordMismatch = "كلمة المرور غير متطابقة.";
        public const string RoleNotFound = "الدور المطلوب غير موجود.";
        public const string EmailAlreadyExists = "البريد الإلكتروني مستخدم من قبل.";

        // رسائل تحذير أو إشعار
        public const string MissingData = "يرجى التأكد من إدخال جميع البيانات المطلوبة.";
        public const string InvalidInput = "البيانات المدخلة غير صحيحة.";
        public const string SessionExpired = "انتهت صلاحية الجلسة، يرجى تسجيل الدخول مرة أخرى.";
        // رسائل المستخدمين 
        public const string PersonAdded = "تم إضافة المستخدم بنجاح.";
        public const string PersonUpdated = "تم تعديل بيانات المستخدم بنجاح.";
        public const string PersonDeleted = "تم حذف المستخدم بنجاح.";
        public const string PersonNotFound = "المستخدم المطلوب غير موجود في النظام.";
        public const string ErrorCreatingPerson = "تعذر إنشاء المستخدم بسبب خطأ غير متوقع.";
        public const string ErrorUpdatingPerson = "تعذر تعديل بيانات المستخدم بسبب خطأ غير متوقع.";
        public const string ErrorDeletingPerson = "تعذر حذف المستخدم بسبب خطأ غير متوقع.";
        public const string GeneralError = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
    }
}
