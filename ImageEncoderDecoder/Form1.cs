using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private ImageEncoder outputImageEncoderVar = null;
        private String outputMessageVar = null;
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
                updateImageInfo(uploadedImage);
                changeImageInBox(picturePreview, uploadedImage, maxImagePreviewSize);
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
        

        // OUTPUT BUTTONS

        private void backButton_Click(object sender, EventArgs e)
        {
            backFromOutput();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (status == ImageEncoderView.DECODING && outputMessageVar != null)
            {
                decodeSave(new EncryptString(outputMessageVar)); // pass it a new encryptstring in case the user wants to reencrypt. (future feature). Could be made more efficient by using the encryptstring from the initial decode
            }
            else if (status == ImageEncoderView.ENCODING && outputImageEncoderVar != null)
            {
                encodeSave(outputImageEncoderVar);
            }
            else
            {
                switchPage(pages[ImageEncoderView.MAINPAGEINDEX]); // There is no output. go back to the main page an error probably occurred.
                debugPrint("An error probably occurred. On the output page, but there is no output...");
            }
        }

        // END OF OUTPUT BUTTONS


        // NON-ACTION LISTENER FUNCTIONS

        // ENCODE/DECODE FUNCTIONS


        /// <summary>
        /// CALL WHEN THE USER WANTS TO RETURN TO THE MAIN PAGE
        /// </summary>
        private void backFromOutput()
        {
            switchPage(pages[ImageEncoderView.MAINPAGEINDEX]);
            outputMessageVar = null;
            outputImageEncoderVar = null;
        }
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
        private void decodeSave(EncryptString decodedStringObject)
        {
            String filePath = saveOutput("Text File (*.txt) | *.txt");
            if (filePath.Length != 0)
            {
                decodedStringObject.saveToFile(filePath);
            }
        }
       
        private void encode(Image image, String encryptionKeyText, String positionSeedText, String message)
        {
            if ((message.Length > 0 && encryptionKeyText.Length > 0) && positionSeedText.Length > 0)
            {
                switchPage(pages[ImageEncoderView.OUTPUTPAGEINDEX]); // switch to the output page
                outputImageEncoderVar = new ImageEncoder(image, "bmp"); // assume bmp for now
                outputImageEncoderVar.embedMessage(message, encryptionKeyText, positionSeedText);
                changeImageInBox(outputPreview, outputImageEncoderVar.Image, maxImagePreviewSize);
                outputImageEncoderVar.saveImageToFile("C:/Users/navba/Desktop/newImage.bmp");
            }
        }
        /// <summary>
        /// Called when the user tries to save after encoding
        /// </summary>
        private void encodeSave(ImageEncoder decodedImage)
        {
            String filePath = saveOutput("Image Files(*.bmp)|*.bmp");
            if (filePath.Length > 0)
            {
                decodedImage.saveImageToFile(filePath);
            }
        }
        // END OF ENCODE/DECODE FUNCTIONS



        // Private utility functions
        
        /// <summary>
        /// Will scale the picturebox to the image and maintain the image's dimensions. Will actually scale the picture box to the output dimensions and the image will fill the box (but the box will be the right dimensions)
        /// </summary>
        /// <param name="picutreBox"></param>
        /// <param name="image"></param>
        private void changeImageInBox(PictureBox box, Image image, Size pictureBoxIdealDimensions)
        {
            Image copyImage = (Image)image.Clone(); // in case pictureBox modifies the image when the image fills it
            Size newDimensions = scaleImageToSize(copyImage.Size, pictureBoxIdealDimensions);
            box.Size = newDimensions;
            box.Image = copyImage;
        }
        private void hideAllPages(Panel[] pages)
        {
            foreach (Panel page in pages)
            {
                page.Visible = false;
            }
        }
        private void debugPrint(String message)
        {
            Debug.WriteLine("GUI ERROR: " + message);
        }
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
        private String saveOutput(String filter)
        {
            String result;
            save.Filter = filter;
            if (save.ShowDialog() == DialogResult.OK)
            {
                result = save.FileName;
            } else
            {
                result = "";
            }
            return result;
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

        /// <summary>
        /// Will update the action status based on buttons. Returns the OLD status
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Updates the image info label.
        /// </summary>
        /// <param name="newImage"></param>
        private void updateImageInfo(Image newImage)
        {
            int numberOfPixels = ImageEncoder.getNumberOfPixels(newImage);
            int numberOfLetters = ImageEncoder.getNumberOfLetters(numberOfPixels);
            String info = "The image is " + numberOfPixels + " pixels large. It can store " + numberOfLetters + " letters. Note, the letter contraint is set by the encryption algos used by this software. Different algos can yield a different limit.";
            pictureInfoLabel.Text = info;
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

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureInfoLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
