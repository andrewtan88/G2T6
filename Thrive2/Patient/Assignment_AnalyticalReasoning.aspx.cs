using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thrive2.Patient
{
    public partial class Assignment_AnalyticalReasoning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string startupPath = Server.MapPath("~");
            string[] paths = startupPath.Split('\\');
            StreamReader reader = new StreamReader(startupPath + "/Data/" + "RachelAssignment2(marked).txt");
            //StreamReader reader2 = new StreamReader(startupPath + "/Data/" + "RachelAssignment2.txt");

            string[] feedback = new string[6];
            if (reader.Peek() > 0)
            {
                for (int i = 0; i < reader.Peek(); i++)
                {
                    feedback[i] = reader.ReadLine();
                }
                string s = feedback[3];

                if (s.Equals("P"))
                {
                    tbx_assignment.Visible = true;
                    tbx_feedback.Visible = true;
                    btn_submit.Visible = false;
                    tbx_q1.Visible = false;
                    tbx_q2.Visible = false;
                    tbx_q3.Visible = false;
                    cell_q2.Visible = false;
                    cell_q3.Visible = false;
                    lbl_q1.Visible = false;

                    tbx_feedback.Text = "Feedback" + "\r\n\r\n" + feedback[1];
                }
                else if (s.Equals("F"))
                {
                    tbx_assignment.Visible = false;
                    tbx_feedback.Visible = false;
                }

                //while (reader2.Peek() >= 0)
                //{
                //    tbx_assignment.Text += reader2.ReadLine() + "\r\n";
                //}

                reader.Close();
                //reader2.Close();

                string pname = "Rachel";
                string assignmentno = "Assignment 2";
                string moduletitle = "Analytical Reasoning";
                //string status = "Submitted";
                string q1 = "Sarah can do blah balh blah";
                string q2 = "1,3,5";
                string q3 = "I will handle the situatin by";
                string submissiondate = System.DateTime.Now.ToString();

                tbx_assignment.Text += "Module Title: Analytical Reasoning" + "\r\n" + "Assignment No.: " + assignmentno
                    + "\r\n" + "Submitted on:" + submissiondate + "\r\n" + "Q1. Based on the information provided, what do you think Anita should do to help Sarah?"
                    + "\r\n" + q1 + "\r\n" + "Q2: Select all the items on the list of notes that you feel are relevant to solving this problem."
                    + "\r\n" + q2 + "\r\n" + "Q3: If you are Anita, how will you handle the sitation?" + "\r\n" + q3;

            }
        }
    

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Patient/Home.aspx");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string startupPath = Server.MapPath("~");
            try
            {
                string pname = "Rachel";
                string assignmentno = "Assignment 2";
                string moduletitle = "Analytical Reasoning";
                //string status = "Submitted";
                string q1 = tbx_q1.Text.ToString();
                string q2 = tbx_q2.Text.ToString();
                string q3 = tbx_q3.Text.ToString();
                string submissiondate = System.DateTime.Now.ToString();

                TextWriter tw = new StreamWriter(startupPath + "/Data/" + pname + "Assignment2.txt");

                // write a line of text to the file
                tw.WriteLine("Module Title: " + moduletitle);
                tw.WriteLine("Assignment No.:" + assignmentno);
                tw.WriteLine("Submitted on:" + submissiondate);
                tw.WriteLine("Q1. Based on the information provided, what do you think Anita should do to help Sarah?");
                tw.WriteLine(q1);
                tw.WriteLine("Q2: Select all the items on the list of notes that you feel are relevant to solving this problem.");
                tw.WriteLine(q2);
                tw.WriteLine("Q3: If you are Anita, how will you handle the sitation?");
                tw.WriteLine(q3);

                // close the stream
                tw.Close();

                //send email
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("trinang89@gmail.com");
                message.Subject = "Rachel have submitted her assignment!"; //subject title
                message.From = new System.Net.Mail.MailAddress("eatzfyp@gmail.com");
                message.Body = "Patient, Rachel have completed her assignment. Please proceed to Thrivell to provide her with your feedback!"; //body of the email
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("eatzfyp@gmail.com", "eatzfyp2014");
                smtp.Send(message);

                string print = "You have successfully submitted the assignment.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success",
                    "alert('" + print + "'); setInterval(function(){location.href='../Patient/Home.aspx';},500);", true);

            }
            catch (Exception ex)
            {
            }
        }
    }
}