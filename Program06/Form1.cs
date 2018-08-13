using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using com.epson.pos.driver;
using System.Data.SQLite;
using System.Diagnostics;

namespace testPrinter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

        ArrayList lista = new ArrayList<item>();

        internal System.Windows.Forms.Button cmdClose;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Button cmdPrint;
		internal System.Windows.Forms.PictureBox pbImage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		// Constant variable holding the printer name.
		private const string PRINTER_NAME = "EPSON TM-T20 Receipt";
        internal Button button1;
        private DataGridView dataGridView1;

        // Variables/Objects.
        private StatusAPI m_objAPI;
		
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            m_objAPI = new StatusAPI();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmdClose = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(695, 463);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 13;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cmdPrint);
            this.GroupBox1.Location = new System.Drawing.Point(55, 430);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(322, 74);
            this.GroupBox1.TabIndex = 12;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Receipt";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Location = new System.Drawing.Point(48, 19);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(231, 37);
            this.cmdPrint.TabIndex = 0;
            this.cmdPrint.Text = "Imprimir ticket";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // pbImage
            // 
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.InitialImage = null;
            this.pbImage.Location = new System.Drawing.Point(21, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(207, 73);
            this.pbImage.TabIndex = 14;
            this.pbImage.TabStop = false;
            this.pbImage.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "Leer Datos";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(55, 117);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(689, 270);
            this.dataGridView1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(813, 516);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impresion de tickets";
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		// The executed function when the Print button is clicked.
		private void cmdPrint_Click(object sender, System.EventArgs e)
		{
            Boolean isFinish;
            PrintDocument pdPrint = new PrintDocument();
			pdPrint.PrintPage += new PrintPageEventHandler(pdPrint_PrintPage);
            // Change the printer to the indicated printer.
			pdPrint.PrinterSettings.PrinterName = PRINTER_NAME;

			try 
			{
                // Open a printer status monitor for the selected printer.
                if (m_objAPI.OpenMonPrinter(OpenType.TYPE_PRINTER, pdPrint.PrinterSettings.PrinterName) == ErrorCode.SUCCESS)
                {
                    if (pdPrint.PrinterSettings.IsValid)
                    {
                        pdPrint.DocumentName = "Testing";
                        // Start printing.
                        pdPrint.Print();

                        // Check printing status.
                        isFinish = false;

                        // Perform the status check as long as the status is not ASB_PRINT_SUCCESS.
                        do
                        {
                            if (m_objAPI.Status.ToString().Contains(ASB.ASB_PRINT_SUCCESS.ToString()))
                                isFinish = true;

                        } while (!isFinish);

                        // Notify printing completion.
                        MessageBox.Show("Printing complete.", "Program06", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }
                    else
                        MessageBox.Show("Printer is not available.", "Program06", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    // Always close the Status Monitor after using the Status API.
				    if(m_objAPI.CloseMonPrinter() != ErrorCode.SUCCESS)
                        MessageBox.Show("Failed to close printer status monitor.", "Program06", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                    MessageBox.Show("Failed to open printer status monitor.", "Program06", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch
			{
                MessageBox.Show("Failed to open StatusAPI.", "Program06", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

        }

        // The event handler function when pdPrint.Print is called.
        // This is where the actual printing of sample data to the printer.
        private void pdPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x, y, lineOffset;

           



            // Instantiate font objects used in printing.
            Font printFont = new Font("Lucida Console", (float)8, FontStyle.Regular, GraphicsUnit.Point); // Substituted to FontA Font

            e.Graphics.PageUnit = GraphicsUnit.Point;

            // Draw the bitmap
            x = 0;
            y = 0;
            e.Graphics.DrawImage(pbImage.Image, x, y, pbImage.Image.Width - 13, pbImage.Image.Height - 10);

            // Print the receipt text
            lineOffset = printFont.GetHeight(e.Graphics) - (float)0.5;
            x = 10;
            y = 70 + lineOffset;
            e.Graphics.DrawString("Metge Mir 23, Sabadell", printFont, Brushes.Black, x, y);
            y += lineOffset;
            e.Graphics.DrawString("        TEL   931 70 49 77", printFont, Brushes.Black, x, y);
            y += lineOffset;
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), printFont, Brushes.Black, x, y);
            y = y + (lineOffset * (float)5) ;





            //e.Graphics.DrawString("Burritos                     €20.00", printFont, Brushes.Black, x, y);
            //y += lineOffset;
            //e.Graphics.DrawString("Nachos                       €30.00", printFont, Brushes.Black, x, y);
            //y += lineOffset;
            //e.Graphics.DrawString("Bebidas                      €40.00", printFont, Brushes.Black, x, y);
            //y += lineOffset;
            //e.Graphics.DrawString("Postres                      €50.00", printFont, Brushes.Black, x, y);
            //y += lineOffset;
            //e.Graphics.DrawString("Limonada Artesanal           €60.00", printFont, Brushes.Black, x, y);

            for (int i=0; i < lista.Count; i++)
            {
                item it = lista[i] as item;

                e.Graphics.DrawString(  string.Format ("{0}   {1}",  it.prd, it.precio.ToString() ),  printFont, Brushes.Black, x, y);
                y += lineOffset;

            }





            y += (lineOffset * (float)5);
           // e.Graphics.DrawString("Tax excluded.               €200.00", printFont, Brushes.Black, x, y);
           // y += lineOffset;
          //  e.Graphics.DrawString("Tax     5.0%                 €10.00", printFont, Brushes.Black, x, y);
          //  y += lineOffset;
            e.Graphics.DrawString("___________________________________", printFont, Brushes.Black, x, y);

            printFont = new Font("MingLiU", 19, FontStyle.Regular, GraphicsUnit.Point);
            lineOffset = printFont.GetHeight(e.Graphics) - 3;
            y += lineOffset;
            e.Graphics.DrawString("Total     €210.00", printFont, Brushes.Black, x - 1, y);

            printFont = new Font("Lucida Console", (float)8, FontStyle.Regular, GraphicsUnit.Point);
            lineOffset = printFont.GetHeight(e.Graphics);
            y = y + lineOffset + 1;
           // e.Graphics.DrawString("Customer's payment         €250.00", printFont, Brushes.Black, x, y);
          //  y += lineOffset;
          //  e.Graphics.DrawString("Change                      €40.00", printFont, Brushes.Black, x, y - 2);

            // Indicate that no more data to print, and the Print Document can now send the print data to the spooler.
            e.HasMorePages = false;
        }

		// The executed function when the Close button is clicked.
		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=file.db; Version=3;  Foreign Keys=True;";
            //string sql = "insert into productos (producto, precio  )  values (\"Burrito 'Charro'\", 8.50)";
            string sql = "select * from productos";

            string prd;
            double precio;
            int id;


            //SQLiteConnection.CreateFile("file.db");

            SQLiteConnection m_dbConnection = new SQLiteConnection(connectionString);

            SQLiteDataReader rd;

            m_dbConnection.Open();


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            rd = command.ExecuteReader();

            while (rd.Read())
            {
                id = (rd.GetInt16(0));
                prd = (rd.GetString(1)).ToString();
                precio = Convert.ToDouble (rd.GetFloat(2));

                lista.Add(new item (  prd, precio ));

            }

            //sql = "insert into highscores (name, score) values ('Me', 9001)";

            //command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();

            m_dbConnection.Close();

            dataGridView1.DataSource = lista;




        }

        private class ArrayList<T> : ArrayList
        {
        }
    }
}
