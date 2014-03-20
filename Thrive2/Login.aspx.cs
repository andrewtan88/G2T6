using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thrive2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String url = Request.QueryString["url"];

            if (url == null)
            {
                string startupPath = Server.MapPath("~");
                string[] paths = startupPath.Split('\\');

                StreamReader doc_reader = new StreamReader(startupPath + "/Data/" + "Doctor_Reset.txt");
                StreamReader patient_reader = new StreamReader(startupPath + "/Data/" + "Patient_Reset.txt");

                String doc_content = "";
                String patient_content = "";
                while (doc_reader.Peek() >= 0)
                {
                    doc_content += doc_reader.ReadLine();
                }
                doc_reader.Close();


                while (patient_reader.Peek() >= 0)
                {
                    patient_content += patient_reader.ReadLine();
                }
                patient_reader.Close();


                StreamWriter doc_write = new StreamWriter(startupPath + "/Data/" + "Doctor.txt");
                StreamWriter patient_write = new StreamWriter(startupPath + "/Data/" + "Patient.txt");
                StreamWriter ass_write = new StreamWriter(startupPath + "/Data/" + "RachelAssignment2(marked).txt");

                doc_write.WriteLine(doc_content);
                patient_write.WriteLine(patient_content);
                ass_write.WriteLine("\r\n \r\n \r\n F");

                doc_write.Close();
                patient_write.Close();
                ass_write.Close();
            }

        }
        protected void send_OnClick(object sender, EventArgs e)
        {
            String em = email.Text;
            String p = password.Text;

            if (em.ToString().Equals("david@ktph.com") && p.ToString().Equals("12345"))
            {
                Response.Redirect("Therapist/Home.aspx");
            }
            else if (em.ToString().Equals("rachel@gmail.com") && p.ToString().Equals("12345"))
            {
                Response.Redirect("Patient/Home.aspx");
            }
        }
    }
}