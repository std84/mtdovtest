using System.Collections.Generic;
using System.Threading.Tasks;
using missiontest.MODAL;

//using Newtonsoft.Json.Linq;  
using System.IO;  
using System.Net.Mail;  
using System.Threading.Tasks;  
using System.Web;  
 using System.Net.Http;
using System;

namespace missiontest.REPOSITORY
{
    public class mailRepository: IMailRepository
    {
        public string createBody(IList<Mission> missions){
            string html="";
            html = "<table>";
            html += "<tr>";
            html += "<th>";
            html += "mission name";
            html += "</th>";

           // html += "<th>";
            //html += "mission description";
            //html += "</th>";
            html += "<th>";
            html += "mission active";
            html += "</th>";
            html += "<th>";
            html += "mission done";
            html += "</th>";
            html += "<th>";
            html += "mission date";
            html += "</th>";
            html += "</tr>";
           
            foreach (var missionobj in missions) {
                html += "<tr>";
                html += "<td>";
                html += missionobj.name;
                html += "</td>";

               // html += "<td>";
               // html += missionobj.description;
               // html += "</td>";
                html += "<td>";
                html += missionobj.isActive;
                html += "</td>";
                html += "<td>";
                html += missionobj.isDone;
                html += "</td>";
                html += "<td>";
                html += missionobj.missionDate.ToString();
                html += "</td>";
                html += "</tr>";
                Console.WriteLine(missionobj);
            }
            
            html += "</table>";

            return html;
        }
      
        public  bool ShareMission(Email mail, IList<Mission> missions)
        {
           string subject = mail.subject;// "Email Subject";
           string htbody = createBody(missions);
            string body = htbody;//"Email body";
            string FromMail =  "vizen84@gmail.com";
            string emailTo = mail.toemail;// "reciever@reckonbits.com.pk";
            MailMessage smail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            smail.From = new MailAddress(FromMail);
            smail.To.Add(emailTo);
            smail.Subject = subject;
            smail.Body = body;
            smail.IsBodyHtml = true;
            SmtpServer.Port = 587; 
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("vizen84@gmail.com", "3333333vv");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(smail);
            return true;
        }
        public bool SendMission(Email mail, IList<Mission> missions)
        {
   string subject = mail.subject;// "Email Subject";
           string htbody = createBody(missions);
            string body = htbody;//"Email body";
            string FromMail =  "vizen84@gmail.com";
            string emailTo = mail.toemail;// "reciever@reckonbits.com.pk";
            MailMessage smail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            smail.From = new MailAddress(FromMail);
            smail.To.Add(emailTo);
            smail.Subject = subject;
            smail.Body = body;
            smail.IsBodyHtml = true;
            SmtpServer.Port = 587; 
            SmtpServer.Credentials = new System.Net.NetworkCredential("vizen84@gmail.com", "3333333vv");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(smail);
            return true;
          
        }
    }
}