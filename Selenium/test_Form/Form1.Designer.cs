namespace test_Form
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtWebUrl = new System.Windows.Forms.TextBox();
            this.rtbXml = new System.Windows.Forms.TextBox();
            this.webserver = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtWebUrl
            // 
            this.txtWebUrl.Location = new System.Drawing.Point(78, 18);
            this.txtWebUrl.Name = "txtWebUrl";
            this.txtWebUrl.Size = new System.Drawing.Size(243, 21);
            this.txtWebUrl.TabIndex = 2;
            this.txtWebUrl.Enter += new System.EventHandler(this.txtWebUrl_Enter);
            // 
            // rtbXml
            // 
            this.rtbXml.Location = new System.Drawing.Point(13, 59);
            this.rtbXml.Multiline = true;
            this.rtbXml.Name = "rtbXml";
            this.rtbXml.Size = new System.Drawing.Size(647, 274);
            this.rtbXml.TabIndex = 3;
            // 
            // webserver
            // 
            this.webserver.AutoSize = true;
            this.webserver.Location = new System.Drawing.Point(13, 21);
            this.webserver.Name = "webserver";
            this.webserver.Size = new System.Drawing.Size(59, 12);
            this.webserver.TabIndex = 4;
            this.webserver.Text = "webserver";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(356, 18);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "button1";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 345);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.webserver);
            this.Controls.Add(this.rtbXml);
            this.Controls.Add(this.txtWebUrl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWebUrl;
        private System.Windows.Forms.TextBox rtbXml;
        private System.Windows.Forms.Label webserver;
        private System.Windows.Forms.Button btnCreate;
    }
}

