namespace WFEmisionmasiva
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnEmitirPolzias = new System.Windows.Forms.Button();
            this.btnCargarSolicitudes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(674, 278);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnEmitirPolzias
            // 
            this.btnEmitirPolzias.Location = new System.Drawing.Point(24, 12);
            this.btnEmitirPolzias.Name = "btnEmitirPolzias";
            this.btnEmitirPolzias.Size = new System.Drawing.Size(91, 31);
            this.btnEmitirPolzias.TabIndex = 1;
            this.btnEmitirPolzias.Text = "Emitir";
            this.btnEmitirPolzias.UseVisualStyleBackColor = true;
            this.btnEmitirPolzias.Click += new System.EventHandler(this.btnEmitirPolzias_Click);
            // 
            // btnCargarSolicitudes
            // 
            this.btnCargarSolicitudes.Location = new System.Drawing.Point(121, 12);
            this.btnCargarSolicitudes.Name = "btnCargarSolicitudes";
            this.btnCargarSolicitudes.Size = new System.Drawing.Size(180, 31);
            this.btnCargarSolicitudes.TabIndex = 2;
            this.btnCargarSolicitudes.Text = "Cargar Solicitudes";
            this.btnCargarSolicitudes.UseVisualStyleBackColor = true;
            this.btnCargarSolicitudes.Click += new System.EventHandler(this.btnCargarSolicitudes_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 353);
            this.Controls.Add(this.btnCargarSolicitudes);
            this.Controls.Add(this.btnEmitirPolzias);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Emisión Masiva";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnEmitirPolzias;
        private System.Windows.Forms.Button btnCargarSolicitudes;
    }
}

