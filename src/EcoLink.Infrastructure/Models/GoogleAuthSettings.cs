namespace EcoLink.Infrastructure.Models;

public class GoogleAuthSettings
{
    public string type { get; set; } = string.Empty;
    public string project_id { get; set; } = string.Empty;
    public string private_key_id { get; set; } = string.Empty;
    public string private_key { get; set; } = string.Empty;
    public string client_email { get; set; } = string.Empty;
    public string client_id { get; set; } = string.Empty;
    public string auth_uri { get; set; } = string.Empty;
    public string token_uri { get; set; } = string.Empty;
    public string auth_provider_x509_cert_url { get; set; } = string.Empty;
    public string client_x509_cert_url { get; set; } = string.Empty;
    public string universe_domain { get; set; } = string.Empty;
}