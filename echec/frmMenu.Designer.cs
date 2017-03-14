namespace echec
{
    partial class frmMenu
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNom1 = new System.Windows.Forms.Label();
            this.lblNom2 = new System.Windows.Forms.Label();
            this.tbxNom1 = new System.Windows.Forms.TextBox();
            this.tbxNom2 = new System.Windows.Forms.TextBox();
            this.btnJouer = new System.Windows.Forms.Button();
            this.btnRègle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNom1
            // 
            this.lblNom1.AutoSize = true;
            this.lblNom1.Location = new System.Drawing.Point(13, 28);
            this.lblNom1.Name = "lblNom1";
            this.lblNom1.Size = new System.Drawing.Size(118, 13);
            this.lblNom1.TabIndex = 0;
            this.lblNom1.Text = "nom du joueur 1 (blanc)";
            // 
            // lblNom2
            // 
            this.lblNom2.AutoSize = true;
            this.lblNom2.Location = new System.Drawing.Point(13, 58);
            this.lblNom2.Name = "lblNom2";
            this.lblNom2.Size = new System.Drawing.Size(109, 13);
            this.lblNom2.TabIndex = 1;
            this.lblNom2.Text = "nom du joueur 2 (noir)";
            // 
            // tbxNom1
            // 
            this.tbxNom1.Location = new System.Drawing.Point(134, 28);
            this.tbxNom1.Name = "tbxNom1";
            this.tbxNom1.Size = new System.Drawing.Size(100, 20);
            this.tbxNom1.TabIndex = 2;
            // 
            // tbxNom2
            // 
            this.tbxNom2.Location = new System.Drawing.Point(134, 58);
            this.tbxNom2.Name = "tbxNom2";
            this.tbxNom2.Size = new System.Drawing.Size(100, 20);
            this.tbxNom2.TabIndex = 3;
            // 
            // btnJouer
            // 
            this.btnJouer.Location = new System.Drawing.Point(78, 100);
            this.btnJouer.Name = "btnJouer";
            this.btnJouer.Size = new System.Drawing.Size(75, 23);
            this.btnJouer.TabIndex = 4;
            this.btnJouer.Text = "Jouer";
            this.btnJouer.UseVisualStyleBackColor = true;
            this.btnJouer.Click += new System.EventHandler(this.btnJouer_Click);
            // 
            // btnRègle
            // 
            this.btnRègle.Location = new System.Drawing.Point(16, 258);
            this.btnRègle.Name = "btnRègle";
            this.btnRègle.Size = new System.Drawing.Size(75, 23);
            this.btnRègle.TabIndex = 5;
            this.btnRègle.Text = "règle";
            this.btnRègle.UseVisualStyleBackColor = true;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 293);
            this.Controls.Add(this.btnRègle);
            this.Controls.Add(this.btnJouer);
            this.Controls.Add(this.tbxNom2);
            this.Controls.Add(this.tbxNom1);
            this.Controls.Add(this.lblNom2);
            this.Controls.Add(this.lblNom1);
            this.Name = "frmMenu";
            this.Text = "menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNom1;
        private System.Windows.Forms.Label lblNom2;
        private System.Windows.Forms.TextBox tbxNom1;
        private System.Windows.Forms.TextBox tbxNom2;
        private System.Windows.Forms.Button btnJouer;
        private System.Windows.Forms.Button btnRègle;
    }
}

