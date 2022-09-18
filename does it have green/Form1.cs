using System.IO;
namespace does_it_have_green
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool isGreen(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color colour = image.GetPixel(i, j);
                    if (colour.G > 185)
                    {
                        if (colour.R < 137 && colour.R > 40)
                        {
                            if (colour.B < 137 && colour.B > 40)
                            {
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            bool hasGreen = false;
            Bitmap newImage;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "the photo you want (*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    // Console.WriteLine(fileContent);
                    newImage = new Bitmap (filePath);
                    hasGreen = isGreen(newImage);
                    int index = 0;
                    for (int i = filePath.Length - 1; i > -1; i--)
                    {
                        if (filePath[i] == '\\')
                        {
                            index = i + 1;
                            break;
                        }
                    }
                    String path = filePath.Substring(index, filePath.Length - index);
                    // MessageBox.Show(hasGreen.ToString(), "File Content at path: " + filePath, MessageBoxButtons.OK);
                    label1.Text = String.Format("{0} {1} green", path, hasGreen ? "has" : "has no");
                }
            }

        }
    }
}