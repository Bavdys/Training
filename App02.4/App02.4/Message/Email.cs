using App2._4.Message;
using MailKit.Net.Smtp;
using MimeKit;

namespace App2._4
{
    class Email:IMessage
    {
        public Email(string recipientName, string recipientMail)
        {
            RecipientName = recipientName;
            RecipientMail = recipientMail;
        }
       
        public string RecipientName { get; set; }
        public string RecipientMail { get; set; }

        public async void Send(object message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Sender", "sender@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(RecipientName, RecipientMail));
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message.ToString()
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("sender@gmail.com", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
