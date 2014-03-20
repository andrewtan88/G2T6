using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thrive2.Therapist
{
    public partial class MyPatientAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String pname = Request.QueryString["pname"];
            String assignmentnum = Request.QueryString["assignment"];

            if (tbx_assignment.Text.Length > 0)
            {
                tbx_assignment.Text = "";
            }
            string status = Request.QueryString["s"];
            if (status.Equals("o"))
            {
                Response.Redirect("MyPatient.aspx");
            }
            if (pname.Equals("Rachel"))
            {
                lbl_pname.Text = "Submitted by: " + pname;
                lbl_assignment.Text = "Assignment No." + assignmentnum;

                string startupPath = Server.MapPath("~");
                string[] paths = startupPath.Split('\\');
                StreamReader reader = new StreamReader(startupPath + "/Data/" + pname + "Assignment2.txt");
                while (reader.Peek() >= 0)
                {
                    tbx_assignment.Text += reader.ReadLine() + "\r\n\r\n";
                }
                reader.Close();
            }
            else
            {
                Response.Redirect("MyPatient.aspx");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            String pname = Request.QueryString["pname"];
            String assignmentnum = Request.QueryString["assignment"];
            string score = tbx_score.Text;
            string feedback = tbx_feedback.Text;
            string newassignmentnum = "3";
            string grade = "P";
            //TextWriter tw = new StreamWriter(Server.MapPath("/" + pname + "Assignment(marked).txt"));

            string startupPath = Server.MapPath("~");
            string[] paths = startupPath.Split('\\');

            StreamWriter tw = new StreamWriter(startupPath + "/Data/" + pname + "Assignment2(marked).txt");
            StreamWriter tw2 = new StreamWriter(startupPath + "/Data/" + "Patient.txt");

            // write a line of text to the file
            tw.WriteLine(assignmentnum);
            tw.WriteLine(feedback);
            tw.WriteLine(score);

            
            string message = "You have given " + pname + "a total score of " + score + ". ";

            int finalscore = Int32.Parse(score);
            if (finalscore < 5)
            {
                message += pname + " does not pass the assignment. Same assignment will be assigned to her.";
                newassignmentnum = "2";
                grade = "F";
                tw2.WriteLine("Hi Rachel, \r\n <a href='Module_AnalyticalReasoning.aspx'><b><u>Module 2</u></b></a> has been assigned to you. Your <a href='Assignment_AnalyticalReasoning.aspx'><b><u>assignment 2</u></b></a> has been reassigned.");
            }
            else
            {
                message += " Module " + newassignmentnum + " will be assigned to " + pname + ".";
                tw2.WriteLine("Hi Rachel, \r\n <a href='Module_AnIntroductiontoDepression.aspx'><b><u>Module 3</u></b></a> has been assigned to you. Your <a href='Assignment_AnalyticalReasoning.aspx'><b><u>assignment 2</u></b></a> has been reviewed.");
            }
            tw.WriteLine(grade);
            tw.WriteLine(newassignmentnum);

            // close the stream
            tw.Close();
            tw2.Close();

            //send email
            //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //msg.To.Add("trinang89@gmail.com");
            //msg.Subject = "You have a new notification on Thrivell!"; //subject title
            //msg.From = new System.Net.Mail.MailAddress("eatzfyp@gmail.com");
            //msg.Body = "Please login to Thrivell for your new notification!"; //body of the email
            //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            //smtp.Port = 587;
            //smtp.EnableSsl = true;
            //smtp.Credentials = new System.Net.NetworkCredential("eatzfyp@gmail.com", "eatzfyp2014");
            //smtp.Send(msg);
            //Response.Redirect("Therapist/Home.aspx");



            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success",
                "alert('" + message + "'); setInterval(function(){location.href='../Therapist/Home.aspx';},500);", true);
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Therapist/Home.aspx");
        }

    }
}