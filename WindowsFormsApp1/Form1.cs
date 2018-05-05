using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ImageEncoder : Form
    {
        public static int ENCODING = 1;
        public static int DECODING = -1;
        public static int NEUTRAL = 0;

        private String defaultDialogueValue;

        private Size maxImagePreviewSize;
        private int status = ImageEncoder.NEUTRAL; // represents what the user is trying to do
        public ImageEncoder()
        {
            InitializeComponent();
            maxImagePreviewSize = picturePreview.Size;
            defaultDialogueValue = imageUploadDialog.FileName;

            // Disable action buttons until an image is selected
            toggleActionButtons(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            imageUploadDialog.Filter = "Image Files(*.BMP) | *.BMP"; // Only allow BMP files
            imageUploadDialog.ShowDialog();

            String filepath = imageUploadDialog.FileName;

            if (filepath.Length > 0 && !filepath.Equals(defaultDialogueValue)) // returns the defaultDialogueValue if no image was selected
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(filepath);
                System.Drawing.Image previewImage = (System.Drawing.Image)image.Clone(); // Clones the Image so it can be modified to fit the preview box

                // Resize the piciture box to maintain image dimensions
                Size imageDimensions = previewImage.Size;
                Size newImageDimensions = scaleImageToSize(imageDimensions, maxImagePreviewSize); // maxImagePreviewSize set in constructor of form
                Console.Write(newImageDimensions);
                // Apply resize and set image
                picturePreview.Size = newImageDimensions;
                picturePreview.Image = previewImage;

                // Enable action buttons
                toggleActionButtons(true);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            updateActionStatus(); // updates the status variable
        }
        private void decodeActionButton_CheckedChanged(object sender, EventArgs e)
        {
            updateActionStatus(); // updates the status variable

        }
        // Utility functions

        // Private utility functions
        private Size scaleImageToSize(Size imageSize, Size boxSize)
        {
            double ratio = (double)imageSize.Width / (double)imageSize.Height;
            int widthBasedOnHeight = (int)(ratio * boxSize.Height); // Use the boxes height?

            Size returnSize;
            if (widthBasedOnHeight > boxSize.Width) // the width based on the height is too large
            {
                int heightBasedOnWidth = (int)(boxSize.Width / ratio); // Yields a height
                returnSize = new Size(boxSize.Width, heightBasedOnWidth);
            }
            else
            {
                returnSize = new Size(widthBasedOnHeight, boxSize.Height);
            }
            return returnSize;
        }
        private void toggleActionButtons(Boolean newStatus)
        {
            RadioButton[] radioButtons = { decodeActionButton, encodeActionButton };
            radioToggle(radioButtons, newStatus);
        }

        private void radioToggle(RadioButton[] radioButtons, Boolean newStatus)
        {
            foreach (RadioButton button in radioButtons)
            {
                button.Enabled = newStatus;
            }
        }
        /**
         * Will update the action status based on buttons.
         * Returns the OLD status
         * */
        private int updateActionStatus()
        {
            int oldStatus = status;
            if (decodeActionButton.Checked)
            {
                status = ImageEncoder.DECODING;
            } else if (encodeActionButton.Checked)
            {
                status = ImageEncoder.ENCODING;
            } else
            {
                status = ImageEncoder.NEUTRAL;
            }
            return oldStatus;
        }


    }
}
