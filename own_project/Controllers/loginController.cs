using own_project.Models;
using own_project.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace own_project.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        loginRepository repo = new loginRepository();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(login1 lo)
        {
            if (lo.flag == "login")
            {
                lo.flag = "list";
                var data = repo.getloginlist(lo).Select(p => new { p.userid, p.username, p.userpassword }).FirstOrDefault();
                if (data == null)
                {
                    ViewBag.message = "Invalid UserName and Password";
                    return View();
                }
                else
                {

                    Session["userid"] = data.userid;
                    Session["username"] = data.username;
                    Session["userpassword"] = data.userpassword;
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (lo.flag == "insert")
            {
                var data = repo.Insert(lo);
                if (data == null)
                {
                    ViewBag.errormessage = "Please insert data ";
                    return View();
                }

                else
                {
                    ViewBag.SuccessMessage = "Data inserted successfully!";


                    Session["userid"] = lo.userid;
                    Session["username"] = lo.username;
                    Session["userpassword"] = lo.userpassword;
                    Session["useremail"] = lo.useremail;
                    Session["useraddress"] = lo.useraddress;
                    Session["usercontact"] = lo.usercontact;


                    return RedirectToAction("Index", "login");

                   
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "login");
        }


        [HttpGet]
        public ActionResult resetpassword(string authcode)
        {


            login1 a = new login1();
            a.authcode = authcode;
           
            a.flag = "list";

            var data = repo.check(a);

          
            if (data == "33")
              
            {
                ViewBag.authcode = authcode;
                return View();
            }
            else
            {
                ViewBag.error = "22";
                return View();

            }



        }

        [HttpPost]
        public ActionResult resetpassword(login1 a)
        {

            a.flag = "update";
            a.validate = true;
            var data = repo.update(a);



             if (data == "44")
            {
                ViewBag.error = "44";
                return View();

            }
           else if (data == "11")
            {
                //to send a mail after the  password being resetted
                var _repo = repo.resendmail(a);
                if (_repo == null)
                {
                    ViewBag.message = "error";
                    return View();
                }
                else
                {
                    string toemail = _repo;
                    string sendermail = "PUT YOUR EMAIL";
                    string senderpassword = "PUT YOUR VERIFICATION CODE OF EMAIL";
                    //in gmail go to 2-step verification,scroll down and go to app password and then generate code

                    string subject = "New password";


                    //string emailbody = $"Your New password is: " + a.userpassword;
                    string emailbody = $@"<!DOCTYPE html>
                            <html>
                            <head>
                                <style>
                                    body {{
                                        font-family: Arial, sans-serif;
                                        font-size: 14px;
                                        line-height: 1.5;
                                        color: #333;
                                    }}
                                    h1 {{
                                        font-size: 24px;
                                        font-weight: bold;
                                        margin-top: 0;
                                        margin-bottom: 20px;
                                        color: #555;
                                    }}
                                    p {{
                                        margin-top: 0;
                                        margin-bottom: 10px;
                                    }}
                                    .password {{
                                        background-color: #f0f0f0;
                                        padding: 10px;
                                        font-weight: bold;
                                    }}
                                    .cta {{
                                        display: inline-block;
                                        background-color: #00bfff;
                                        color: #fff;
                                        padding: 10px 20px;
                                        border-radius: 5px;
                                        text-decoration: none;
                                        margin-top: 20px;
                                    }}
                                    .cta:hover {{
                                        background-color: #009acd;
                                    }}
                                </style>
                            </head>
                            <body>
                                <h1 style=""color: #00bfff;"">New Password</h1>
                                <p>Dear <strong style=""color: #555;"">user</strong>,</p>
                                <p>Your new password is:</p>
                                <p class=""password"">{a.userpassword}</p>
                                <p>Please log in using your new password .</p>
                                <a href=""https://localhost:44328/login"" class=""cta"">Log In</a>
                                <p>Thank you for using our service,</p>
                                
                            </body>
                            </html>";
                    var senderEmail = new MailAddress(sendermail);
                    var receiverEmail = new MailAddress(toemail);
                    var smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendermail, senderpassword);
                    smtp.EnableSsl = true;
                    var msg = new MailMessage();
                    msg.To.Add(receiverEmail);
                    msg.Subject = subject;
                    msg.Body = emailbody;
                    msg.IsBodyHtml = true;
                    msg.From = senderEmail;
                    {
                        smtp.Send(msg);
                    }
                }
                //end
                return RedirectToAction("Index", "Login");
            }


            else
            {
                return View();
            }



        }


        public string randomcodegenerate()
        {

            Random rand = new Random();
            string abc = (rand.Next(9999999)).ToString();
            return abc;

            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@$%-+[]{}";

            //var random = new Random();
            //return new string(Enumerable.Repeat(chars, 50)
            //  .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        public string SendMail(string toemail)
        {
            try
            {

                string sendermail = "PUT YOUR EMAIL";
                string senderpassword = "PUT YOUR VERIFICATION CODE OF EMAIL";
                //in gmail go to 2-step verification,scroll down and go to app password and then generate code

                string subject = "Reset Password";
                var code = randomcodegenerate();


                login1 abc = new login1();

                abc.authcode = code;
                abc.useremail = toemail;
                abc.flag = "Insert";
                abc.validate = false;

                var data = repo.insert(abc);
                if (data == "00")
                {
                    var link = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    string emailbody = $"Please reset password. <b><a href='{link}login/resetpassword?authcode={code}'>Click here to reset password</b></a>";
                    var senderEmail = new MailAddress(sendermail);
                    var receiverEmail = new MailAddress(toemail);
                    var smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendermail, senderpassword);
                    smtp.EnableSsl = true;
                    var msg = new MailMessage();
                    msg.To.Add(receiverEmail);
                    msg.Subject = subject;
                    msg.Body = emailbody;
                    msg.IsBodyHtml = true;
                    msg.From = senderEmail;
                    {
                        smtp.Send(msg);
                    }
                    return "Email sent successfully";

                }

                else if (data == "99")
                {

                    return "Password can't be changed more than 3 times in a day";
                }

                else
                {

                    return "Email id doesn't exist in database";
                }



            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}