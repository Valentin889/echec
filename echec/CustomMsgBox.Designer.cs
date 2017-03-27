namespace echec
{
    partial class CustomMsgBox
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
            this.lblPiece = new System.Windows.Forms.Label();
            this.btnQueen = new System.Windows.Forms.Button();
            this.btnRook = new System.Windows.Forms.Button();
            this.btnBishop = new System.Windows.Forms.Button();
            this.btnKnight = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPiece
            // 
            this.lblPiece.AutoSize = true;
            this.lblPiece.Location = new System.Drawing.Point(74, 34);
            this.lblPiece.Name = "lblPiece";
            this.lblPiece.Size = new System.Drawing.Size(126, 13);
            this.lblPiece.TabIndex = 0;
            this.lblPiece.Text = "Veuillez choisir une pièce";
            // 
            // btnQueen
            // 
            this.btnQueen.Location = new System.Drawing.Point(12, 99);
            this.btnQueen.Name = "btnQueen";
            this.btnQueen.Size = new System.Drawing.Size(75, 23);
            this.btnQueen.TabIndex = 1;
            this.btnQueen.Text = "Queen";
            this.btnQueen.UseVisualStyleBackColor = true;
            this.btnQueen.Click += new System.EventHandler(this.btnQueen_Click);
            // 
            // btnRook
            // 
            this.btnRook.Location = new System.Drawing.Point(93, 99);
            this.btnRook.Name = "btnRook";
            this.btnRook.Size = new System.Drawing.Size(75, 23);
            this.btnRook.TabIndex = 2;
            this.btnRook.Text = "Rook";
            this.btnRook.UseVisualStyleBackColor = true;
            this.btnRook.Click += new System.EventHandler(this.btnRook_Click);
            // 
            // btnBishop
            // 
            this.btnBishop.Location = new System.Drawing.Point(174, 99);
            this.btnBishop.Name = "btnBishop";
            this.btnBishop.Size = new System.Drawing.Size(75, 23);
            this.btnBishop.TabIndex = 3;
            this.btnBishop.Text = "Bishop";
            this.btnBishop.UseVisualStyleBackColor = true;
            this.btnBishop.Click += new System.EventHandler(this.btnBishop_Click);
            // 
            // btnKnight
            // 
            this.btnKnight.Location = new System.Drawing.Point(255, 99);
            this.btnKnight.Name = "btnKnight";
            this.btnKnight.Size = new System.Drawing.Size(75, 23);
            this.btnKnight.TabIndex = 4;
            this.btnKnight.Text = "Knight";
            this.btnKnight.UseVisualStyleBackColor = true;
            this.btnKnight.Click += new System.EventHandler(this.btnKnight_Click);
            // 
            // CustomMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 132);
            this.Controls.Add(this.btnKnight);
            this.Controls.Add(this.btnBishop);
            this.Controls.Add(this.btnRook);
            this.Controls.Add(this.btnQueen);
            this.Controls.Add(this.lblPiece);
            this.Name = "CustomMsgBox";
            this.Text = "CustomMsgBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPiece;
        private System.Windows.Forms.Button btnQueen;
        private System.Windows.Forms.Button btnRook;
        private System.Windows.Forms.Button btnBishop;
        private System.Windows.Forms.Button btnKnight;
    }
}