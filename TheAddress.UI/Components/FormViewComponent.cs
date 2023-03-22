using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using TheAddress.DAL.Data;
using TheAddress.DAL.DBModel;

namespace TheAddress.UI.Components
{
    public class FormViewComponent : ViewComponent
    {
        readonly AppDBContext db;
        public FormViewComponent(AppDBContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(Contact contacts)
        {
            if (ModelState.IsValid)
            {

                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                var host = configuration["Gmail:Host"];
                var port = int.Parse(configuration["Gmail:Port"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var displayName = configuration["Gmail:DisplayName"];

                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                MailMessage mail = new MailMessage($"{contacts.Email}", $"{username}");
                mail.Subject = displayName;


                mail.Body = $@"
               <html>
               <head> 
               <style>
              

              </style>
              </head>
              <body>
              <h2>Email: {contacts.Email} </h2>
              <h3>Name: {contacts.Name}</h2> 
               <h3>{contacts.Phone}</h3>
            </body>
            </html>";
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(host, port);
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);


                ViewBag.msg = "Sizin müraciətiniz uğurla göndərildi.";

                db.Contacts.Add(contacts);
                db.SaveChanges();
                return View();

            }
            return View();
        }
    }
}