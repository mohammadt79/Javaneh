namespace Javaneh.API.Helpers;

public static class ResponseMessageHelper
{
    public static string GetMessage(int statusCode)
    {
        return statusCode switch
        {
            200 => "عملیات با موفقیت انجام شد",
            201 => "منبع با موفقیت ایجاد شد",
            400 => "درخواست نامعتبر است",
            401 => "احراز هویت انجام نشده است",
            403 => "شما اجازه دسترسی به این منبع را ندارید",
            404 => "منبع مورد نظر پیدا نشد",
            500 => "خطای داخلی سرور",
            _ => "درخواست پردازش شد"
        };
    }
}