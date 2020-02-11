namespace 省市区联动
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
            this.cboProvince = new System.Windows.Forms.ComboBox();
            this.cboCity = new System.Windows.Forms.ComboBox();
            this.cboDistinct = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboProvince
            // 
            this.cboProvince.FormattingEnabled = true;
            this.cboProvince.Location = new System.Drawing.Point(12, 36);
            this.cboProvince.Name = "cboProvince";
            this.cboProvince.Size = new System.Drawing.Size(121, 20);
            this.cboProvince.TabIndex = 0;
            this.cboProvince.SelectedIndexChanged += new System.EventHandler(this.cboProvince_SelectedIndexChanged);
            // 
            // cboCity
            // 
            this.cboCity.FormattingEnabled = true;
            this.cboCity.Location = new System.Drawing.Point(140, 36);
            this.cboCity.Name = "cboCity";
            this.cboCity.Size = new System.Drawing.Size(121, 20);
            this.cboCity.TabIndex = 1;
            this.cboCity.SelectedIndexChanged += new System.EventHandler(this.cboCity_SelectedIndexChanged);
            // 
            // cboDistinct
            // 
            this.cboDistinct.FormattingEnabled = true;
            this.cboDistinct.Location = new System.Drawing.Point(268, 35);
            this.cboDistinct.Name = "cboDistinct";
            this.cboDistinct.Size = new System.Drawing.Size(121, 20);
            this.cboDistinct.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 202);
            this.Controls.Add(this.cboDistinct);
            this.Controls.Add(this.cboCity);
            this.Controls.Add(this.cboProvince);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboProvince;
        private System.Windows.Forms.ComboBox cboCity;
        private System.Windows.Forms.ComboBox cboDistinct;
    }
}

