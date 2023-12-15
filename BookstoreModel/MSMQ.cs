using System;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using Experimental.System.Messaging;

namespace FundooModel.User
{
    public class MSMQ
    {

        MessageQueue messageQueue = new MessageQueue();
        public void sendData2Queue(String token,String email)
        {
            messageQueue.Path = @".\private$\token";
            if (!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += (sender,e) => MessageQ_ReceiveCompleted(sender, e, email);  //Delegate
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }
        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e,string email)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "Bookstore Notes App Reset Link";
                string body = "http://localhost:4200/password/reset/" + token;
                var SMTP = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("shrey0683@gmail.com", "dtijkisinohllanp"),
                    EnableSsl = true
                };
                SMTP.Send("shrey0683@gmail.com", email, subject, body);
                // Process the logic be sending the message
                //Restart the asynchronous receive operation.
                messageQueue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
