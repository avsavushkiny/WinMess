using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq.Expressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WinMess
{
    public partial class Form1 : Form
    {
        string path;
        string path_to_message_files;
        string path_to_preferences = "c:\\Winme";
        string path_to_preferences_file = "c:\\Winme\\preferences.txt";

        private void path_to_file()
        {
            try
            {
                path_to_message_files = File.ReadAllText("C:\\Winme\\preferences.txt");
                label2.Text = path_to_message_files;

                if (path_to_message_files == "")
                {
                    textBox1.Text = "The file preferences.txt is empty! Specify the path to the message file!";
                    textBox1.Enabled = false;

                    message_box("The file preferences.txt is empty! " +
                        "\nSpecify the path to the message file!", "Error", MessageBoxIcon.Error);

                    label2.Text = "Empty";
                }
            }
            catch
            {
                message_box("Unforeseen error related to the configuration file! " +
                    "\n Check the location of the preferences.txt file." +
                    "\n\n" + path_to_preferences_file,
                    "Error", MessageBoxIcon.Error);
            }
        }

        private void message_box(string Text, String Title, MessageBoxIcon ICON)
        {
            MessageBox.Show(Text, Title,
                    MessageBoxButtons.OK,
                    ICON);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TextWriter txt = new StreamWriter(path_to_message_files);
                txt.WriteLine(textBox1.Text);
                txt.Close();

                textBox1.ForeColor = Color.DarkGreen;

                message_box("Message successfully sent to " + path_to_message_files, "Information", MessageBoxIcon.Information);
                textBox1.Clear();

                textBox1.ForeColor = Color.Black;
            }
            catch
            {
                textBox1.ForeColor = Color.DarkRed;

                message_box("Message not sent! Possible reasons:" +
                    "\n- the path to the message.txt file is not specified" +
                    "\n- problems accessing the message.txt file",
                    "Warning", MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(path_to_preferences_file) == false)
            {
                System.IO.Directory.CreateDirectory(path_to_preferences);
                File.Create(path_to_preferences_file).Close();

                textBox1.Text = "The preferences.txt file has been created! Open the file " + path_to_preferences_file +
                    ", write the path to the message.txt file. For example: \\\\Server\\D\\message.txt";
                textBox1.Enabled = false;

                message_box("The preferences.txt file has been created! \n" +
                    "\n- Open the file " + path_to_preferences_file + 
                    "\n- Write the path to the message.txt file" +
                    "\n\nFor example:" +
                    "\n\\\\Server\\D\\message.txt", "Information", MessageBoxIcon.Information);
            }
            else
            {
                //label2.ForeColor = System.Drawing.Color.DarkSlateGray; label2.Visible = true;
                label2.Text = path_to_message_files;
            }

            path_to_file();

        }
    }
}










//File.WriteAllText("C:\\Winme\\preferences.txt", String.Empty); // чистим файл
/*try
{
if (!File.Exists(path_to_message_files)) // If file does not exists
{
    File.Create(path_to_message_files).Close(); // Create file
}
else // If file already exists
{
    File.WriteAllText(path_to_message_files, String.Empty); // Clear file
}
}
catch
{
MessageBox.Show("Error! \n\n" +
                "Possible problems:\n" +
                "- preferences.txt file is missing\n" +
                "- file preferences.txt is empty");
}*/
