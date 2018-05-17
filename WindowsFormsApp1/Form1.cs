using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.bin;

namespace WindowsFormsApp1
{
    public partial class ImageEncoderView : Form
    {
        public static int ENCODING = 1;
        public static int DECODING = -1;
        public static int NEUTRAL = 0;

        public static int MAINPAGEINDEX = 0;
        public static int OUTPUTPAGEINDEX = 1;

        private Panel currentPage;
        private String defaultDialogueValue; // contains the default path of the dialogue BEFORE any image has been selected
        private Size maxImagePreviewSize;
        private ImageEncoder outputImageEncoderVar;
        private String outputMessageVar;
        private Panel[] pages;
        private Image uploadedImage;

        private int status = ImageEncoderView.NEUTRAL; // represents what the user is trying to do
        public ImageEncoderView()
        {
            InitializeComponent();
            maxImagePreviewSize = picturePreview.Size;
            defaultDialogueValue = imageUploadDialog.FileName;

            // Set default visibilities
            pages = getAllPages();
            hideAllPages(pages);
            switchPage(pages[ImageEncoderView.MAINPAGEINDEX]);

            outputMessage.ReadOnly = true; // Decoding message box is read only
            // Disable action buttons until an image is selected
            toggleActionButtons(false);
            updateActionStatus();


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
                uploadedImage = System.Drawing.Image.FromFile(filepath);
                Image previewImage = (System.Drawing.Image)uploadedImage.Clone(); // Clones the Image so it can be modified to fit the preview box

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
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void actionSelectPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void applyActionButton_Click(object sender, EventArgs e)
        {
            String encryptKeyText = encryptionKey.Text;
            String positionSeedText = positionSeed.Text;
            if (status == ImageEncoderView.ENCODING)
            {
                encode(uploadedImage, encryptKeyText, positionSeedText, messageBox.Text); // uploadedImage -- private variable where upload image is stored
            } else if (status == ImageEncoderView.DECODING)
            {
                decode(uploadedImage, encryptKeyText, positionSeedText);
            }
        }

        // Utility functions

        // ENCODE/DECODE FUNCTIONS
        private void decode(Image image, String encryptionKeyText, String positionSeedText)
        {
            if (encryptionKeyText.Length > 0 && positionSeedText.Length > 0)
            {
                switchPage(pages[ImageEncoderView.OUTPUTPAGEINDEX]); // switch to the output page
                ImageEncoder imageEncode = new ImageEncoder(image, "bmp"); // assume bmp for now
                outputMessageVar = imageEncode.decrypt(encryptionKeyText, positionSeedText, -1);
                if (outputMessageVar.Length == 0)
                {
                    outputMessageVar = "The image could not be succesfully. Maybe you have the wrong encryption key and/or position seed.";
                }
                outputMessage.Text = outputMessageVar;
            }
        }
        /// <summary>
        /// Called when the user tries to save after decoding
        /// </summary>
        private void decodeSave()
        {

        }
        ///
        private void encode(Image image, String encryptionKeyText, String positionSeedText, String message)
        {
            if ((message.Length > 0 && encryptionKeyText.Length > 0) && positionSeedText.Length > 0)
            {
                switchPage(pages[ImageEncoderView.OUTPUTPAGEINDEX]); // switch to the output page
                outputImageEncoderVar = new ImageEncoder(image, "bmp"); // assume bmp for now
                outputImageEncoderVar.embedMessage(message, encryptionKeyText, positionSeedText);
                outputImageEncoderVar.saveImageToFile("C:/Users/navba/Desktop/newImage.bmp");
            }
        }
        /// <summary>
        /// Called when the user tries to save after encoding
        /// </summary>
        private void encodeSave()
        {

        }

        // Private utility functions
        private String getActionString(int status)
        {
            String returnString;
            if (status == ImageEncoderView.DECODING)
            {
                returnString = "Decode";
            } else if (status == ImageEncoderView.ENCODING)
            {
                returnString = "Encode";
            } else
            {
                returnString = "No Action Selected";
            }
            return returnString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>an array of panels with all the pages</returns>
        private Panel[] getAllPages()
        {
            Panel[] pages = { mainPage, outputPage };
            return pages;
        }

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
        private void hideAllPages(Panel[] pages)
        {
            foreach (Panel page in pages)
            {
                page.Visible = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPage">The new page (panel object)</param>
        private void switchPage(Panel newPage)
        {
            newPage.Visible = true;
            if (currentPage != null)
            {
                currentPage.Visible = false; // make the current page invisible
            }
            currentPage = newPage;
        }
        private void toggleActionButtons(Boolean newStatus)
        {
            RadioButton[] radioButtons = { decodeActionButton, encodeActionButton };
            radioToggle(radioButtons, newStatus);
        }

        private void radioToggle (RadioButton[] radioButtons, Boolean newStatus)
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
        private int updateActionStatus ()
        {
            int oldStatus = status;
            if (decodeActionButton.Checked)
            {
                status = ImageEncoderView.DECODING;

                parametersPanel.Visible = true;
                messageParameterPanel.Visible = false;
                applyActionButton.Text = getActionString(status);
                applyActionButton.Visible = true;
                decryptOutputPanel.Visible = true; // set this visibility ahead of time. will still be invisible because parent is hidden
                encryptOutputPanel.Visible = false;
            }
            else if (encodeActionButton.Checked)
            {
                status = ImageEncoderView.ENCODING;

                parametersPanel.Visible = true;
                messageParameterPanel.Visible = true;
                applyActionButton.Text = getActionString(status);
                applyActionButton.Visible = true;
                encryptOutputPanel.Visible = true; // set this visibility ahead of time. will still be invisible because parent is hidden
                decryptOutputPanel.Visible = false;
           
            }
            else
            {
                status = ImageEncoderView.NEUTRAL;
                parametersPanel.Visible = false;
                applyActionButton.Visible = false;
                applyActionButton.Text = getActionString(status);
            }
            return oldStatus;
        }

        private void outputLabel_Click(object sender, EventArgs e)
        {

        }

        private void mainPage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void outputPage_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
