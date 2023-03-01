namespace GUI
{
    partial class downloadfile
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
            this.components = new System.ComponentModel.Container();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.labelDownSize = new System.Windows.Forms.Label();
            this.progressBarDownloadFile = new System.Windows.Forms.ProgressBar();
            this.labelFileSizeTotal = new System.Windows.Forms.Label();
            this.labelProcess = new System.Windows.Forms.Label();
            this.labelDownSpd = new System.Windows.Forms.Label();
            this.labelDownNeedTime = new System.Windows.Forms.Label();
            this.labelSpanTime = new System.Windows.Forms.Label();
            this.labelAvgSpd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.置于后台运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于下载器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDownload.Location = new System.Drawing.Point(166, 122);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(224, 60);
            this.buttonDownload.TabIndex = 0;
            this.buttonDownload.Text = "下载";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click_1);
            // 
            // labelDownSize
            // 
            this.labelDownSize.AutoSize = true;
            this.labelDownSize.BackColor = System.Drawing.Color.Transparent;
            this.labelDownSize.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.labelDownSize.Location = new System.Drawing.Point(17, 47);
            this.labelDownSize.Name = "labelDownSize";
            this.labelDownSize.Size = new System.Drawing.Size(82, 24);
            this.labelDownSize.TabIndex = 2;
            this.labelDownSize.Text = "已下载：";
            // 
            // progressBarDownloadFile
            // 
            this.progressBarDownloadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBarDownloadFile.Location = new System.Drawing.Point(21, 64);
            this.progressBarDownloadFile.Name = "progressBarDownloadFile";
            this.progressBarDownloadFile.Size = new System.Drawing.Size(536, 41);
            this.progressBarDownloadFile.TabIndex = 3;
            // 
            // labelFileSizeTotal
            // 
            this.labelFileSizeTotal.AutoSize = true;
            this.labelFileSizeTotal.BackColor = System.Drawing.Color.Transparent;
            this.labelFileSizeTotal.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.labelFileSizeTotal.Location = new System.Drawing.Point(361, 47);
            this.labelFileSizeTotal.Name = "labelFileSizeTotal";
            this.labelFileSizeTotal.Size = new System.Drawing.Size(100, 24);
            this.labelFileSizeTotal.TabIndex = 4;
            this.labelFileSizeTotal.Text = "文件大小：";
            // 
            // labelProcess
            // 
            this.labelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProcess.AutoSize = true;
            this.labelProcess.BackColor = System.Drawing.Color.Transparent;
            this.labelProcess.Font = new System.Drawing.Font("微软雅黑 Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProcess.Location = new System.Drawing.Point(253, 15);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(70, 46);
            this.labelProcess.TabIndex = 5;
            this.labelProcess.Text = "0%";
            // 
            // labelDownSpd
            // 
            this.labelDownSpd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDownSpd.AutoSize = true;
            this.labelDownSpd.BackColor = System.Drawing.Color.Transparent;
            this.labelDownSpd.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDownSpd.Location = new System.Drawing.Point(22, 37);
            this.labelDownSpd.Name = "labelDownSpd";
            this.labelDownSpd.Size = new System.Drawing.Size(140, 24);
            this.labelDownSpd.TabIndex = 6;
            this.labelDownSpd.Text = "速度：0.00MB/s";
            // 
            // labelDownNeedTime
            // 
            this.labelDownNeedTime.AutoSize = true;
            this.labelDownNeedTime.BackColor = System.Drawing.Color.Transparent;
            this.labelDownNeedTime.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.labelDownNeedTime.Location = new System.Drawing.Point(361, 12);
            this.labelDownNeedTime.Name = "labelDownNeedTime";
            this.labelDownNeedTime.Size = new System.Drawing.Size(100, 24);
            this.labelDownNeedTime.TabIndex = 7;
            this.labelDownNeedTime.Text = "剩余时间：";
            // 
            // labelSpanTime
            // 
            this.labelSpanTime.AutoSize = true;
            this.labelSpanTime.BackColor = System.Drawing.Color.Transparent;
            this.labelSpanTime.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSpanTime.Location = new System.Drawing.Point(17, 12);
            this.labelSpanTime.Name = "labelSpanTime";
            this.labelSpanTime.Size = new System.Drawing.Size(64, 24);
            this.labelSpanTime.TabIndex = 8;
            this.labelSpanTime.Text = "用时：";
            // 
            // labelAvgSpd
            // 
            this.labelAvgSpd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAvgSpd.AutoSize = true;
            this.labelAvgSpd.BackColor = System.Drawing.Color.Transparent;
            this.labelAvgSpd.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAvgSpd.Location = new System.Drawing.Point(417, 37);
            this.labelAvgSpd.Name = "labelAvgSpd";
            this.labelAvgSpd.Size = new System.Drawing.Size(140, 24);
            this.labelAvgSpd.TabIndex = 9;
            this.labelAvgSpd.Text = "均速：0.00MB/s";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.labelAvgSpd);
            this.panel1.Controls.Add(this.buttonDownload);
            this.panel1.Controls.Add(this.progressBarDownloadFile);
            this.panel1.Controls.Add(this.labelDownSpd);
            this.panel1.Controls.Add(this.labelProcess);
            this.panel1.Location = new System.Drawing.Point(0, 446);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 196);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.置于后台运行ToolStripMenuItem,
            this.关于下载器ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(71, 52);
            // 
            // 置于后台运行ToolStripMenuItem
            // 
            this.置于后台运行ToolStripMenuItem.Name = "置于后台运行ToolStripMenuItem";
            this.置于后台运行ToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            // 
            // 关于下载器ToolStripMenuItem
            // 
            this.关于下载器ToolStripMenuItem.Name = "关于下载器ToolStripMenuItem";
            this.关于下载器ToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.labelSpanTime);
            this.panel2.Controls.Add(this.labelFileSizeTotal);
            this.panel2.Controls.Add(this.labelDownNeedTime);
            this.panel2.Controls.Add(this.labelDownSize);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(581, 149);
            this.panel2.TabIndex = 11;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.label1.Location = new System.Drawing.Point(17, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "下载直链：";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(0, 147);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(581, 301);
            this.panel3.TabIndex = 13;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // downloadfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.Name = "downloadfile";
            this.Size = new System.Drawing.Size(581, 642);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Label labelDownSize;
        private System.Windows.Forms.ProgressBar progressBarDownloadFile;
        private System.Windows.Forms.Label labelFileSizeTotal;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.Label labelDownSpd;
        private System.Windows.Forms.Label labelDownNeedTime;
        private System.Windows.Forms.Label labelSpanTime;
        private System.Windows.Forms.Label labelAvgSpd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 置于后台运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于下载器ToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
    }
}

