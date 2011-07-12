namespace NConvert
{
    partial class MoreSetting
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
            this.btnSetMoreInfo = new System.Windows.Forms.Button();
            this.tbxPageSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxPostsTableSize = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSetMoreInfo
            // 
            this.btnSetMoreInfo.Location = new System.Drawing.Point(33, 92);
            this.btnSetMoreInfo.Name = "btnSetMoreInfo";
            this.btnSetMoreInfo.Size = new System.Drawing.Size(157, 23);
            this.btnSetMoreInfo.TabIndex = 0;
            this.btnSetMoreInfo.Text = "保存设置";
            this.btnSetMoreInfo.UseVisualStyleBackColor = true;
            this.btnSetMoreInfo.Click += new System.EventHandler(this.btnSetMoreInfo_Click);
            // 
            // tbxPageSize
            // 
            this.tbxPageSize.Location = new System.Drawing.Point(90, 17);
            this.tbxPageSize.Name = "tbxPageSize";
            this.tbxPageSize.Size = new System.Drawing.Size(100, 21);
            this.tbxPageSize.TabIndex = 1;
            this.tbxPageSize.Text = "1000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "分页大小";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "分表大小";
            // 
            // tbxPostsTableSize
            // 
            this.tbxPostsTableSize.Location = new System.Drawing.Point(90, 55);
            this.tbxPostsTableSize.Name = "tbxPostsTableSize";
            this.tbxPostsTableSize.Size = new System.Drawing.Size(100, 21);
            this.tbxPostsTableSize.TabIndex = 3;
            this.tbxPostsTableSize.Text = "30000";
            // 
            // MoreSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 130);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxPostsTableSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxPageSize);
            this.Controls.Add(this.btnSetMoreInfo);
            this.Name = "MoreSetting";
            this.Text = "扩展设置";
            this.Load += new System.EventHandler(this.MoreSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetMoreInfo;
        private System.Windows.Forms.TextBox tbxPageSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPostsTableSize;
    }
}