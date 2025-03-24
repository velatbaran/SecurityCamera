namespace SecurityCamera.WebUI.Utils
{
    public class MailHelper
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly bool _enableSsl;

        public MailHelper(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, bool enableSsl = true)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _enableSsl = enableSsl;
        }
    }
}
