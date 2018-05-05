namespace WindowsFormsApp1
{
    partial class ImageEncoder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEncoder));
            this.imageUploadDialog = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.picturePreview = new System.Windows.Forms.PictureBox();
            this.actionSelectPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.encodeActionButton = new System.Windows.Forms.RadioButton();
            this.decodeActionButton = new System.Windows.Forms.RadioButton();
            this.parametersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.requiredParametersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.messageParameterPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).BeginInit();
            this.actionSelectPanel.SuspendLayout();
            this.parametersPanel.SuspendLayout();
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
            resources.ApplyResources(this.requiredParametersPanel, "requiredParametersPanel");
            this.requiredParametersPanel.Name = "requiredParametersPanel";
            // 
            // messageParameterPanel
            // 
            resources.ApplyResources(this.messageParameterPanel, "messageParameterPanel");
            this.messageParameterPanel.Name = "messageParameterPanel";
            // 
            // ImageEncoder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.parametersPanel);
            this.Controls.Add(this.actionSelectPanel);
            this.Controls.Add(this.picturePreview);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ImageEncoder";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).EndInit();
            this.actionSelectPanel.ResumeLayout(false);
            this.actionSelectPanel.PerformLayout();
            this.parametersPanel.ResumeLayout(false);
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
    }
}

