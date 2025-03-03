using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Sumukha.Data
{
    public class EmailService : IEmailService
    {
        private const string CompanyEmail = "info@sumukha.in"; // Change to your company's email
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SmtpUsername = "vinaycmusale@gmail.com";  // Your SMTP email
        private const string SmtpPassword = "zynearjfitfodesa";  // Your App Password

        public async Task<bool> SendNotificationAsync(string Name, string Email, string Subject, string Message)
        {
            try
            {
                // 📌 1. Send Contact Message to Company
                var companyEmail = new MimeMessage();
                companyEmail.From.Add(new MailboxAddress("Website Contact Form", SmtpUsername));
                companyEmail.To.Add(new MailboxAddress("Sumukha", CompanyEmail));
                companyEmail.Subject = $"New Contact Form Submission: {Subject}";
                companyEmail.Body = new TextPart("plain")
                {
                    Text = $"Sender Name: {Name}\nSender Email: {Email}\n\nMessage:\n{Message}"
                };

                // 📌 2. Send Acknowledgment Email to Sender
                var acknowledgmentEmail = new MimeMessage();
                acknowledgmentEmail.From.Add(new MailboxAddress("Sumukha Technologies", SmtpUsername));
                acknowledgmentEmail.To.Add(new MailboxAddress(Name, Email));
                acknowledgmentEmail.Subject = "Thank You for Contacting Us!";
                acknowledgmentEmail.Body = new TextPart("plain")
                {
                    Text = $"Dear {Name},\n\nThank you for reaching out! We have received your message:\n\n\"{Message}\"\n\nOur team will get back to you soon.\n\nBest regards,\nSumukha Technology"
                };

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(SmtpServer, SmtpPort, SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(SmtpUsername, SmtpPassword);

                    // Send both emails
                    await smtpClient.SendAsync(companyEmail);
                    await smtpClient.SendAsync(acknowledgmentEmail);

                    await smtpClient.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email Sending Failed: {ex.Message}");
                return false;
            }
        }
    }
}
