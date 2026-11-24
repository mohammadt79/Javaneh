public class AppSettings
{
    public CorsSettings Cors { get; set; } = new();

    public FileUploadSettings FileUpload { get; set; } = new();
}