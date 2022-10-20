
namespace QuickHullZalevskiy
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_gererate_quick_hull = new System.Windows.Forms.Button();
            this.button_clear_canvas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox1.Location = new System.Drawing.Point(174, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(614, 426);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // button_gererate_quick_hull
            // 
            this.button_gererate_quick_hull.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_gererate_quick_hull.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_gererate_quick_hull.Location = new System.Drawing.Point(27, 27);
            this.button_gererate_quick_hull.Name = "button_gererate_quick_hull";
            this.button_gererate_quick_hull.Size = new System.Drawing.Size(107, 80);
            this.button_gererate_quick_hull.TabIndex = 1;
            this.button_gererate_quick_hull.Text = "Start QuickHull";
            this.button_gererate_quick_hull.UseVisualStyleBackColor = true;
            this.button_gererate_quick_hull.Click += new System.EventHandler(this.button_gererate_quick_hull_Click);
            // 
            // button_clear_canvas
            // 
            this.button_clear_canvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_clear_canvas.Location = new System.Drawing.Point(27, 148);
            this.button_clear_canvas.Name = "button_clear_canvas";
            this.button_clear_canvas.Size = new System.Drawing.Size(107, 91);
            this.button_clear_canvas.TabIndex = 2;
            this.button_clear_canvas.Text = "Clear Canvas";
            this.button_clear_canvas.UseVisualStyleBackColor = true;
            this.button_clear_canvas.Click += new System.EventHandler(this.button_clear_canvas_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_clear_canvas);
            this.Controls.Add(this.button_gererate_quick_hull);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_gererate_quick_hull;
        private System.Windows.Forms.Button button_clear_canvas;
    }
}

