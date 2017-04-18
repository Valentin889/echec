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
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnYes = new System.Windows.Forms.RadioButton();
            this.rbtnNo = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblNom1
            // 
            this.lblNom1.AutoSize = true;
            this.lblNom1.Location = new System.Drawing.Point(12, 26);
            this.lblNom1.Name = "lblNom1";
            this.lblNom1.Size = new System.Drawing.Size(118, 13);
            this.lblNom1.TabIndex = 0;
            this.lblNom1.Text = "nom du joueur 1 (blanc)";
            // 
            // lblNom2
            // 
            this.lblNom2.AutoSize = true;
            this.lblNom2.Location = new System.Drawing.Point(12, 56);
            this.lblNom2.Name = "lblNom2";
            this.lblNom2.Size = new System.Drawing.Size(109, 13);
            this.lblNom2.TabIndex = 1;
            this.lblNom2.Text = "nom du joueur 2 (noir)";
            // 
            // tbxNom1
            // 
            this.tbxNom1.Location = new System.Drawing.Point(136, 26);
            this.tbxNom1.Name = "tbxNom1";
            this.tbxNom1.Size = new System.Drawing.Size(216, 20);
            this.tbxNom1.TabIndex = 2;
            // 
            // tbxNom2
            // 
            this.tbxNom2.Location = new System.Drawing.Point(136, 56);
            this.tbxNom2.Name = "tbxNom2";
            this.tbxNom2.Size = new System.Drawing.Size(216, 20);
            this.tbxNom2.TabIndex = 3;
            // 
            // btnJouer
            // 
            this.btnJouer.Location = new System.Drawing.Point(92, 151);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Voulez vous que le jeu se tourne à chaque coup";
            // 
            // rbtnYes
            // 
            this.rbtnYes.AutoSize = true;
            this.rbtnYes.Checked = true;
            this.rbtnYes.Location = new System.Drawing.Point(269, 91);
            this.rbtnYes.Name = "rbtnYes";
            this.rbtnYes.Size = new System.Drawing.Size(39, 17);
            this.rbtnYes.TabIndex = 8;
            this.rbtnYes.TabStop = true;
            this.rbtnYes.Text = "oui";
            this.rbtnYes.UseVisualStyleBackColor = true;
            // 
            // rbtnNo
            // 
            this.rbtnNo.AutoSize = true;
            this.rbtnNo.Location = new System.Drawing.Point(324, 91);
            this.rbtnNo.Name = "rbtnNo";
            this.rbtnNo.Size = new System.Drawing.Size(43, 17);
            this.rbtnNo.TabIndex = 9;
            this.rbtnNo.Text = "non";
            this.rbtnNo.UseVisualStyleBackColor = true;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 292);
            this.Controls.Add(this.rbtnNo);
            this.Controls.Add(this.rbtnYes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRègle);
            this.Controls.Add(this.btnJouer);
            this.Controls.Add(this.tbxNom2);
            this.Controls.Add(this.tbxNom1);
            this.Controls.Add(this.lblNom2);
            this.Controls.Add(this.lblNom1);
            this.Name = "frmMenu";
            this.Text = "menu";
            this.Load += new System.EventHandler(this.frmMenu_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtnYes;
        private System.Windows.Forms.RadioButton rbtnNo;
    }
}

