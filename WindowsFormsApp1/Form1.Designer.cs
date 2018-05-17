namespace WindowsFormsApp1
{
    partial class ImageEncoderView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEncoderView));
            this.imageUploadDialog = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.picturePreview = new System.Windows.Forms.PictureBox();
            this.actionSelectPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.encodeActionButton = new System.Windows.Forms.RadioButton();
            this.decodeActionButton = new System.Windows.Forms.RadioButton();
            this.parametersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.requiredParametersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.encryptionKey = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.positionSeed = new System.Windows.Forms.TextBox();
            this.messageParameterPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.applyActionButton = new System.Windows.Forms.Button();
            this.outputPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.outputLabel = new System.Windows.Forms.Label();
            this.outputMessage = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.outputPage = new System.Windows.Forms.Panel();
            this.mainPage = new System.Windows.Forms.Panel();
            this.decryptOutputPanel = new System.Windows.Forms.Panel();
            this.encryptOutputPanel = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).BeginInit();
            this.actionSelectPanel.SuspendLayout();
            this.parametersPanel.SuspendLayout();
            this.requiredParametersPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.messageParameterPanel.SuspendLayout();
            this.outputPanel.SuspendLayout();
            this.outputPage.SuspendLayout();
            this.mainPage.SuspendLayout();
            this.decryptOutputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageUploadDialog
            // 
            this.imageUploadDialog.FileName = "imageDialogue";
            resources.ApplyResources(this.imageUploadDialog, "imageUploadDialog");
            this.imageUploadDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // picturePreview
            // 
            this.picturePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.picturePreview, "picturePreview");
            this.picturePreview.Name = "picturePreview";
            this.picturePreview.TabStop = false;
            this.picturePreview.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // actionSelectPanel
            // 
            this.actionSelectPanel.Controls.Add(this.label2);
            this.actionSelectPanel.Controls.Add(this.encodeActionButton);
            this.actionSelectPanel.Controls.Add(this.decodeActionButton);
            resources.ApplyResources(this.actionSelectPanel, "actionSelectPanel");
            this.actionSelectPanel.Name = "actionSelectPanel";
            this.actionSelectPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.actionSelectPanel_Paint);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // encodeActionButton
            // 
            resources.ApplyResources(this.encodeActionButton, "encodeActionButton");
            this.encodeActionButton.Name = "encodeActionButton";
            this.encodeActionButton.TabStop = true;
            this.encodeActionButton.UseVisualStyleBackColor = true;
            this.encodeActionButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // decodeActionButton
            // 
            resources.ApplyResources(this.decodeActionButton, "decodeActionButton");
            this.decodeActionButton.Name = "decodeActionButton";
            this.decodeActionButton.TabStop = true;
            this.decodeActionButton.UseVisualStyleBackColor = true;
            this.decodeActionButton.CheckedChanged += new System.EventHandler(this.decodeActionButton_CheckedChanged);
            // 
            // parametersPanel
            // 
            this.parametersPanel.Controls.Add(this.requiredParametersPanel);
            this.parametersPanel.Controls.Add(this.messageParameterPanel);
            resources.ApplyResources(this.parametersPanel, "parametersPanel");
            this.parametersPanel.Name = "parametersPanel";
            // 
            // requiredParametersPanel
            // 
            this.requiredParametersPanel.Controls.Add(this.flowLayoutPanel1);
            this.requiredParametersPanel.Controls.Add(this.flowLayoutPanel2);
            resources.ApplyResources(this.requiredParametersPanel, "requiredParametersPanel");
            this.requiredParametersPanel.Name = "requiredParametersPanel";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.encryptionKey);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // encryptionKey
            // 
            resources.ApplyResources(this.encryptionKey, "encryptionKey");
            this.encryptionKey.Name = "encryptionKey";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.positionSeed);
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // positionSeed
            // 
            resources.ApplyResources(this.positionSeed, "positionSeed");
            this.positionSeed.Name = "positionSeed";
            // 
            // messageParameterPanel
            // 
            this.messageParameterPanel.Controls.Add(this.label4);
            this.messageParameterPanel.Controls.Add(this.messageBox);
            resources.ApplyResources(this.messageParameterPanel, "messageParameterPanel");
            this.messageParameterPanel.Name = "messageParameterPanel";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // messageBox
            // 
            resources.ApplyResources(this.messageBox, "messageBox");
            this.messageBox.Name = "messageBox";
            // 
            // applyActionButton
            // 
            resources.ApplyResources(this.applyActionButton, "applyActionButton");
            this.applyActionButton.ForeColor = System.Drawing.Color.Black;
            this.applyActionButton.Name = "applyActionButton";
            this.applyActionButton.UseVisualStyleBackColor = true;
            this.applyActionButton.Click += new System.EventHandler(this.applyActionButton_Click);
            // 
            // outputPanel
            // 
            this.outputPanel.Controls.Add(this.encryptOutputPanel);
            this.outputPanel.Controls.Add(this.decryptOutputPanel);
            this.outputPanel.Controls.Add(this.saveButton);
            resources.ApplyResources(this.outputPanel, "outputPanel");
            this.outputPanel.Name = "outputPanel";
            // 
            // outputLabel
            // 
            resources.ApplyResources(this.outputLabel, "outputLabel");
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Click += new System.EventHandler(this.outputLabel_Click);
            // 
            // outputMessage
            // 
            resources.ApplyResources(this.outputMessage, "outputMessage");
            this.outputMessage.Name = "outputMessage";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // outputPage
            // 
            this.outputPage.Controls.Add(this.outputLabel);
            this.outputPage.Controls.Add(this.outputPanel);
            resources.ApplyResources(this.outputPage, "outputPage");
            this.outputPage.Name = "outputPage";
            this.outputPage.Paint += new System.Windows.Forms.PaintEventHandler(this.outputPage_Paint);
            // 
            // mainPage
            // 
            this.mainPage.Controls.Add(this.parametersPanel);
            this.mainPage.Controls.Add(this.actionSelectPanel);
            this.mainPage.Controls.Add(this.applyActionButton);
            this.mainPage.Controls.Add(this.picturePreview);
            this.mainPage.Controls.Add(this.button1);
            resources.ApplyResources(this.mainPage, "mainPage");
            this.mainPage.Name = "mainPage";
            this.mainPage.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPage_Paint);
            // 
            // decryptOutputPanel
            // 
            this.decryptOutputPanel.Controls.Add(this.outputMessage);
            resources.ApplyResources(this.decryptOutputPanel, "decryptOutputPanel");
            this.decryptOutputPanel.Name = "decryptOutputPanel";
            // 
            // encryptOutputPanel
            // 
            resources.ApplyResources(this.encryptOutputPanel, "encryptOutputPanel");
            this.encryptOutputPanel.Name = "encryptOutputPanel";
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // ImageEncoderView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.outputPage);
            this.Controls.Add(this.mainPage);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ImageEncoderView";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).EndInit();
            this.actionSelectPanel.ResumeLayout(false);
            this.actionSelectPanel.PerformLayout();
            this.parametersPanel.ResumeLayout(false);
            this.requiredParametersPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.messageParameterPanel.ResumeLayout(false);
            this.messageParameterPanel.PerformLayout();
            this.outputPanel.ResumeLayout(false);
            this.outputPage.ResumeLayout(false);
            this.outputPage.PerformLayout();
            this.mainPage.ResumeLayout(false);
            this.decryptOutputPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog imageUploadDialog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox picturePreview;
        private System.Windows.Forms.FlowLayoutPanel actionSelectPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton encodeActionButton;
        private System.Windows.Forms.RadioButton decodeActionButton;
        private System.Windows.Forms.FlowLayoutPanel parametersPanel;
        private System.Windows.Forms.FlowLayoutPanel requiredParametersPanel;
        private System.Windows.Forms.FlowLayoutPanel messageParameterPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox encryptionKey;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox positionSeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button applyActionButton;
        private System.Windows.Forms.FlowLayoutPanel outputPanel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.RichTextBox outputMessage;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.Panel outputPage;
        private System.Windows.Forms.Panel mainPage;
        private System.Windows.Forms.Panel decryptOutputPanel;
        private System.Windows.Forms.Panel encryptOutputPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

