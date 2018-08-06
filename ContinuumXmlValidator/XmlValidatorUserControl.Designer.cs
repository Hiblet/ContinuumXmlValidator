namespace ContinuumXmlValidator
{
    partial class XmlValidatorUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelVersionCode = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxDataField = new System.Windows.Forms.ComboBox();
            this.labelXmlData = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxSchemaField = new System.Windows.Forms.ComboBox();
            this.labelSchemaField = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxOutputField = new System.Windows.Forms.TextBox();
            this.labelOutputField = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelVersionCode);
            this.panel1.Controls.Add(this.labelHeader);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 50);
            this.panel1.TabIndex = 0;
            // 
            // labelVersionCode
            // 
            this.labelVersionCode.AutoSize = true;
            this.labelVersionCode.Location = new System.Drawing.Point(143, 21);
            this.labelVersionCode.Name = "labelVersionCode";
            this.labelVersionCode.Size = new System.Drawing.Size(46, 13);
            this.labelVersionCode.TabIndex = 1;
            this.labelVersionCode.Text = "(v 1.0.2)";
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(8, 13);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(128, 24);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "XML Validator";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBoxDataField);
            this.panel2.Controls.Add(this.labelXmlData);
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 50);
            this.panel2.TabIndex = 1;
            // 
            // comboBoxDataField
            // 
            this.comboBoxDataField.FormattingEnabled = true;
            this.comboBoxDataField.Location = new System.Drawing.Point(107, 15);
            this.comboBoxDataField.Name = "comboBoxDataField";
            this.comboBoxDataField.Size = new System.Drawing.Size(180, 21);
            this.comboBoxDataField.TabIndex = 1;
            // 
            // labelXmlData
            // 
            this.labelXmlData.AutoSize = true;
            this.labelXmlData.Location = new System.Drawing.Point(10, 18);
            this.labelXmlData.Name = "labelXmlData";
            this.labelXmlData.Size = new System.Drawing.Size(57, 13);
            this.labelXmlData.TabIndex = 0;
            this.labelXmlData.Text = "XML Field:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comboBoxSchemaField);
            this.panel3.Controls.Add(this.labelSchemaField);
            this.panel3.Location = new System.Drawing.Point(0, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(300, 50);
            this.panel3.TabIndex = 2;
            // 
            // comboBoxSchemaField
            // 
            this.comboBoxSchemaField.FormattingEnabled = true;
            this.comboBoxSchemaField.Location = new System.Drawing.Point(107, 15);
            this.comboBoxSchemaField.Name = "comboBoxSchemaField";
            this.comboBoxSchemaField.Size = new System.Drawing.Size(180, 21);
            this.comboBoxSchemaField.TabIndex = 1;
            // 
            // labelSchemaField
            // 
            this.labelSchemaField.AutoSize = true;
            this.labelSchemaField.Location = new System.Drawing.Point(10, 18);
            this.labelSchemaField.Name = "labelSchemaField";
            this.labelSchemaField.Size = new System.Drawing.Size(74, 13);
            this.labelSchemaField.TabIndex = 0;
            this.labelSchemaField.Text = "Schema Field:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBoxOutputField);
            this.panel4.Controls.Add(this.labelOutputField);
            this.panel4.Location = new System.Drawing.Point(0, 150);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(300, 50);
            this.panel4.TabIndex = 3;
            // 
            // textBoxOutputField
            // 
            this.textBoxOutputField.Location = new System.Drawing.Point(107, 15);
            this.textBoxOutputField.Name = "textBoxOutputField";
            this.textBoxOutputField.Size = new System.Drawing.Size(180, 20);
            this.textBoxOutputField.TabIndex = 1;
            this.textBoxOutputField.Text = "XmlErrors";
            // 
            // labelOutputField
            // 
            this.labelOutputField.AutoSize = true;
            this.labelOutputField.Location = new System.Drawing.Point(10, 18);
            this.labelOutputField.Name = "labelOutputField";
            this.labelOutputField.Size = new System.Drawing.Size(67, 13);
            this.labelOutputField.TabIndex = 0;
            this.labelOutputField.Text = "Output Field:";
            // 
            // XmlValidatorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "XmlValidatorUserControl";
            this.Size = new System.Drawing.Size(300, 200);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelVersionCode;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBoxDataField;
        private System.Windows.Forms.Label labelXmlData;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxSchemaField;
        private System.Windows.Forms.Label labelSchemaField;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxOutputField;
        private System.Windows.Forms.Label labelOutputField;
    }
}
